using System.ComponentModel.DataAnnotations;

namespace Instrument.WebUI.Models
{
	public class LoginTemplate
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
