using MRSUMobile.Models.User;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MRSUMobile.Services
{
	public static class mrsuApiService
	{
		//private static string baseURL = @"https://papi.mrsu.ru/";

		//public static HttpClient MrsuAPI;

		//static PAPI()
		//{
		//	MrsuAPI = new HttpClient()
		//	{
		//		BaseAddress = new System.Uri(baseURL)
		//	};
		//}

		//public static async Task<bool> Authorize()
		//{
		//	MrsuAPI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue()
		//}

		//public static async Task<User> GetUser()
		//{
		//	var json = await MrsuAPI.GetStringAsync("v1/User");
		//	var user = JsonSerializer.Deserialize<User>(json);
		//	return user;
		//}
	}
}