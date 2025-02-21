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

		public IActionResult Index()
		{
			var products = _productService.GetAll();
			return View(products);
		}

		public IActionResult GetByPriceRange(decimal minPrice, decimal maxPrice)
		{
			// Fiyat aralığı kontrolü (minPrice < maxPrice)
			if (minPrice > maxPrice)
			{
				TempData["Error"] = "Minimum fiyat, maksimum fiyattan büyük olamaz.";
				return RedirectToAction("Index"); // Hata durumunda ana sayfaya yönlendir
			}

			// Fiyat aralığında ürünleri getir
			var productsInRange = _productService.GetProductsByPriceRange(minPrice, maxPrice);

			// Ürünler boşsa hata mesajı ekleyelim
			if (productsInRange == null || productsInRange.Count == 0)
			{
				TempData["Error"] = "Bu fiyat aralığında ürün bulunamadı.";
			}

			var model = new EProductListModel
			{
				EProducts = productsInRange
			};

			return View("Index", model); // Fiyat aralığına göre filtrelenmiş ürünleri Index sayfasına gönder
		}
	}
}
