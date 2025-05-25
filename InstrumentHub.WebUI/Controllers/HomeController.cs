using System.Diagnostics;
using Instrument.Business.Abstract;
using Instrument.WebUI.Models;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	// Ana sayfa işlemlerini yöneten controller sınıfı
	public class HomeController : Controller
	{
		// Ürün işlemlerini yöneten servis arayüzü
		private IEProductServices _productServices;

		// Dependency injection ile servis bağımlılığının çözümlenmesi
		public HomeController(IEProductServices productServices)
		{
			_productServices = productServices;
		}

		// Ana sayfa görünümünü döndüren action metodu
		// Tüm ürünleri listeler
		public IActionResult Index()
		{
			var products = _productServices.GetAll();

			// Ürün listesi boş ise yeni liste oluştur
			if (products == null || !products.Any())
			{
				products = new List<EProduct>();
			}

			// Ürünleri view model içinde görünüme gönder
			return View(new EProductListModel()
			{
				EProducts = products,
			});
		}

		/*bu kjsmda home sayfasna eklediim fiyat aralna gre �r�nleri getirme i�lemi yap�l�yor.
		  katmanlar arası ba��ml�l��� azaltmak için bu işlemi controllerda yapmak yerine service katman�nda yapmak daha mant�kl�yd�
			öyle yaptık bu alddaki k�osmda home sayfam�zdaki fiyat filtreleme k�sm�n�n controller�
		 */
		public IActionResult FilterByPrice(decimal minPrice, decimal maxPrice)
		{
			// Fiyat aralığı kontrolü
			if (minPrice > maxPrice)
			{
				TempData["Error"] = "Minimum fiyat, maksimum fiyattan büyük olamaz.";
				return RedirectToAction("Index");
			}

			// Fiyat aralığına göre ürünleri getir
			var productsInRange = _productServices.GetProductsByPriceRange(minPrice, maxPrice);

			// Ürün bulunamadıysa hata mesajı göster
			if (productsInRange == null || productsInRange.Count == 0)
			{
				TempData["Error"] = "Bu fiyat aralığında ürün bulunamadı.";
			}

			var model = new EProductListModel
			{
				EProducts = productsInRange
			};

			return View("Index", model);  // **Home/Index'e ynlendiriyoruz**
		}
	}
}
