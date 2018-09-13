using System.Web.Http;

namespace Telefe_Examen_API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{word}",
				defaults: new { word = RouteParameter.Optional }
			);
		}
	}
}
