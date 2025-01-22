using System.ComponentModel.DataAnnotations;
using InstrumentHub.Entites;

namespace Instrument.WebUI.Models
{
	public class EProductModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(40,MinimumLength = 5, ErrorMessage = "Ürün adı, asgari 5 ve azami 40 karakter arasında olmalıdır.")]
		public string Name { get; set; }
		[Required]
		[StringLength(500, MinimumLength = 5, ErrorMessage = "Ürün açıklaması, en az 5 ve en fazla 500 karakter arasında olmalıdır")]
		public string Description { get; set; }
		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Fiyat geçerli bir değer olmalıdır. Lütfen pozitif bir sayı giriniz.")]
		public decimal Price { get; set; }
		public List<Image> Image { get; set; }
		[Required]
		public string Brand { get; set; }
		public List<Division> SelectedDivision { get; set; }
		public string DivisionId { get; set; }

		public EProductModel()
		{
			Image = new List<Image>();
		}
	}
}
