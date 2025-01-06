namespace Instrument.WebUI.Models
{
	public class CartTemplate
	{
		public int CartId { get; set; }
		public List<CartItemTemplate> CartItems { get; set; }

		public Decimal TotalPrice()
		{
			return CartItems.Sum(i => i.Price * i.Quantity);
		}
	}

	public class CartItemTemplate
	{
		public int CartItemId { get; set; }
		public int Quantity { get; set; }
		public Decimal Price { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Image { get; set; }
	}
}
