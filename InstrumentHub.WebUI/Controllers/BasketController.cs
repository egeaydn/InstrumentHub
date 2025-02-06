using DocuSign.eSign.Model;
using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class BasketController : Controller
	{
		private ICartServices _cartService;
		private IEProductServices _productService;
		private IOrderServices _orderService;
		private UserManager<AplicationUser> _usermanager;

		public BasketController(ICartServices cartServices,IEProductServices eProductServices,IOrderServices orderServices,UserManager<AplicationUser> usermanager)
		{
			_cartService = cartServices;
			_productService = eProductServices;
			_orderService = orderServices;
			_usermanager = usermanager;
		}

		public IActionResult Home()
		{
			var basket = _cartService.GetCartByUserId(_usermanager.GetUserId(User));

			return View(
				new CartModel()
				{
					CartId = basket.Id,
					CartItems = basket.CartItems.Select(i => new CartItemModel()
					{
						CartItemId = i.Id,
						ProductId = i.EProduct.Id,
						ProductName = i.EProduct.Name,
						Price = i.EProduct.Price,
						Quantity = i.Quantity,
						Image = i.EProduct.Images[0].ImageUrl
					}).ToList()
				}
			);
		}
		public IActionResult AddToBasket(int productId,int quantity)
		{
				_cartService.AddToCart(_usermanager.GetUserId(User), productId, quantity);
			
			return RedirectToAction("Home");
		}

		[HttpPost]
		public IActionResult DeleteFromBasket(int eProductId)
		{
			_cartService.DeleteFromCart(_usermanager.GetUserId(User), eProductId);
			return RedirectToAction("Home");
		}
	}
}
