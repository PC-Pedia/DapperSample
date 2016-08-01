using System;

namespace DapperSample.Models
{
	public class Bar : IModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

	}
}
