using InstrumentHub.Entites;

namespace Instrument.WebUI.Models
{
	public class DivisionTemplate
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<EProduct> EProducts { get; set; }
	}
}
