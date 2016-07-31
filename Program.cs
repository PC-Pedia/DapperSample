using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DapperSample
{
    public class Program
    {
	    private static SQLiteDataProvider _connection;

	    public static void Main(string[] args)
        {
			_connection = new SQLiteDataProvider("Data Source=Sample.db");
		    _connection.CrateTable();

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
		    Console.WriteLine("2. Add new Foo value (random)");
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
						GetAll();
						break;
					case 2:
						Save();
						break;

				}
			}
		}

	    private static void GetAll()
	    {
		    var foos = _connection.GetAll();

			if(!foos.Any())
				Console.WriteLine("No values");

		    foreach (var foo in foos)
		    {
				Console.WriteLine($"[{foo.Id}]:: {foo.Name}");
			}

		    Console.WriteLine();   
	    }

		private static void Save()
		{
			var foo = new Foo
			          {
				          Id = Guid.NewGuid().ToString(),
				          Name = DateTime.UtcNow.ToString("G")
			          };

			_connection.Save(foo);

			Console.WriteLine($"Value saved: {foo.Id}");
			Console.WriteLine();
		}
	}
}
