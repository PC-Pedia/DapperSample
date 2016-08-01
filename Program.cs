using System;
using System.Linq;
using DapperSample.DTOs;
using DapperSample.Models;

namespace DapperSample
{
    public class Program
    {
	    private static SQLiteDataProvider _connection;

	    public static void Main(string[] args)
        {
			_connection = new SQLiteDataProvider("Data Source=Sample.db");
		    _connection.CreateTable();

		    string option = string.Empty;
		    do
		    {
			    Menu();
			    option = Console.ReadLine();
			    Execute(option);

		    } while (!option.Equals("exit"));
        }

	    

	    private static void Menu()
	    {
		    Console.WriteLine("1. Get all Foo values");
		    Console.WriteLine("2. Get all Bar value");
			Console.WriteLine("3. Get all Foo values with Bar relations");
			Console.WriteLine("4. Add new Foo value (random)");
		    Console.WriteLine("5. Add new Bar value (random");
			Console.WriteLine("6. Add a Bar with a Foo relation (add a new Foo and Bar");
			

		    Console.Write("\n(type exit to finish)\n=> ");
	    }

		private static void Execute(string option)
		{
			int optionNumber;
			if (int.TryParse(option, out optionNumber))
			{
				switch (optionNumber)
				{
					case 1:
						GetAll<Foo>();
						break;
					case 2:
						GetAll<Bar>();
						break;
					case 3:
						GetFooWithRelations();
						break;
					case 4:
						Save<Foo>();
						break;
					case 5:
						Save<Bar>();
						break;
					case 6:
						SaveFooWithRelation();
						break;

				}
			}
		}

	    private static void GetAll<T>() where T:IModel, new()
	    {
		    var values = _connection.GetAll<T>();

			if(!values.Any())
				Console.WriteLine("No values");

		    foreach (var value in values)
		    {
				Console.WriteLine($"[{value.Id}]:: {value.Name}");
			}

		    Console.WriteLine();   
	    }

		private static void GetFooWithRelations()
		{
			var values = _connection.GetFooDto();

			if (!values.Any())
				Console.WriteLine("No values");

			foreach (var value in values)
			{
				Console.WriteLine($"[{value.FooId}] :: {value.FooName} :: {value.BarName}");
			}

			Console.WriteLine();
		}

		private static void Save<T>() where T: IModel, new()
		{
			var value = new T()
			          {
				          Id = Guid.NewGuid(),
				          Name = DateTime.UtcNow.ToString("G")
			          };

			_connection.Save(value);

			Console.WriteLine($"Value saved: {value.Id}");
			Console.WriteLine();
		}

		private static void SaveFooWithRelation()
		{
			var value = new FooDto
			            {
				            FooId = Guid.NewGuid(),
				            FooName = $"[F] - {DateTime.UtcNow.ToString("G")}",
				            BarId = Guid.NewGuid(),
				            BarName = $"[B] - {DateTime.UtcNow.ToString("G")}"
			            };

			_connection.SaveFooDto(value);

			Console.WriteLine($"Value saved: [F]:{value.FooId} [B]:{value.BarId}");
			Console.WriteLine();
		}
	}
}
