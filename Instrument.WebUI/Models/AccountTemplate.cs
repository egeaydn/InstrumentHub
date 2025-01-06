using Instrument.WebUI.Identity;

namespace Instrument.WebUI.Models
{
	public class AccountTemplate : AplicationUser
	{
		public string FullName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
	}
}
