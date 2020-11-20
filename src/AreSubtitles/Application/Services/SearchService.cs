using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Services.Contract;
using Nest;

namespace Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly ElasticClient _client;

        public SearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")
            ).DefaultIndex("films");
            _client = new ElasticClient(settings);
        }

        public async Task<IReadOnlyCollection<FilmSearchDto>> Find(string query, int from = 0, int size = 20)
        {
            var searchResponse = await _client.SearchAsync<FilmSearchDto>(s => s
                .From(from)
                .Size(size)
                .Query(q =>
                    q.Match(m => m
                        .Field(f => f.Id)
                        .Query(query))
                    ||
                    q.Match(m => m
                        .Field(f => f.Name)
                        .Query(query))

                )

            );

            return searchResponse.Documents;
        }
        
        public async Task<IndexResponse> Index(FilmSearchDto filmSearchDto)
        {
            return await _client.IndexDocumentAsync(filmSearchDto);
        }
    }
}