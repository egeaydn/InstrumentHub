using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace Instrument.Business.Abstract
{
	public interface IOrderServices
	{
		List<Order> GetOrders(string userId);
		void Create(Order entity);

	}
}
