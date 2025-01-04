using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;

namespace Instrument.Business.Abstract
{
	public interface ICartServices 
	{
		void InitialCart(string userId);
		Cart GetCartByUserId(string userId);
		void AddToCart(string userId, int productId, int quantity);
		void DeleteFromCart(string userId, int productId);
		void ClearCart(string cartId);
	}
}
