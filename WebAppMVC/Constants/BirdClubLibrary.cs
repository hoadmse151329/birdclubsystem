﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using WebAppMVC.Constants;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Azure;
using BAL.ViewModels;
using Org.BouncyCastle.Ocsp;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Constants
{
    public class BirdClubLibrary
    {
        public BirdClubLibrary()
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
                string json = JsonSerializer.Serialize(inputType,options);
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
                _logger.LogError("Error while processing your request!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nError Message: " + jsonResponse);
                return null;
            };
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            _logger.LogInformation("Processing your Request Successfully!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nSuccess Message: " + jsonResponse);
            return result;
        }

		public void SetCookie(HttpResponse response, string key, object inputType, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions, int? expireTime = null)
		{
			string json = JsonSerializer.Serialize(inputType,jsonOptions);
            if(expireTime.HasValue)
            {
				CookieOptions privatecookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expireTime.Value),
                    MaxAge = TimeSpan.FromMinutes(10),
                    Secure = true,
                    IsEssential = true,
                };
				response.Cookies.Append(key, json, privatecookieOptions);
			}
            else
			response.Cookies.Append(key, json, cookieOptions);
		}

        public async Task<T> GetCookie<T>(HttpRequest request, string key, JsonSerializerOptions jsonOptions) where T :class
        {
            string value = request.Cookies.FirstOrDefault(c => c.Key == key).Value;
            if (value == null) return null;
            var returnobject = JsonSerializer.Deserialize<T>(value, jsonOptions);
            return returnobject;
		}
		public void RemoveCookie(HttpResponse response, string key, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions)
		{
			response.Cookies.Delete(key, cookieOptions);
		}

        public List<SelectListItem> GetManagerEventStatusSelectableList(string eventStatus)
        {
            List<SelectListItem> defaultEventStatus = new();
            switch (eventStatus)
            {
                case var value when value.Equals(Constants.EVENT_STATUS_ON_HOLD):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ON_HOLD, Value = Constants.EVENT_STATUS_ON_HOLD, Selected = true });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_OPEN_REGISTRATION):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_OPEN_REGISTRATION, Value = Constants.EVENT_STATUS_OPEN_REGISTRATION, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_POSTPONED, Value = Constants.EVENT_STATUS_POSTPONED });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_POSTPONED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_POSTPONED, Value = Constants.EVENT_STATUS_POSTPONED, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_OPEN_REGISTRATION, Value = Constants.EVENT_STATUS_OPEN_REGISTRATION });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CLOSED_REGISTRATION, Value = Constants.EVENT_STATUS_CLOSED_REGISTRATION });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CANCELLED, Value = Constants.EVENT_STATUS_CANCELLED });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CLOSED_REGISTRATION):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CLOSED_REGISTRATION, Value = Constants.EVENT_STATUS_CLOSED_REGISTRATION, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_POSTPONED, Value = Constants.EVENT_STATUS_POSTPONED });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CHECKING_IN):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CHECKING_IN, Value = Constants.EVENT_STATUS_CHECKING_IN, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CANCELLED, Value = Constants.EVENT_STATUS_CANCELLED });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_ONGOING):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ONGOING, Value = Constants.EVENT_STATUS_ONGOING, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CANCELLED, Value = Constants.EVENT_STATUS_CANCELLED });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ENDED, Value = Constants.EVENT_STATUS_ENDED });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CANCELLED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CANCELLED, Value = Constants.EVENT_STATUS_CANCELLED, Selected = true, Disabled = true });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_ENDED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ENDED, Value = Constants.EVENT_STATUS_ENDED, Selected = true, Disabled = true });
                        break;
                    }
            }
            return defaultEventStatus;
        }
        public List<SelectListItem> GetManagerFieldTripInclusionTypeSelectableList(string inclusionType)
        {
            List<SelectListItem> defaultInclusionTypes = new();
            switch (inclusionType)
            {
                case var value when value.Equals(Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED):
                    {
                        defaultInclusionTypes.Add(new SelectListItem { Text = Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED, Value = Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED, Selected = true });
                        defaultInclusionTypes.Add(new SelectListItem { Text = Constants.FIELDTRIP_INCLUSION_TYPE_EXCLUDED, Value = Constants.FIELDTRIP_INCLUSION_TYPE_EXCLUDED });
                        break;
                    }
                case var value when value.Equals(Constants.FIELDTRIP_INCLUSION_TYPE_EXCLUDED):
                    {
                        defaultInclusionTypes.Add(new SelectListItem { Text = Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED, Value = Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED });
                        defaultInclusionTypes.Add(new SelectListItem { Text = Constants.FIELDTRIP_INCLUSION_TYPE_EXCLUDED, Value = Constants.FIELDTRIP_INCLUSION_TYPE_EXCLUDED, Selected = true });
                        break;
                    }
            }
            return defaultInclusionTypes;
        }
        public List<SelectListItem> GetStaffEventStatusSelectableList(string eventStatus)
        {
            List<SelectListItem> defaultEventStatus = new();
            switch (eventStatus)
            {
                case var value when value.Equals(Constants.EVENT_STATUS_ON_HOLD):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ON_HOLD, Value = Constants.EVENT_STATUS_ON_HOLD, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_OPEN_REGISTRATION, Value = Constants.EVENT_STATUS_OPEN_REGISTRATION });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_OPEN_REGISTRATION):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_OPEN_REGISTRATION, Value = Constants.EVENT_STATUS_OPEN_REGISTRATION, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CLOSED_REGISTRATION, Value = Constants.EVENT_STATUS_CLOSED_REGISTRATION});
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_POSTPONED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_POSTPONED, Value = Constants.EVENT_STATUS_POSTPONED, Selected = true, Disabled = true });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CLOSED_REGISTRATION):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CLOSED_REGISTRATION, Value = Constants.EVENT_STATUS_CLOSED_REGISTRATION, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CHECKING_IN, Value = Constants.EVENT_STATUS_CHECKING_IN });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CHECKING_IN):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CHECKING_IN, Value = Constants.EVENT_STATUS_CHECKING_IN, Selected = true });
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ONGOING, Value = Constants.EVENT_STATUS_ONGOING });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_ONGOING):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ONGOING, Value = Constants.EVENT_STATUS_ONGOING, Selected = true, Disabled = true });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_CANCELLED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_CANCELLED, Value = Constants.EVENT_STATUS_CANCELLED, Selected = true, Disabled = true });
                        break;
                    }
                case var value when value.Equals(Constants.EVENT_STATUS_ENDED):
                    {
                        defaultEventStatus.Add(new SelectListItem { Text = Constants.EVENT_STATUS_NAME_ENDED, Value = Constants.EVENT_STATUS_ENDED, Selected = true, Disabled = true });
                        break;
                    }
            }
            return defaultEventStatus;
        }

        public T GetValidationTempData<T>(
            ControllerBase context,
            ITempDataDictionary tempData, 
            string tempDataName, 
            string viewObjectName, 
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            if (tempData.Peek(tempDataName) != null)
            {
                var objectForValidation = JsonSerializer.Deserialize<T>(tempData[tempDataName].ToString(), jsonOptions);
                tempData.Remove(tempDataName);
                context.TryValidateModel(objectForValidation, viewObjectName);
                return objectForValidation;
            }
            return null;
        }
        public ITempDataDictionary GetValidationTempData<T>(ITempDataDictionary tempData, string tempDataName, T objectForSerialize, JsonSerializerOptions jsonOptions) where T : class
        {
            string validJson = JsonSerializer.Serialize(objectForSerialize, jsonOptions);
            tempData[tempDataName] = validJson;
            return tempData;
        }
        /* Getter
         * testmodel2.CreateFieldTrip = null;
        if (TempData.Peek(Constants.Constants.CREATE_FIELDTRIP_VALID) != null)
        {
            testmodel2.CreateFieldTrip = JsonSerializer.Deserialize<FieldTripViewModel>(TempData[Constants.Constants.CREATE_FIELDTRIP_VALID].ToString());
            TempData.Remove(Constants.Constants.CREATE_FIELDTRIP_VALID);
            TryValidateModel(testmodel2.CreateFieldTrip, "createFieldTrip");
        }*/
        /*
         * Setter
        if (!ModelState.IsValid)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, options);
        TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                return RedirectToAction("ManagerMeeting");
        }*/
        /* Old Code
         * TempData["ValidationErrors"] = ModelState.Values.SelectMany(v => v.Errors.Select(c => c.ErrorMessage)).ToList();
                List<string> errorlist = ModelState.Values.SelectMany(v => v.Errors.Select(c => c.ErrorMessage)).ToList();
                methcall.SetCookie(Response, "ValidationErrors", errorlist, cookieOptions, options);
        */

    }
}
