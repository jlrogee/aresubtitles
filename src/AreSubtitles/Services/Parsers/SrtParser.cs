using System;
using System.Collections.Generic;
using System.Text;
using src.Entities;

namespace src.Services.Parsers
{
    public class SrtParser : ISrtParser
    {
        private readonly IPhraseSplitter _phraseSplitter;
        private readonly ISrtSubtitleBuilder _srtSubtitleBuilder;

        public SrtParser(
            IPhraseSplitter phraseSplitter, ISrtSubtitleBuilder srtSubtitleBuilder)
        {
            _phraseSplitter = phraseSplitter;
            _srtSubtitleBuilder = srtSubtitleBuilder;
        }

        public IEnumerable<SubtitleItem> Parse(string src)
            => GetRawSubtitles(src);

        private IEnumerable<SubtitleItem> GetRawSubtitles(string src)
        {
            var subPhrase = new StringBuilder();

            foreach (var str in src.Split(Environment.NewLine))
            {
                if (str.Trim().Length != 0)
                    subPhrase.Append($"{str.Trim()}{Environment.NewLine}");
                else
                {
                    var subtitle = _srtSubtitleBuilder.Build(subPhrase.ToString());
                    if (subtitle != null)
                        yield return subtitle;
                    
                    subPhrase.Clear();
                }
            }

            subPhrase.Clear();
        }
    }
}
 