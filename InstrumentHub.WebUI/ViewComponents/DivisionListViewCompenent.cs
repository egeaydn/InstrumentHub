using Instrument.Business.Abstract;
using Instrument.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.ViewComponents
{
	public class DivisionListViewComponent : ViewComponent
	{
		private IDivisonsServices _divisionService;

		public DivisionListViewComponent(IDivisonsServices divisionService)
		{
			_divisionService = divisionService;
		}

		public IViewComponentResult Invoke()
		{
			return View(
				new DivisionListViewModel
				{
					SelectDivision = RouteData.Values["division"]?.ToString(),
					Divisions = _divisionService.GetAll()
				}
			);
		}
	}
}
