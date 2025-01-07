using System.Diagnostics;
using Instrument.Business.Abstract;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Instrument.WebUI.Controllers
{
	public class HomeController : Controller
	{
		IEProductServices _productServices;

		public HomeController(IEProductServices productServices)
		{
			_productServices = productServices;
		}
		public IActionResult Index()
		{
			var products = _productServices.GetAll();

			if (products == null || !products.Any())
			{
				products = new List<EProduct>();
			}
			return View(new EProductListTemplate()
			{
				EProducts = products
			});
		}

	}
}
