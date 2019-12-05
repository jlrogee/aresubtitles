using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using src.Entities;

namespace src.Services.Parsers
{
    public class SrtParser : ISrtParser
    {
        private readonly string[] _timeSeparators = { "-->", "- >", "->" };

        public IEnumerable<SubtitleItem> Parse(string src)
            => GetRawSubtitles(src).Select(DoSubtitle);

        private IEnumerable<string> GetRawSubtitles(string src)
        {
            var rawSubs = new List<string>();

            var sub = new StringBuilder();

            foreach (var str in src.Split(Environment.NewLine, StringSplitOptions.None))
            {
                if (str.Trim().Length != 0)
                    sub.Append($"{str.Trim()}{Environment.NewLine}");
                else
                {
                    rawSubs.Add(sub.ToString());
                    sub.Clear();
                }
            }

            sub.Clear();

            return rawSubs;
        }

        private SubtitleItem DoSubtitle(string raw)
        {
            var sub = new SubtitleItem();

            var src = raw.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            if (src.Count() < 3)
                return null;

            if (!int.TryParse(src[0], out int num))
                return null;

            sub.Num = num;

            if (!SetTime(src[1], ref sub))
                return null;

            sub.Text = string.Join(" ", src.Skip(2));

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
}
 