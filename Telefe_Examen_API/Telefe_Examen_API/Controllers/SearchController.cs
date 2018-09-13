using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services;

namespace Telefe_Examen_API.Controllers
{
	[RoutePrefix("api/search")]
	public class SearchController : ApiController
	{
		private static readonly string[] _sequence = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };

		private static readonly string _connString = ConfigurationManager.AppSettings["connString"];

		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Include,
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		public IHttpActionResult Index()
		{
			return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.NoContent));
		}
		[Route("{word}")]
		[HttpGet]
		public IHttpActionResult Get(string word)
		{
			var searchService = new SearchService(_connString);

			int[,] coordenates = searchService.GetCoordenates(_sequence, word);

			int record_id = searchService.CreateRecord(word, coordenates.Length > 0, DateTime.Now);

			return new JsonResult<int[,]>(coordenates, _serializerSettings, Encoding.UTF8, this);
		}

		[Route("records")]
		[HttpGet]
		public IHttpActionResult GetRecords()
		{
			var searchService = new SearchService(_connString);

			IEnumerable<SearchRecord> records = searchService.GetRecords();

			return new JsonResult<IEnumerable<SearchRecord>>(records, _serializerSettings, Encoding.UTF8, this);
		}
	}
}
