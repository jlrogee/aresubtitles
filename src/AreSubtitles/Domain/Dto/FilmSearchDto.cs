using Domain.Entities;

namespace Domain.Dto
{
    public class FilmSearchDto
    {
        public FilmSearchDto(FilmDocument film)
        {
            Id = film.Id;
            Name = film.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}