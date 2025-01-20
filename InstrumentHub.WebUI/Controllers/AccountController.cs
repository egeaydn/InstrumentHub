using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.WebUI.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentHub.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AplicationUser> _userManager;
		private SignInManager<AplicationUser> _signInManager;
		private ICartServices _cartService;

		public AccountController(UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager, ICartServices cartService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_cartService = cartService;
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel register)
		{

			if (!ModelState.IsValid)
			{
				return View(register);
			}

			var user = new AplicationUser
			{
				UserName = register.UserName,
				Email = register.Email,
				FullName = register.FullName
			};
			var result = await _userManager.CreateAsync(user, register.Password);

			if (result.Succeeded)
			{
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = code });
				string siteUrl = "https://localhost:5174";
				string aktifUrl = $"{siteUrl}{callbackUrl}";

				string body = $"Merhaba {user.FullName}, <br> Hesabınızı aktifleştirmek ve Sitemizin Tüm Fonksiyonlarından yararlanmak için <a href='{aktifUrl}'>tıklayınız</a>";

				MailHelper.SendEmail(body, register.Email, "InstrumentHub Yönetim ve Hesap Aktifleştirme Onayı");
			}
			return View(register);
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Login()
		{

			return View();
		}
	}
}
