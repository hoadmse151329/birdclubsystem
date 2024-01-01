using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using WebAppMVC.Constants;
using System.Text;
using System.Text.Json;

namespace WebAppMVC.Constants
{
    public class MethodCaller
    {
        public MethodCaller()
        {
        }
        public async Task<T?> CallMethodReturnObject<T>(
            HttpClient _httpClient,
            JsonSerializerOptions options,
            string methodName,
            string url,
            T inputType = null) where T : class
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var json = JsonSerializer.Serialize(inputType);
            // sử dụng frombody để 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (methodName.Equals(Constants.GET_METHOD))
            {
                response = await _httpClient.GetAsync(url);
            }
            else if (methodName.Equals(Constants.POST_METHOD) && inputType != null)
            {
                response = await _httpClient.PostAsync(url, content);
            }
            else if (methodName.Equals(Constants.PUT_METHOD) && inputType != null)
            {
                response = await _httpClient.PutAsync(url, content);
            }
            if (!response.IsSuccessStatusCode) return null;
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            return result;
        }
    }
}
