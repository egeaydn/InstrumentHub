using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface IOrderDal : IRepository<Order>
	{
		List<Order> GetOrders(string userId);
	}
}
