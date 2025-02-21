using Instrument.Business.Abstract;
using Instrument.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IEProductServices _productService;

		public ProductController(IEProductServices productService)
		{
			_productService = productService;
		}

		// Tüm ürünleri getir
		public IActionResult Index()
		{
			var model = new EProductListModel
			{
				EProducts = _productService.GetAll() // Modeli doğru şekilde set ettik
			};
			return View(model);
		}

		// Belirli fiyat aralığında ürünleri getir
		public IActionResult GetByPriceRange(decimal minPrice, decimal maxPrice)
		{
			if (minPrice > maxPrice)
			{
				TempData["Error"] = "Minimum fiyat, maksimum fiyattan büyük olamaz.";
				return RedirectToAction("Index");
			}

			var productsInRange = _productService.GetProductsByPriceRange(minPrice, maxPrice);

			if (productsInRange == null || productsInRange.Count == 0)
			{
				TempData["Error"] = "Bu fiyat aralığında ürün bulunamadı.";
			}

			var model = new EProductListModel
			{
				EProducts = productsInRange
			};

			return View("Index", model); // **Burada View içine model gönderiyoruz!**
		}


	}
}
