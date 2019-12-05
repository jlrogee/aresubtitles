using System.Collections.Generic;
using System.Linq;

namespace src.Entities
{
    public class Movie
    {
        private Dictionary<string, WordEntry> _wordEntriesDictionary;
        
        public long Id { get; set; }
        public long Hashcode { get; set; }
        public SubtitleItem [] Subtitles { get; set; }
        public WordEntry[] Words => _wordEntriesDictionary?.Values.ToArray();

        public void SetWords(Dictionary<string, WordEntry> wordEntries)
        {
            _wordEntriesDictionary = wordEntries;
        }
    }
}