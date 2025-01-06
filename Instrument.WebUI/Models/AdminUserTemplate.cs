using System.ComponentModel.DataAnnotations;

namespace Instrument.WebUI.Models
{
	public class AdminUserTemplate
	{
		public string FullName { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string UserName { get; set; }
		public bool IsAdmin  { get; set; }
		public bool EmailSuccess  { get; set; }

	}
}
