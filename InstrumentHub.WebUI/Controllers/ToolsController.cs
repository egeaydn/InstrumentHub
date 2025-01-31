using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class ToolsController : Controller
	{
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
	}
}
