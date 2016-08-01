using System;

namespace DapperSample.Models
{
	public class Foo : IModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

	}
}
