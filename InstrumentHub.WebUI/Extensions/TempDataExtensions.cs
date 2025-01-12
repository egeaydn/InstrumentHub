using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace InstrumentHub.WebUI.Extensions
{
	public static class TempDataExtensions
	{
		public static void Put<T>(this ITempDataDictionary tempdata, string key,T value) where T : class
		{
			tempdata[key] = JsonConvert.SerializeObject(value);
		}
		public static T Get<T>(this ITempDataDictionary tempdata, string key) where T : class
		{
			object o;
			tempdata.TryGetValue(key, out o);
			return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
		}
	}
}
