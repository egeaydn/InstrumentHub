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
				string siteUrl = "https://localhost:7136";//7136 portu benim bilgisayarımda çalışan port 
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



		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (userId == null || token == null)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Geçersiz Token",
					Message = "Hesap onay bilgileri yanlış",
					Css = "danger"
				});

				return Redirect("~");
			}

			var user = await _userManager.FindByIdAsync(userId);

			if (user != null)
			{
				var result = await _userManager.ConfirmEmailAsync(user, token); // email confirmed ı 1 yapar.

				if (result.Succeeded)
				{
					_cartService.InitialCart(userId);

					TempData.Put("message", new ResultMessageModel()
					{
						Title = "Hesap Onayı",
						Message = "Hesabınız onaylanmıştır",
						Css = "success"
					});

					return RedirectToAction("Login", "Account");

				}

			}

			TempData.Put("message", new ResultMessageModel()
			{
				Title = "Hesap Onayı",
				Message = "Hesabınız onaylanmamıştır",
				Css = "danger"
			});

			return Redirect("~");
		}


		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Şifremi Unuttum",
					Message = "Lütfen Email adresini boş bırakmayınız",
					Css = "danger"
				});

				return View();
			}

			var user = await _userManager.FindByEmailAsync(email);

			if (user is null)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Şifremi Unuttum",
					Message = "Bu Email adresiyle bir kullanıcı bulunamadı",
					Css = "danger"
				});

				return View();
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callbackUrl = Url.Action("ResetPassword", "Account", new
			{
				token = code
			});

			string siteUrl = "https://localhost:7136";
			string activeUrl = $"{siteUrl}{callbackUrl}";

			string body = $"Parolanızı yenilemek için linke <a href='{activeUrl}'> tıklayınız.</a>";

			// Email Service 
			MailHelper.SendEmail(body, email, "InstrumentHub Parola Yenileme");

			TempData.Put("message", new ResultMessageModel()
			{
				Title = "Şifremi Unuttum",
				Message = "Email adresinize şifre yenileme bağlantısı gönderilmiştir.",
				Css = "success"
			});

			return RedirectToAction("Login");
		}




		public IActionResult ResetPassword(string token)
		{
			if (token == null)
			{
				return RedirectToAction("Home", "Index");
			}

			var model = new ResetPassModel { Token = token };

			return View(model);
		}



		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPassModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Şifremi Unuttum",
					Message = "Bu Email adresi ile kullanıcı bulunamadı.",
					Css = "danger"
				});
				return RedirectToAction("Home", "Index");
			}

			var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("Login");
			}
			else
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Şifremi Unuttum",
					Message = "Şifreniz uygun değildir.",
					Css = "danger"
				});

				return View(model);
			}
		}


		public async Task<IActionResult> Manage()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Bağlantı Hatası",
					Message = "Kullanıcı bilgileri bulunamadı tekrar deneyin.",
					Css = "danger"
				});
				return View();
			}

			var model = new AccountModel
			{
				FullName = user.FullName,
				UserName = user.UserName,
				Email = user.Email
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Manage(AccountModel model)
		{

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
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				TempData["message"] = new ResultMessageModel()
				{
					Title = "Bağlantı Hatası",
					Message = "Kullanıcı bilgileri bulunamadı, lütfen tekrar deneyin.",
					Css = "danger"
				};
				return RedirectToAction("Login", "Account");
			}




			user.FullName = model.FullName;
			user.UserName = model.UserName;
			user.Email = model.Email;

			if (model.Email != user.Email)
			{
				var code = await _userManager.GeneratePasswordResetTokenAsync(user);
				var callbackUrl = Url.Action("ResetPassword", "Account", new
				{
					userId = user.Id,
					token = code
				});
				string siteUrl = "https://localhost:7076";
				string resetUrl = $"{siteUrl}{callbackUrl}";
				
				string body = $"Şifrenizi yenilemek için linke <a href='{resetUrl}'> tıklayınız.</a>";

				MailHelper.SendEmail(body, model.Email, "ETRADE Şifre Sıfırlama");
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Şifre Sıfırlama",
					Message = "Şifre sıfırlama linki email adresinize gönderilmiştir.",
					Css = "success"
				});
				return RedirectToAction("Login");
			}

			var result = await _userManager.UpdateAsync(user);


			if (result.Succeeded)
			{
				TempData.Put("message", new ResultMessageModel()
				{
					Title = "Hesap Bilgileri Güncellendi",
					Message = "Bilgileriniz başarıyla güncellenmiştir.",
					Css = "success"
				});
				return RedirectToAction("Index", "Home");
			}

			TempData.Put("message", new ResultMessageModel()
			{
				Title = "Hata",
				Message = "Bilgileriniz güncellenemedi. Lütfen tekrar deneyin.",
				Css = "danger"
			});
			return View(model);
		}
	}
}
