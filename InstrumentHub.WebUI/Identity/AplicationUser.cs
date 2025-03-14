using Microsoft.AspNetCore.Identity;

namespace Instrument.WebUI.Identity
{
	// ASP.NET Core Identity'nin IdentityUser sınıfından türetilen özelleştirilmiş kullanıcı sınıfı
	// Varsayılan Identity özelliklerine (Email, UserName, PasswordHash vb.) ek özellikler ekler
	public class AplicationUser : IdentityUser
	{
		// Kullanıcının tam adını saklamak için eklenen özel alan
		public string FullName { get; set; }
	}
}
