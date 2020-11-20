using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Parsers
{
    public interface ISrtParser
    {
        IEnumerable<SubtitleItemEmbedDocument> Parse(string src);
    }
}