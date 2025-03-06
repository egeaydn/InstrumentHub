using Microsoft.AspNetCore.Identity;

namespace Instrument.WebUI.Identity
{
	public static class SeedIdentity
	{
		public static async Task Seed(UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
		{
			var username = configuration["Data: AdminUser:username"];
			var password = configuration["Data: AdminUser:password"];
			var email = configuration["Data:AdminUser:email"];
			var role = configuration["Data:AdminUser:role"];

			if (await userManager.FindByEmailAsync(email) == null)
			{
				await roleManager.CreateAsync(new IdentityRole(role));

				var user = new AplicationUser
				{
					UserName = username,
					Email = email,
					FullName = "Ege Aydın",
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(user, password);

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, role);
				}
				// burada kodumuzu ilk başlattığımızda admin kullanıcısını oluşturuyor olmalıyız fakat benim kodumda şuanda bir hata var ve bu hata düzeltilmelidir.
			}
		}
	}
}
