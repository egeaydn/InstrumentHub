using InstrumentHub.Entites;

namespace Instrument.WebUI.Models
{
	public class OrderListeModel
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
		public List<OrderItemCartItemModel> OrderItems { get; set; }

		public decimal TotalPrice()
		{
				return OrderItems.Sum(x => x.Price * x.Quantity);
		}
	}

	public class OrderItemCartItemModel
	{
		public int OrderItemId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
	}
}
