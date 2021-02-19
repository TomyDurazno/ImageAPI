using ImageAPI.Interfaces;
using ImageAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImageAPI.Services
{
    public class ImageService : IImageService
	{
		string _authpath; 
		string _imageDetailsUrl;
		string _url;
		string _apiKey;

		public ImageService(string authPath, string imageDetailsUrl, string url, string apiKey)
        {
			_authpath = authPath;
			_imageDetailsUrl = imageDetailsUrl;
			_url = url;
			_apiKey = apiKey;
		}

		public async Task<ImageSearchPaginatedResult> GetPage(int n)
        {
			var client = new RestClient(new Uri(_url.Replace("{{n}}", n.ToString())));

			var result = await MakeRequest(client);

			return JsonSerializer.Deserialize<ImageSearchPaginatedResult>(result.Content);
		}

		public async Task<ImageSearchDetailResult> GetImage(string id)
		{
			var client = new RestClient(new Uri(_imageDetailsUrl.Replace("{{id}}", id)));

			var result = await MakeRequest(client);

			return JsonSerializer.Deserialize<ImageSearchDetailResult>(result.Content);
		}

		public async Task<IRestResponse> MakeRequest(RestClient client)
        {
			RestRequest Make()
			{
				var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
				request.AddHeader("Authorization", $"Bearer {Token}");
				return request;
			}

			var result = await client.ExecuteAsync(Make());

			if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				await Refresh();

				return await client.ExecuteAsync(Make());
			}
            else
				return result;
		}

		public string Token { get; set; }

		public async Task<AuthResult> Refresh()
        {
			var client = new RestClient(new Uri(_authpath));

			var request = new RestRequest(Method.POST) { RequestFormat = DataFormat.Json };

			request.AddJsonBody(new { apiKey = _apiKey });

			var result = await client.ExecuteAsync(request);

			var authResult = JsonSerializer.Deserialize<AuthResult>(result.Content);

			Token = authResult.token;

			return authResult;
		}
    }
}
