using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services;

namespace Telefe_Examen_API.Controllers
{
	public class SearchController : ApiController
	{
		private static readonly string[] _sequence = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };

		public IHttpActionResult Index()
		{
			return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.NoContent));
		}

		// GET api/search/{word}
		public IHttpActionResult Get(string word)
		{
			var searchService = new SearchService();

			int[,] coordenates = searchService.GetCoordenates(_sequence, word);

			var serializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Include,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			return new JsonResult<int[,]>(coordenates, serializerSettings, Encoding.UTF8, this);
		}
	}
}
