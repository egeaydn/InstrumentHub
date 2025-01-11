using System.ComponentModel.DataAnnotations;

namespace Instrument.WebUI.Models
{
	public class ResetPassModel
	{
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string Token { get; set; }

	}
}
