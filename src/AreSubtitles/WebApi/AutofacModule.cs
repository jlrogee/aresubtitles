using Application.Persistence.Implementations;
using Application.Services;
using Autofac;
using Domain.Parsers;
using Domain.Persistence.Contract;
using Domain.Services;
using Domain.Services.Contract;
using Infrastructure.Db.Repository;
using Sourcing;
using Module = Autofac.Module;

namespace src
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ISrtParser, SrtParser>();
            builder.Register<IPhraseSplitter, PhraseSplitter>();
            builder.Register<IMoviesService, MoviesService>();
            builder.Register<ISrtSubtitleBuilder, SrtSubtitleBuilder>();
            builder.Register<IFilmRepository, FilmRepository>();
            builder.Register<IOpenSubtitleService, OpenSubtitlesService>();
            builder.Register<ISearchService, SearchService>();
            
            builder.Register<IDbContext, DbContext>();
            
            builder.RegisterType<OpenSubtitleClient>();
            
            // TODO: register all implementations
//            var assemblies = Assembly.GetExecutingAssembly();
//            builder.RegisterAssemblyTypes(assemblies)
//                .Where(t => typeof(IHttpRequest).IsAssignableFrom(t))
//                .AsImplementedInterfaces();
        }
    }
    
    public static class ContainerBuilderExtension
    {
        public static void Register<T, TImpl>(this ContainerBuilder builder)
        {
            builder.RegisterType<TImpl>().As<T>();
        }
    }
}