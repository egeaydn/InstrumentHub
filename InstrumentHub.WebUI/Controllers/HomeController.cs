using System.Diagnostics;
using Instrument.Business.Abstract;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private IEProductServices _eproductServices;

		public HomeController(IEProductServices eProductServices)
		{
			_eproductServices = eProductServices;
		}

		public IActionResult Index()
		{
			var eProducts = _eproductServices.GetAll();

			if (eProducts == null || eProducts.Any() )
			{
				eProducts = new List<EProduct>();
			}
			return View(new EProductListModel()
			{
				EProducts = eProducts,
			});
		}
	}
}
