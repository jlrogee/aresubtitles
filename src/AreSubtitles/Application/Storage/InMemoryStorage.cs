using System.Collections.Concurrent;
using System.Threading;
using Domain.Entities;

namespace src.Storage
{
    public class InMemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, long> _hashesPerIds = new ConcurrentDictionary<long, long>();
        private ConcurrentDictionary<long, Movie> Movies { get; } = new ConcurrentDictionary<long, Movie>();

        public void Put(long id, Movie movie)
        {
            Movies.TryAdd(id, movie);
            _hashesPerIds.TryAdd(movie.Hashcode, id);
        }

        public Movie Get(long id)
        {
            return Movies.TryGetValue(id, out var movie) ? movie : null;
        }

        public bool GetIdIfExists(long hash, out long id)
            => _hashesPerIds.TryGetValue(hash, out id);
    }

    public interface IStorage
    {
        void Put(long id, Movie movie);
        Movie Get(long id);
        bool GetIdIfExists(long hash, out long id);
    }
    
    public static class IdGenerator
    {
        private static long _counter;

        public static long GetNewId() => Interlocked.Increment(ref _counter);
    }
}