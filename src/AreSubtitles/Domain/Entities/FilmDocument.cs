using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonCollection("films")]
    public class FilmDocument : IDocument
    {
        private Dictionary<string, WordEntry> _wordEntriesDictionary;
        
        public string Id { get; set; }
        public string Name { get; set; }
        public long Hashcode { get; set; }

        public SubtitleItemEmbedDocument [] Subtitles { get; set; }
        
        [BsonIgnore]
        public WordEntry[] Words => _wordEntriesDictionary?.Values.ToArray();

        public void SetWords(Dictionary<string, WordEntry> wordEntries)
        {
            _wordEntriesDictionary = wordEntries;
        }
    }
}