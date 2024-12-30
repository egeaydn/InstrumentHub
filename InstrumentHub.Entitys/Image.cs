using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	[Table("ImagesTable")]
	public class Image
	{
		public int Id { get; set; }
		public string? ImageUrl { get; set; }
		public string EProductId { get; set; }
		public EProduct EProduct { get; set; }
	}
}
