using System.Collections.Generic;
using System.Linq;

namespace src.Entities
{
    public class WordEntry
    {
        public HashSet<int> FromSubs { get; }
        public int Count { get; private set; } = 1;
        public string Word { get; }

        public WordEntry(string word)
        {
            FromSubs = new HashSet<int>();
            Word = word;
        }

        public void Increment() => Count++;
        public void FoundInSub(int subNum) => FromSubs.Add(subNum);

        public override string ToString()
            => $"Entries {Count} in subs: {string.Join(", ", FromSubs.Select(x => x))}";
    }
}