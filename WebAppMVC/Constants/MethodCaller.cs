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
            ILogger _logger,
            object inputType = null,
            string accessToken = null) where T : class
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if(accessToken != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if (methodName.Equals(Constants.GET_METHOD) && inputType == null)
            {
                response = await _httpClient.GetAsync(url);
            }
            else if(inputType != null)
            {
                string json = JsonSerializer.Serialize(inputType);
                // sử dụng frombody để lấy dữ liệu
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                if (methodName.Equals(Constants.POST_METHOD))
                {
                    response = await _httpClient.PostAsync(url, content);
                }else if (methodName.Equals(Constants.PUT_METHOD))
                {
                    response = await _httpClient.PutAsync(url, content);
                }else if (methodName.Equals(Constants.DELETE_METHOD))
                {
                    response = await _httpClient.DeleteAsync(url);
                }
            }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error while processing your request!: " + response.StatusCode + " , Error Message: " + jsonResponse);
                return null;
            };
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            _logger.LogInformation("Processing your Request Successfully!: " + response.StatusCode + " , Success Message: " + result);
            return result;
        }
    }
}
