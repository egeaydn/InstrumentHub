using Instrument.WebUI.Models;

namespace Instrument.WebUI.ViewComponent
{
	public class DivsionListViewComponent : ViewComponent
	{
		private IDivisonsServices _divisonsServices;

		public DivsionListViewComponent(IDivisonsServices divisonsServices)
		{
			_divisonsServices = divisonsServices;
		}

		public IViewComponentResult Invoke()
		{
			return View(
				
					new DivisionListViewModel()
					{
						SelectDivision = RouteData.Values["category"]?.ToString(),
						Divisions = _divisonsServices.GetAll()
					}

				);
		}
	}
}
