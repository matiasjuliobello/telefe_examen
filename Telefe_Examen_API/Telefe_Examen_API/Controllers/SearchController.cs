using System;
using System.Collections.Generic;
using System.Configuration;
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
		private readonly ISearchService _searchService;

		public SearchController(ISearchService searchService)
		{
			this._searchService = searchService;
		}

		private static readonly string[] _sequence = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };

		private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Include,
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		[Route("")]
		[HttpGet]
		public IHttpActionResult GetCoordinates([FromUri] string word = null)
		{
			if (string.IsNullOrEmpty(word))
				return BadRequest();

			int[,] coordenates = _searchService.GetCoordinates(_sequence, word);

			int record_id = _searchService.CreateRecord(word, coordenates.Length > 0, DateTime.Now);

			return new JsonResult<int[,]>(coordenates, _serializerSettings, Encoding.UTF8, this);
		}

		[Route("records")]
		[HttpGet]
		public IHttpActionResult GetRecords()
		{
			IEnumerable<SearchRecord> records = _searchService.GetRecords();

			return new JsonResult<IEnumerable<SearchRecord>>(records, _serializerSettings, Encoding.UTF8, this);
		}
	}
}
