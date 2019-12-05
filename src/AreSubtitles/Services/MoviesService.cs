using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using src.Entities;
using src.Services.Parsers;
using src.Storage;

namespace src.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ISrtParser _srtParser;
        private readonly IPhraseSplitter _phraseSplitter;
        private readonly IStorage _storage;

        public MoviesService(
            ISrtParser srtParser,
            IPhraseSplitter phraseSplitter,
            IStorage storage)
        {
            _srtParser = srtParser;
            _phraseSplitter = phraseSplitter;
            _storage = storage;
        }

        public async Task<Movie> CreateMovie(string rawContent)
        {
            if (_storage.GetIdIfExists(rawContent.GetHashCode(), out long foundId))
                return await Task.FromResult(_storage.Get(foundId));
            
            var subs = _srtParser.Parse(rawContent).ToArray();
            var words = GetWordEntries(subs);
            
            var movie = new Movie
            {
                Id = IdGenerator.GetNewId(),
                Hashcode = rawContent.GetHashCode(),
                Subtitles = subs,
            };
            
            movie.SetWords(words);
            
            _storage.Put(movie.Id, movie);

            return movie;
        }

        public async Task<Movie> GetMovie(long id)
        {
            return await Task.FromResult(_storage.Get(id));
        }

        private Dictionary<string, WordEntry> GetWordEntries(SubtitleItem[] subs)
        {
            var dict = new Dictionary<string, WordEntry>();
            foreach (var sub in subs)
            {
                if (sub == null) continue;
                
                var cleanStr = Regex.Replace(sub.Text, "[^A-Za-z0-9 -']", "")
                    
                    // TODO REMOVE TAGS
                    
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
        Task<Movie> CreateMovie(string rawContent);

        Task<Movie> GetMovie(long id);
    }
}