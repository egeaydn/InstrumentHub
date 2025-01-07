using InstrumentHub.Entites;

namespace Instrument.WebUI.Models
{
	public class OrderListeTemplates
	{
		public int id { get; set; }
		public string OrderNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public OrderStatus OrderState { get; set; }
		public PaymentMethod PaymentTypes { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Adress { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string OrderNote { get; set; }
		public List<OrderItemTemplate> OrderItems { get; set; }

		public decimal TotalPrice()
		{
				return OrderItems.Sum(x => x.Price * x.Quantity);
		}
	}

	public class OrderItemTemplate
	{
		public int OrderItemId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
	}
}
