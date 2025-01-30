using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class ToolsController : Controller
	{
		public IActionResult Application()
		{
			return View();
		}
	}
}
