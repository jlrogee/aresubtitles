using Domain.Entities;

namespace Domain.Persistence.Contract
{
    public interface IStorage
    {
        void Put(string id, FilmDocument filmDocument);
        FilmDocument Get(long id);
        bool GetIdIfExists(long hash, out long id);
    }
}