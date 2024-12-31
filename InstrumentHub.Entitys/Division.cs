using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	[Table("DivisionTable")]
	public class Division
	{
		public int Id { get; set; }
		public string CategoryName { get; set; }
		public List<ProductDivision> ProductDivisions { get; set; }

		public Division()
		{
			ProductDivisions = new List<ProductDivision>();
		}
	}
}
