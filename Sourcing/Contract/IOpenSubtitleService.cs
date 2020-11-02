using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Sourcing.Http.OpenSubtitles;
using Domain.Entities;

namespace Sourcing
{
    public interface IOpenSubtitleService
    {
        Task<List<SubtitlesByImdbIdDtoShort>> GetByImdbIdRaw(string imdbId, Language language = Language.Eng);
    }
}