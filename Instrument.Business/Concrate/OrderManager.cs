using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instrument.Business.Abstract;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.Entites;

namespace Instrument.Business.Concrate
{
	public class OrderManager : IOrderServices
	{
		private IOrderDal _orderDal;

		public OrderManager(IOrderDal orderDal)
		{
			_orderDal = orderDal;
		}
		public void Create(Order entity)
		{
			_orderDal.Create(entity);
		}

		public List<Order> GetOrders(string userId)
		{
			return _orderDal.GetOrders(userId);
		}
	}
}
