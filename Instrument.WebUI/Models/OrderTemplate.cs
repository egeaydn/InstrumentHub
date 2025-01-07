namespace Instrument.WebUI.Models
{
	public class OrderTemplate
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Adress { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string? CardName { get; set; }
		public string? CardNumber { get; set; }
		public string? ExparationMonth { get; set; }
		public string? ExparationYear { get; set; }
		public string? CVV { get; set; }
		public string OrderNote { get; set; }
		public CartTemplate CartTemplate { get; set; }
	}
}
