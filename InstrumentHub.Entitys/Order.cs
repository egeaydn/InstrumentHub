using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	[Table("OrderTable")]
	public class Order
	{
		public int Id { get; set; }
		public string OrderNumber { get; set; }
		public DateTime Orderdate { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string Lastname { get; set; }
		public string Adress { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public string Email{ get; set; }
		public string OrderNote{ get; set; }
		public string PaymentId { get; set; }
		public string PaymentToken { get; set; }
		public string ConversionId { get; set; }
		public OrderStatus OrderEnums { get; set; }
		public PaymentMethod PaymentEnum { get; set; }
		public List<OrderItem> OrderItems { get; set; }
		public Order()
		{
			OrderItems = new List<OrderItem>();
		}
	}
	public enum OrderStatus
	{
		waiting = 0,
		unpaind = 1,
		completed = 2,
		canceled = 3
	}
	public enum PaymentMethod
	{
		CreditCard = 0,
		Eft = 1,
		CashOnDelivery = 2
	}
}
