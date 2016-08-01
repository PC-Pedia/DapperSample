using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DapperSample.DTOs;
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
			try
			{
				_connection.Open();

				var tableName = typeof(T).Name;
				var sql = $"SELECT Id, Name FROM {tableName}";

				var values = _connection.Query(sql)
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

		public IEnumerable<FooDto> GetFooDto()
		{
			try
			{
				_connection.Open();

				var sql = @"SELECT f.Id Id, f.Name Name, b.Name bName FROM Foo f INNER JOIN Bar b ON b.Id = f.BarId";

				var values = _connection.Query(sql).Select(x => new FooDto
				                                   {
					                                   FooId = new Guid(x.Id),
					                                   FooName = x.Name,
					                                   BarName = x.bName
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
			try
			{
				_connection.Open();
				
				var tableName = typeof(T).Name;
				var sql = $"INSERT INTO {tableName}(Id, Name) VALUES('{foo.Id}', '{foo.Name}')";

				_connection.Execute(sql);
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

		public void SaveFooDto(FooDto dto)
		{
			try
			{
				_connection.Open();

				var sql =$@"
					INSERT INTO Bar(Id, Name) VALUES('{dto.BarId}', '{dto.BarName}');

					INSERT INTO Foo(Id, Name, BarId) VALUES('{dto.FooId}', '{dto.FooName}', '{dto.BarId}')";

				_connection.Execute(sql);
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
