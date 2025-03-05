using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace InstrumentHub.WebUI.EmailServices
{
	public class MailHelper
	{
		private readonly IConfiguration _configuration;

		// Bağımlılık enjeksiyonu ile IConfiguration'ı alıyoruz
		public MailHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// Tek alıcıya mail göndermeyi sağlayan yardımcı metot.
		public bool SendEmail(string body, string to, string subject, bool isHtml = true)
		{
			return SendEmail(body, new List<string> { to }, subject, isHtml);
		}

		// Birden fazla alıcıya mail göndermeyi sağlayan asıl metot.
		private bool SendEmail(string body, List<string> to, string subject, bool isHtml)
		{
			bool result = false; // Mail gönderim sonucunu burada tutuyoruz

			try
			{
				// SMTP ayarlarını appsettings.json'dan okuyoruz
				var smtpSettings = _configuration.GetSection("SmtpSettings");

				var message = new MailMessage(); // Yeni bir e-posta mesajı nesnesi oluşturuluyor burada

				// Gönderen e-posta adresi belirleniyor.
				message.From = new MailAddress(smtpSettings["Username"]);

				// Alıcı adresleri mesajın To kısmına ekliyoruz
				to.ForEach(x =>
				{
					message.To.Add(new MailAddress(x));
				});

				message.Subject = subject;
				message.Body = body;
				message.IsBodyHtml = isHtml;

				// SMTP istemcisi oluşturuluyor ve yapılandırılıyor
				using (var smtp = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"])))
				{
					smtp.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]); // SSL kullanımı etkinleştiriliyor

					//  kimlik doğrulama bilgilerini appsettings.json'dan alıyoruz güvenlik için
					smtp.Credentials = new NetworkCredential(
						smtpSettings["Username"],
						smtpSettings["Password"]
					);

					smtp.UseDefaultCredentials = false;

					smtp.Send(message); // E-postayı gönderiyoruz
					result = true; // Mail gönderildiği için sonucu true olarak döndürüyoruz
				}
			}
			catch (Exception e)
			{
				// Hata oluştuğunda hatayı loglama veya hata mesajını saklama işlemi burada yapılabilir
				Console.WriteLine($"Email gönderme hatası: {e.Message}");
				result = false;
			}

			return result;
		}
	}
}
