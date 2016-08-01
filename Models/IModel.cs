using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample.Models
{
    public interface IModel
    {
		Guid Id { get; set; }
		string Name { get; set; }
    }
}
