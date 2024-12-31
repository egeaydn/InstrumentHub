using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.Entites
{
	[Table("CartTable")]
	public class Cart
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public List <CartItem> CartItems { get; set; }
	}

	public class CartItem
	{
		public int Id { get; set; }
		public int EProductId { get; set; }
		public EProduct EProduct { get; set; }
		public Cart Cart { get; set; }
		public int CartId { get; set; }
		public int Quantity { get; set; }

}
}
