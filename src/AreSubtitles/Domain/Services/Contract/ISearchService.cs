using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dto;
using Nest;

namespace Domain.Services.Contract
{
    public interface ISearchService
    {
        public Task<IReadOnlyCollection<FilmSearchDto>> Find(string query, int from = 0, int size = 20);

        public Task<IndexResponse> Index(FilmSearchDto filmSearchDto);
    }
}