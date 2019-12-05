using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using src.Entities;
using src.Services.Parsers;

namespace src.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ISrtParser _srtParser;
        private readonly IPhraseSplitter _phraseSplitter;

        public MoviesService(ISrtParser srtParser,
            IPhraseSplitter phraseSplitter)
        {
            _srtParser = srtParser;
            _phraseSplitter = phraseSplitter;
        }

        public Movie CreateMovie(long id, string rawContent)
        {
            var subs = _srtParser.Parse(rawContent).ToArray();
            var words = GetWordEntries(subs);
            
            var movie = new Movie
            {
                Id = id,
                
                Hashcode = rawContent.GetHashCode(),
                Subtitles = subs,
            };
            
            movie.SetWords(words);

            return movie;
        }
        
        private Dictionary<string, WordEntry> GetWordEntries(SubtitleItem[] subs)
        {
            var dict = new Dictionary<string, WordEntry>();
            foreach (var sub in subs)
            {
                if (sub == null) continue;
                
                var cleanStr = Regex.Replace(sub.Text, "[^A-Za-z0-9 -']", "")
                    .Replace("!", ""); // ?

                var words = _phraseSplitter.Split(cleanStr);
                foreach (var word in words)
                {
                    if (string.IsNullOrEmpty(word))
                        continue;

                    if (dict.Keys.Contains(word))
                        dict[word].Increment();
                    else
                        dict.Add(word, new WordEntry(word));

                    dict[word].FoundInSub(sub.Num);
                }
            }

            return dict;
        }
    }
    
    

    public interface IMoviesService
    {
        Movie CreateMovie(long id, string rawContent);
    }
}