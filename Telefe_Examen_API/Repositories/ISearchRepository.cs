using System;
using System.Collections.Generic;
using System.Data;
using Entities;

namespace Repositories
{
	public interface ISearchRepository
	{
		IEnumerable<SearchRecord> GetRecords(IDbTransaction transaction = null);

		int CreateRecord(string search, bool result, DateTime timestamp, IDbTransaction transaction = null);
	}
}
