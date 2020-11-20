using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Parsers;
using Domain.Persistence.Contract;
using Domain.Services.Contract;

namespace Domain.Services
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

        public async Task<FilmDocument> CreateMovie(string rawContent)
        {
            if (_storage.GetIdIfExists(rawContent.GetHashCode(), out long foundId))
                return await Task.FromResult(_storage.Get(foundId));
            
            var subs = _srtParser.Parse(rawContent).ToArray();
            var words = GetWordEntries(subs);
            
            var movie = new FilmDocument
            {
                Id = IdGenerator.GetNewId(),
                Hashcode = rawContent.GetHashCode(),
                Subtitles = subs,
            };
            
            movie.SetWords(words);
            
            _storage.Put(movie.Id, movie);

            return movie;
        }

        public async Task<FilmDocument> GetMovie(long id)
        {
            return await Task.FromResult(_storage.Get(id));
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