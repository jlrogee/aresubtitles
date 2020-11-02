using Application.Services;
using Application.Services.Parsers;
using Application.Storage;
using Autofac;
using Sourcing;
using src.Services.Parsers;
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
            builder.Register<IStorage, InMemoryStorage>();
            builder.Register<IOpenSubtitleService, OpenSubtitlesService>();
            
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