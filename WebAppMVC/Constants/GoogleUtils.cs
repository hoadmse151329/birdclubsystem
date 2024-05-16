using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DAL.Models;
using Newtonsoft.Json;
using WebAppMVC.Constants;

public static class GoogleUtils
{
    public static async Task<string> GetToken(string code)
    {
        using (var client = new HttpClient())
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", Constants.GOOGLE_CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", Constants.GOOGLE_CLIENT_SECRET),
                new KeyValuePair<string, string>("redirect_uri", Constants.GOOGLE_REDIRECT_URI),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            });

            var response = await client.PostAsync(Constants.GOOGLE_LINK_GET_TOKEN, formContent);
            var responseString = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseString);
            return responseData.access_token;
        }
    }

    public static async Task<GoogleUserInfo> GetUserInfo(string accessToken)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(Constants.GOOGLE_LINK_GET_USER_INFO + accessToken);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleUserInfo>(responseString);
        }
    }
}