using InstrumentHub.Entites;

namespace Instrument.WebUI.Models
{
	public class EProductListTemplate
	{
		public PageInformation PageInformation { get; set; }
		public List<EProduct> EProducts { get; set; }
		public List<Image> Images { get; set; }

		public EProductListTemplate()
		{
			Images = new List<Image>();
		}
	}

	public class PageInformation
	{
		public int ToatalItems { get; set; }
		public int ItemsPerPage { get; set; }
		public int CurrentPage { get; set; }
		public int CurrentDivision { get; set; }
		public int TotalPages()
		{
			return (int)Math.Ceiling((decimal)ToatalItems / ItemsPerPage);
		}
	}
}
