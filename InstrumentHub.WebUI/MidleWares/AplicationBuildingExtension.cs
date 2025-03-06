using Microsoft.Extensions.FileProviders;

namespace InstrumentHub.WebUI.MidleWares
{
	public static class AplicationBuildingExtension
	{
		public static IApplicationBuilder CustomStaticFiles(this IApplicationBuilder app)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "node_modules");//dizindeki node_modules klasörünü bulur.

			var options = new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(path), //bu dizini dosya sağlayıcısı olarak kullanır ve bu dizindeki dosyaları sunar.
				RequestPath = "/modules"
			};

			app.UseStaticFiles(options);// BUrada ise bu ayarları kullanarak statik dosyaları servis ederiz
			return app;
		}
	}
}
