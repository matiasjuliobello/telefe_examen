using System.Data;
using System.Data.SqlClient;

namespace Repositories
{
	public class BaseRepository
	{
		protected IDbConnection _connection { get; }

		protected BaseRepository(string connString)
		{
			this._connection = new SqlConnection(connString);
		}

		protected string GetIdentitySql()
		{
			return "SELECT CAST(SCOPE_IDENTITY() AS INT);";
		}
	}
}
