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
	public class CartManager : ICartServices
	{
		private ICartDal _cartDal;

		public CartManager(ICartDal cartDal)
		{
			_cartDal = cartDal;
		}
		public void AddToCart(string userId, int productId, int quantity)
		{
			var cart = GetCartByUserId(userId);

			if (cart is not null)
			{
				var index = cart.CartItems.FindIndex(x => x.EProductId == productId);

				if (index < 0)
				{
					cart.CartItems.Add(
						new CartItem()
						{
							EProductId = productId,
							Quantity = quantity,
							CartId = cart.Id
						}
					);
				}
				else
				{
					cart.CartItems[index].Quantity += quantity;
				}
			}

			_cartDal.Update(cart); // Dataaccess aracılığıyla sepeti günceller.
		}

		public void ClearCart(string cartId)
		{
			_cartDal.ClearCart(cartId);
		}

		public void DeleteFromCart(string userId, int productId)
		{
			var cart = GetCartByUserId(userId);

			if (cart != null)
			{
				_cartDal.DeleteCart(cart.Id, productId);
			}
		}

		public Cart GetCartByUserId(string userId)
		{
			return _cartDal.CartByUserId(userId);
		}

		public void InitialCart(string userId)
		{
			Cart cart = new Cart()
			{
				UserId = userId,
			};
			_cartDal.Create(cart);
		}
	}
}
