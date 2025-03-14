using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Instrument.WebUI.Identity
{
	// Kullanıcı kimlik doğrulama ve yetkilendirme için kullanılan veritabanı bağlamı
	// IdentityDbContext'ten türetilerek ASP.NET Core Identity altyapısını kullanır
	public class ApplicationIdentityDbContext : IdentityDbContext<AplicationUser>
	{
		// Veritabanı bağlantı ayarlarını constructor üzerinden alır
		// options parametresi Program.cs'de yapılandırılan bağlantı ayarlarını içerir
		public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
		{

		}
	}
}

