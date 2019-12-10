using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class WordEntry
    {
        public string Word { get; }
        public int CountInMovie { get; private set; } = 1;
        public HashSet<int> SubtitlesNo { get; }

        public WordEntry(string word)
        {
            SubtitlesNo = new HashSet<int>();
            Word = word;
        }

        public void Increment() => CountInMovie++;
        public void FoundInSub(int subNum) => SubtitlesNo.Add(subNum);

        public override string ToString()
            => $"Entries {CountInMovie} in subs: {string.Join(", ", SubtitlesNo.Select(x => x))}";
    }
}