using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace InstrumentHub.DataAccess.Abstract
{
	public interface ICartDal : IRepository<Cart>
	{
		void DeleteCart (int cartId, int eproductId);
		void ClearCart (string cartId);
		Cart CartByUserId (string userId);
	}
}
