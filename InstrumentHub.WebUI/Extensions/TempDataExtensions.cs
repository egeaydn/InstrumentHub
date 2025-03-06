using Microsoft.AspNetCore.Mvc.ViewFeatures; 
using Newtonsoft.Json;

namespace InstrumentHub.WebUI.Extensions
{
	public static class TempDataExtensions
	{
		public static void Put<T>(this ITempDataDictionary tempdata, string key, T value) where T : class
		{
			// Verilen nesneyi JSON formatına çevirerek TempData içine kaydediyoruz
			tempdata[key] = JsonConvert.SerializeObject(value);
		}

		public static T Get<T>(this ITempDataDictionary tempdata, string key) where T : class
		{
			object o;

			// Belirtilen key'in TempData içinde olup olmadığını kontrol ediyoruz
			tempdata.TryGetValue(key, out o);

			// Eğer key bulunamazsa, null döndürür
			return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
		}
	}
}
