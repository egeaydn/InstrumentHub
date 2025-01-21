using Instrument.Business.Abstract;
using Instrument.WebUI.Identity;
using Instrument.WebUI.Models;
using InstrumentHub.WebUI.EmailServices;
using InstrumentHub.WebUI.Extensions;
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

		public IActionResult Register()
		{
			return View();
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


		public IActionResult Login(string returnUrl = null)
		{
			return View(
					new LoginModel()
					{
						ReturnUrl = returnUrl
					}
			);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			ModelState.Remove("ReturnUrl");

			if (!ModelState.IsValid)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Giriş Bilgileri",
					Message = "Bilgileriniz Hatalıdır",
					Css = "danger"
				});

				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null)
			{
				ModelState.AddModelError("", "Bu email adresi ile kayıtlı kullanıcı bulunamadı");
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

			if (result.Succeeded)
			{
				return Redirect(model.ReturnUrl ?? "~/");
			}
			if (result.IsLockedOut)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Hesap Kilitlendi",
					Message = "Hesabınız geçici olarak kilitlenmiştir. Lütfen biraz sonra tekrar deneyin.",
					Css = "danger"
				});
				return View(model);
			}

			ModelState.AddModelError("", "Email veya şifre hatalı");

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			TempData.Put("message", new ResultMessageModel()
			{
				Title = "Oturum Hesabı Kapatıldı",
				Message = "Hesabınız güvenli bir şekilde sonlandırıldı",
				Css = "success"
			});
			return Redirect("~/");
		}

	}
}
