using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	public class EProduct
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Image> Images { get; set; }
		public List<ProductDivision> ProductDivisions { get; set; }
		public List<Comment> Comments { get; set; }
		public string Brand { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Fiyat geçerli bir değer olmalıdır. Lütfen pozitif bir sayı giriniz.")]
		public decimal Price { get; set; }

		public EProduct()
		{
			Images = new List<Image>();
			ProductDivisions = new List<ProductDivision>();
			Comments = new List<Comment>();
		}
	}
	
}
