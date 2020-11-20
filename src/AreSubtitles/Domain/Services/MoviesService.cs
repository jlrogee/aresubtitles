using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Parsers;
using Domain.Persistence.Contract;
using Domain.Services.Contract;
using Domain.Utils;

namespace Domain.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ISrtParser _srtParser;
        private readonly IPhraseSplitter _phraseSplitter;
        private readonly IFilmRepository _storage;

        public MoviesService(
            ISrtParser srtParser,
            IPhraseSplitter phraseSplitter,
            IFilmRepository storage)
        {
            _srtParser = srtParser;
            _phraseSplitter = phraseSplitter;
            _storage = storage;
        }

        public async Task<FilmDocument> CreateMovie(string rawContent)
        {
            var existed = await _storage.GetIdByHashcode(rawContent.GetHashCode());
            if (existed != null)
                return existed;
            
            var subs = _srtParser.Parse(rawContent).ToArray();
            var words = GetWordEntries(subs);
            
            var movie = new FilmDocument
            {
                Id = IdGenerator.GetNewId(),
                Hashcode = rawContent.GetHashCode(),
                Subtitles = subs,
            };
            
            movie.SetWords(words);
            
            await _storage.Insert(movie);

            return movie;
        }

        public async Task<FilmDocument> GetMovie(string id)
        {
            return await _storage.ById(id);
        }

        private Dictionary<string, WordEntry> GetWordEntries(SubtitleItemEmbedDocument[] subs)
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
}