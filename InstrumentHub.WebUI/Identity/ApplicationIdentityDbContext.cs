using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Instrument.WebUI.Identity
{
	public class ApplicationIdentityDbContext : IdentityDbContext<AplicationUser>
	{
		public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
		{
		}
	}
}
