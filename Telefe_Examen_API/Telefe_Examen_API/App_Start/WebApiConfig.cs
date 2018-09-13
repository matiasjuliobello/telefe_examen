using System.Configuration;
using System.Web.Http;
using Autofac;
using Services;
using System.Reflection;
using Autofac.Integration.WebApi;
using Repositories;

namespace Telefe_Examen_API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			string connString = ConfigurationManager.AppSettings["connString"];

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}"
			);

			// Dependency Resolver (Autofac) Configuration
			var builder = new ContainerBuilder();
				builder.RegisterInstance(new SearchRepository(connString)).As<ISearchRepository>();
				builder.RegisterType<SearchService>().As<ISearchService>().InstancePerRequest();

			var executingAssembly = Assembly.GetExecutingAssembly();

				builder.RegisterApiControllers(executingAssembly);

			IContainer container = builder.Build();
			var resolver = new AutofacWebApiDependencyResolver(container);
			config.DependencyResolver = resolver;
		}

	}
}
