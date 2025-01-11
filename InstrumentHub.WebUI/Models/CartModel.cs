namespace Instrument.WebUI.Models
{
	public class CartModel
	{
		public int CartId { get; set; }
		public List<CartItemModel> CartItems { get; set; }

		public Decimal TotalPrice()
		{
			return CartItems.Sum(i => i.Price * i.Quantity);
		}
	}

	public class CartItemModel
	{
		public int CartItemId { get; set; }
		public int Quantity { get; set; }
		public Decimal Price { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Image { get; set; }
	}
}
