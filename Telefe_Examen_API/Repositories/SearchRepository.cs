using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Entities;

namespace Repositories
{
    public class SearchRepository : BaseRepository, ISearchRepository
	{
	    public SearchRepository(string connString) : base(connString)
	    {
	    }

		public IEnumerable<SearchRecord> GetRecords(IDbTransaction transaction = null)
	    {
		    DynamicParameters parameters = null;

			var sql = new StringBuilder();
		    sql.AppendLine("SELECT");
		    sql.AppendLine("	Id [Id],");
		    sql.AppendLine("	Search [Search],");
		    sql.AppendLine("	Result [Result],");
		    sql.AppendLine("	Timestamp [Timestamp]");
		    sql.AppendLine("FROM [dbo].[Search_Record]");

			var searchRecords = _connection.Query<SearchRecord>(sql.ToString(), parameters, transaction: transaction);

		    return searchRecords;
	    }

		public int CreateRecord(string search, bool result, DateTime timestamp, IDbTransaction transaction = null)
	    {
		    var parameters = new DynamicParameters();
				parameters.Add("search", search);
				parameters.Add("result", result);
				parameters.Add("timestamp", timestamp);

		    var sql = new StringBuilder();
				sql.AppendLine("INSERT INTO [dbo].[Search_Record]");
				sql.AppendLine("	( Search, Result, Timestamp )");
				sql.AppendLine("VALUES");
				sql.AppendLine("	( @search, @result, @timestamp );");
				sql.AppendLine(base.GetIdentitySql());

			int id = _connection.Query<int>(sql.ToString(), parameters, transaction: transaction).Single();

		    return id;
		}
    }
}
