using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DapperSample.Models;
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


		public void CreateTable()
		{
			_connection.Open();
			_connection.Execute("CREATE TABLE IF NOT EXISTS Foo(Id TEXT PRIMARY KEY NOT NULL, BarId TEXT, Name TEXT)");
			_connection.Execute("CREATE TABLE IF NOT EXISTS Bar(Id TEXT PRIMARY KEY NOT NULL, Name TEXT)");
			_connection.Close();
		}



		public IEnumerable<T> GetAll<T>() where T : IModel, new()
		{
			_connection.Open();
			try
			{
				var tableName = typeof(T).Name;
				var values = _connection.Query($"SELECT Id, Name FROM {tableName}")
				                        .Select(x => new T
				                                     {
					                                     Id = new Guid(x.Id),
					                                     Name = x.Name
				                                     });

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

		public void Save<T>(T foo) where T : IModel
		{
			_connection.Open();
			try
			{
				var tableName = typeof(T).Name;
				_connection.Execute($"INSERT INTO {tableName}(Id, Name) VALUES('{foo.Id}', '{foo.Name}')");
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
