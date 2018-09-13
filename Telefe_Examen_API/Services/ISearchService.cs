using System;
using System.Collections.Generic;
using Entities;

namespace Services
{
	public interface ISearchService
	{
		//ISearchRepository _repository { get; set; }

		int[,] GetCoordinates(string[] sequence, string word);

		IEnumerable<SearchRecord> GetRecords();

		int CreateRecord(string search, bool result, DateTime timestamp);
	}
}
