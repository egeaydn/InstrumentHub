using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class ToolsController : Controller
	{
		/*
			Bu controllerların veritabanı ile veya başkabirşey ile ilgisi olmadığımı için işleri boş bunlar sadece view döndürüyorlar
		 */
		public IActionResult Application()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		public IActionResult SSS()
		{
			return View();
		}
	}
}
