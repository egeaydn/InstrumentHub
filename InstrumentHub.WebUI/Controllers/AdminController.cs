using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class AdminController : Controller
	{

		private IEProductServices _productService;
		private IDivisonsServices _categoryService;
		private UserManager<AplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public AdminController(IEProductServices productService, IDivisonsServices categoryService, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_productService = productService;
			_categoryService = categoryService;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		
	}
}
