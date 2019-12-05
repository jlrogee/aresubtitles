using System.Collections.Generic;
using src.Entities;

namespace src.Services.Parsers
{
    public interface ISrtParser
    {
        IEnumerable<SubtitleItem> Parse(string src);
    }
}