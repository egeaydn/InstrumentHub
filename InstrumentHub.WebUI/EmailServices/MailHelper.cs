using System.Net.Mail;
using System.Net;

namespace InstrumentHub.WebUI.EmailServices
{
	public static class MailHelper
	{

		public static bool SendEmail(string body, string to, string subject, bool isHtml = true)
		{
			return SendEmail(body, new List<string> { to }, subject, isHtml);
		}

		// Birden fazla alıcıya mail göndermeyi sağlayan asıl metot.
		private static bool SendEmail(string body, List<string> to, string subject, bool isHtml)
		{
			bool result = false; // Mail gönderim sonucunu burada tutuyoruz

			try
			{
				var message = new MailMessage(); // Yeni bir e-posta mesajı nesnesi oluşturuluyor burada

				// Gönderen e-posta adresi belirleniyor.
				message.From = new MailAddress("instrumenthubmailservices@gmail.com");

				// Alıcı adresler mesajın To kısmına ekiyorum
				to.ForEach(x =>
				{
					message.To.Add(new MailAddress(x));
				});

				message.Subject = subject;  
				message.Body = body;       
				message.IsBodyHtml = isHtml; 

				// SMTP istemcisi oluşturuluyor ve Gmail'in SMTP sunucusunu kullanıyoruz
				using (var smtp = new SmtpClient("smtp.gmail.com", 587))
				{
					smtp.EnableSsl = true; // ssl aktif ediyoruz

					// gmail adı ve şifre belirleniyor şifre ilgili google hesabımızda güvenlik ayarlarının olduğu kısımda
					// uygulama şifreleri kısmından alınabilir ve şifre tek seferlik gösterilir
					smtp.Credentials = new NetworkCredential("instrumenthubmailservices@gmail.com", "dgjk jxrg gcjh tjrk" +
						"");

					smtp.UseDefaultCredentials = false; 

					smtp.Send(message); // E-posta yı gönderiyoruz
					result = true; // Mail gönderildiği için sonucu true olarak döndürüyoruz
				}
			}
			catch (Exception e)
			{
				// 
				Console.WriteLine(e);
				result = false;
			}

			return result; 
		}
	}
}
