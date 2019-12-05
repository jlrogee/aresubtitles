using System.Collections.Concurrent;
using System.Threading;
using src.Entities;

namespace src.Storage
{
    public class InMemoryStorage : IStorage
    {
        public ConcurrentDictionary<long, Movie> Movies { get; } = new ConcurrentDictionary<long, Movie>();

        public void Put(long id, Movie movie)
        {
            Movies.TryAdd(id, movie);
        }

        public Movie Get(long id)
        {
            return Movies.TryGetValue(id, out var movie) ? movie : null;
        }
    }

    public interface IStorage
    {
        void Put(long id, Movie movie);
        Movie Get(long id);
    }
    
    public class IdGenerator
    {
        private static long _counter;

        public static long GetNewId() => Interlocked.Increment(ref _counter);
    }
}