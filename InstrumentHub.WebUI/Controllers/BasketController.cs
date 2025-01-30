using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class BasketController : Controller
	{
		private ICartServices _cartService;
		private IEProductServices _productService;
		private IOrderServices _orderService;
		private List<AplicationUser> _usermanager;

		public BasketController(ICartServices cartServices,IEProductServices eProductServices,IOrderServices orderServices,List<AplicationUser> usermanager)
		{
			_cartService = cartServices;
			_productService = eProductServices;
			_orderService = orderServices;
			_usermanager = usermanager;
		}

		public IActionResult Home()
		{


			return View();
		}
	}
}
