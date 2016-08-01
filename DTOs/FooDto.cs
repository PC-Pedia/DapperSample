using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.DTOs
{
    public class FooDto
    {
		public Guid FooId { get; set; }
		public string FooName { get; set; }
		public Guid BarId { get; set; }
		public string BarName { get; set; }
    }
}
