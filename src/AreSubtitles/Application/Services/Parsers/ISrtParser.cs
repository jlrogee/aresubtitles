using System.Collections.Generic;
using Domain.Entities;

namespace Application.Services.Parsers
{
    public interface ISrtParser
    {
        IEnumerable<SubtitleItem> Parse(string src);
    }
}