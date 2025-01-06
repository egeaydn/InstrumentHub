using Microsoft.Extensions.FileProviders;

namespace Instrument.WebUI.MiddleWare
{
	public static class BuilderExtension
	{
		public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder app)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "node_modules");
			var options = new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(path),
				RequestPath = "/node_modules"
			};
			app.UseStaticFiles(options);
			return app;
		}
	}
}
