using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace DapperSample
{
    public class SQLiteDataProvider
    {
	    private SqliteConnection _connection;

	    public SQLiteDataProvider(string connectionString)
	    {
		    _connection = new SqliteConnection(connectionString);
	    }


	    public void CrateTable()
	    {
			_connection.Open();
		    _connection.Execute("CREATE TABLE IF NOT EXISTS Foo(Id TEXT PRIMARY KEY NOT NULL, Name TEXT)");
			_connection.Close();
	    }

	    public IEnumerable<Foo> GetAll()
	    {
		    _connection.Open();
		    try
		    {
			    var values = _connection.Query<Foo>("SELECT Id, Name FROM Foo");
			    return values;
		    }
		    catch (SqliteException ex)
		    {
			    throw;
		    }
		    finally
		    {
			    _connection.Close();
		    }
	    }

	    public void Save(Foo foo)
	    {
		    _connection.Open();
		    try
		    {
			    _connection.Execute($"INSERT INTO Foo(Id, Name) VALUES('{foo.Id}', '{foo.Name}')");
		    }
		    catch (SqliteException ex)
		    {
			    throw;
		    }
		    finally
		    {
				_connection.Close();
			}
		    
			
	    }
    }
}
