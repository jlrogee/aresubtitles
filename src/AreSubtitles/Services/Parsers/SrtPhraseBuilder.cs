using System;
using System.Linq;
using src.Entities;

namespace src.Services.Parsers
{
    public class SrtSubtitleBuilder : ISrtSubtitleBuilder
    {
        private readonly string[] _timeSeparators = { "-->", "- >", "->" };

        private readonly IPhraseSplitter _phraseSplitter;
        
        public SrtSubtitleBuilder(IPhraseSplitter phraseSplitter)
        {
            _phraseSplitter = phraseSplitter;
        }
        
        public SubtitleItem Build(string rawPhrase)
        {
            var sub = new SubtitleItem();

            var src = rawPhrase.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            if (src.Count() < 3)
                return null;

            if (!int.TryParse(src[0], out int num))
                return null;

            sub.Num = num;

            if (!SetTime(src[1], ref sub))
                return null;

            sub.Text = string.Join(" ", src.Skip(2));
            sub.Words = _phraseSplitter.Split(sub.Text);

            return sub;
        }

        private bool SetTime(string timeStr, ref SubtitleItem sub)
        {
            var timeArr = timeStr.Split(_timeSeparators, StringSplitOptions.RemoveEmptyEntries);

            if (timeArr.Length != 2)
                return false;

            sub.Start = ParseTimeCode(timeArr[0]);
            if (sub.Start == -1)
                return false;

            sub.End = ParseTimeCode(timeArr[1]);
            return sub.End != 1;
        }

        private static int ParseTimeCode(string src)
        {
            return TimeSpan.TryParse(src.Replace(',', '.'), out TimeSpan res) 
                ? (int)res.TotalMilliseconds
                : -1;
        }
    }
    
    public interface ISrtSubtitleBuilder
    {
        SubtitleItem Build(string rawPhrase);
    }
}