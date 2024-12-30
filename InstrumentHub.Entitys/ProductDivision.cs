using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	public class ProductDivision
	{
		public string DivisionId { get; set; }
		public Division Division { get; set; }
		public int EProductId { get; set; }
		public EProduct EProduct { get; set; }
	}
}
