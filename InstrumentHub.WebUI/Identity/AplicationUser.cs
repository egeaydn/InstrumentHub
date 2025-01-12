using Microsoft.AspNetCore.Identity;

namespace Instrument.WebUI.Identity
{
	public  class AplicationUser : IdentityUser
	{
		public string FullName { get; set; }
	}
}
