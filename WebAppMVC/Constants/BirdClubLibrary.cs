﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models.Error;
using BAL.ViewModels.Manager;

namespace WebAppMVC.Constants
{
    public class BirdClubLibrary
    {
        public BirdClubLibrary()
        {

        }
        #region CallHttpRequestAndGetHttpResponse
        public async Task<T?> CallMethodReturnObject<T>(
            HttpClient _httpClient,
            JsonSerializerOptions options,
            string methodName,
            string url,
            ILogger _logger,
            object? inputType = null,
            string? accessToken = null) where T : class
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
                if (!string.IsNullOrEmpty(jsonResponse) || !string.IsNullOrWhiteSpace(jsonResponse))
                {
                    if (jsonResponse.Contains("data") || jsonResponse.Contains("errorMessage"))
                    {
                        var errorT = JsonSerializer.Deserialize<T>(jsonResponse, options);
                        return errorT;
                    }
                    else if(jsonResponse.Contains("Exception"))
                    {
                        return null;
                    }
                    var error = JsonSerializer.Deserialize<GetErrorVM>(jsonResponse, options);
                    return null;
                }
                return null;
            };
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            _logger.LogInformation("Processing your request Successfully!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nSuccess Message: " + jsonResponse);
            return result;
        }
        #endregion

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
        public List<SelectListItem> GetManagerNewsCategorySelectableList(string category)
        {
            List<SelectListItem> defaultNewsCategories = new();
            switch (category)
            {
                case var value when value.Equals(Constants.NEWS_CATEGORY_ANNOUNCEMENT):
                    {
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Value = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Selected = true });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_MEETING, Value = Constants.NEWS_CATEGORY_MEETING });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_FIELDTRIP, Value = Constants.NEWS_CATEGORY_FIELDTRIP });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_CONTEST, Value = Constants.NEWS_CATEGORY_CONTEST });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_OTHERS, Value = Constants.NEWS_CATEGORY_OTHERS });
                        break;
                    }
                case var value when value.Equals(Constants.NEWS_CATEGORY_MEETING):
                    {
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Value = Constants.NEWS_CATEGORY_ANNOUNCEMENT });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_MEETING, Value = Constants.NEWS_CATEGORY_MEETING, Selected = true });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_FIELDTRIP, Value = Constants.NEWS_CATEGORY_FIELDTRIP });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_CONTEST, Value = Constants.NEWS_CATEGORY_CONTEST });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_OTHERS, Value = Constants.NEWS_CATEGORY_OTHERS });
                        break;
                    }
                case var value when value.Equals(Constants.NEWS_CATEGORY_FIELDTRIP):
                    {
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Value = Constants.NEWS_CATEGORY_ANNOUNCEMENT });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_MEETING, Value = Constants.NEWS_CATEGORY_MEETING });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_FIELDTRIP, Value = Constants.NEWS_CATEGORY_FIELDTRIP, Selected = true });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_CONTEST, Value = Constants.NEWS_CATEGORY_CONTEST });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_OTHERS, Value = Constants.NEWS_CATEGORY_OTHERS });
                        break;
                    }
                case var value when value.Equals(Constants.NEWS_CATEGORY_CONTEST):
                    {
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Value = Constants.NEWS_CATEGORY_ANNOUNCEMENT });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_MEETING, Value = Constants.NEWS_CATEGORY_MEETING });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_FIELDTRIP, Value = Constants.NEWS_CATEGORY_FIELDTRIP });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_CONTEST, Value = Constants.NEWS_CATEGORY_CONTEST, Selected = true });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_OTHERS, Value = Constants.NEWS_CATEGORY_OTHERS });
                        break;
                    }
                case var value when value.Equals(Constants.NEWS_CATEGORY_OTHERS):
                    {
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_ANNOUNCEMENT, Value = Constants.NEWS_CATEGORY_ANNOUNCEMENT });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_MEETING, Value = Constants.NEWS_CATEGORY_MEETING });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_FIELDTRIP, Value = Constants.NEWS_CATEGORY_FIELDTRIP });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_CONTEST, Value = Constants.NEWS_CATEGORY_CONTEST });
                        defaultNewsCategories.Add(new SelectListItem { Text = Constants.NEWS_CATEGORY_OTHERS, Value = Constants.NEWS_CATEGORY_OTHERS, Selected = true });
                        break;
                    }
            }
            return defaultNewsCategories;
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
        public List<SelectListItem> GetStaffEventParticipationStatusSelectableList(string eventStatus)
        {
            List<SelectListItem> defaultStatusTypes = new();
            if(eventStatus!= null && eventStatus.Equals(Constants.EVENT_STATUS_CHECKING_IN))
            {
                defaultStatusTypes.Add(new SelectListItem { Text = Constants.EVENT_PARTICIPANT_STATUS_CHECKED_IN, Value = Constants.EVENT_PARTICIPANT_STATUS_CHECKED_IN });
                defaultStatusTypes.Add(new SelectListItem { Text = Constants.EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN, Value = Constants.EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN });
            }
            else
            {
                defaultStatusTypes.Add(new SelectListItem { Text = Constants.EVENT_PARTICIPANT_STATUS_CHECKED_IN, Value = Constants.EVENT_PARTICIPANT_STATUS_CHECKED_IN, Disabled = true });
                defaultStatusTypes.Add(new SelectListItem { Text = Constants.EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN, Value = Constants.EVENT_PARTICIPANT_STATUS_NOT_CHECKED_IN, Selected = true , Disabled = true });
            }
            return defaultStatusTypes;
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
        public List<SelectListItem> GetMemberStatusSelectableList(string memberStatus)
        {
            List<SelectListItem> defaultMemberStatus = new();
            switch (memberStatus)
            {
                case var value when value.Equals(Constants.MEMBER_STATUS_ACTIVE):
                    {
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_ACTIVE, Value = Constants.MEMBER_STATUS_ACTIVE, Selected = true });
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_SUSPENDED, Value = Constants.MEMBER_STATUS_SUSPENDED });
                        break;
                    }
                case var value when value.Equals(Constants.MEMBER_STATUS_INACTIVE):
                    {
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_ACTIVE, Value = Constants.MEMBER_STATUS_ACTIVE });
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_INACTIVE, Value = Constants.MEMBER_STATUS_INACTIVE, Selected = true });
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_DENIED, Value = Constants.MEMBER_STATUS_DENIED });
                        break;
                    }
                case var value when value.Equals(Constants.MEMBER_STATUS_EXPIRED):
                    {
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_ACTIVE, Value = Constants.MEMBER_STATUS_ACTIVE });
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_EXPIRED, Value = Constants.MEMBER_STATUS_EXPIRED, Selected = true });
                        break;
                    }
                case var value when value.Equals(Constants.MEMBER_STATUS_DENIED):
                    {
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_DENIED, Value = Constants.MEMBER_STATUS_DENIED, Selected = true });
                        break;
                    }
                case var value when value.Equals(Constants.MEMBER_STATUS_SUSPENDED):
                    {
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_ACTIVE, Value = Constants.MEMBER_STATUS_ACTIVE });
                        defaultMemberStatus.Add(new SelectListItem { Text = Constants.MEMBER_STATUS_SUSPENDED, Value = Constants.MEMBER_STATUS_SUSPENDED, Selected = true });
                        break;
                    }
            }
            return defaultMemberStatus;
        }
        public List<SelectListItem> GetEmployeeRoleSelectableList(string employeeRole)
        {
            List<SelectListItem> defaultEmployeeStatus = new();
            switch (employeeRole)
            {
                case var value when value.Equals(Constants.MANAGER):
                    {
                        defaultEmployeeStatus.Add(new SelectListItem { Text = Constants.MANAGER, Value = Constants.MANAGER, Selected = true });
                        defaultEmployeeStatus.Add(new SelectListItem { Text = Constants.STAFF, Value = Constants.STAFF });
                        break;
                    }
                case var value when value.Equals(Constants.STAFF):
                    {
                        defaultEmployeeStatus.Add(new SelectListItem { Text = Constants.MANAGER, Value = Constants.MANAGER });
                        defaultEmployeeStatus.Add(new SelectListItem { Text = Constants.STAFF, Value = Constants.STAFF, Selected = true });
                        break;
                    }
            }
            return defaultEmployeeStatus;
        }
        public List<SelectListItem> GetStaffNameSelectableList(string? staffName, List<GetStaffName> staffNameList)
        {
            List<SelectListItem> defaultStaffNameList = new();
            foreach(var name in staffNameList)
            {
                if(!string.IsNullOrEmpty(staffName) && !string.IsNullOrWhiteSpace(staffName) && staffName.Equals(name))
                {
                    defaultStaffNameList.Add(new SelectListItem { Text = name.FullName, Value = name.FullName, Selected = true });
                }
                else
                {
                    defaultStaffNameList.Add(new SelectListItem { Text = name.FullName, Value = name.FullName });
                }
            }
            return defaultStaffNameList;
        }
        public List<SelectListItem> GetUserGenderSelectableList(string userGender)
        {
            List<SelectListItem> defaultUserGenders = new();
            switch (userGender)
            {
                case var value when value.Equals(Constants.MALE):
                    {
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.MALE, Value = Constants.MALE, Selected = true });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.FEMALE, Value = Constants.FEMALE });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.OTHER, Value = Constants.OTHER });
                        break;
                    }
                case var value when value.Equals(Constants.FEMALE):
                    {
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.MALE, Value = Constants.MALE });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.FEMALE, Value = Constants.FEMALE, Selected = true });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.OTHER, Value = Constants.OTHER });
                        break;
                    }
                case var value when value.Equals(Constants.OTHER):
                    {
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.MALE, Value = Constants.MALE });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.FEMALE, Value = Constants.FEMALE });
                        defaultUserGenders.Add(new SelectListItem { Text = Constants.OTHER, Value = Constants.OTHER, Selected = true });
                        break;
                    }
            }
            return defaultUserGenders;
        }
        public List<SelectListItem> GetBirdStatusSelectableList(string birdStatus)
        {
            List<SelectListItem> defaultBirdStatus = new();
            switch (birdStatus)
            {
                case var value when value.Equals(Constants.BIRD_STATUS_INACTIVE):
                    {
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INACTIVE, Value = Constants.BIRD_STATUS_INACTIVE, Selected = true });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_ACTIVE, Value = Constants.BIRD_STATUS_ACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INJURED, Value = Constants.BIRD_STATUS_INJURED });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_UNAVAILABLE, Value = Constants.BIRD_STATUS_UNAVAILABLE });
                        break;
                    }
                case var value when value.Equals(Constants.BIRD_STATUS_ACTIVE):
                    {
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INACTIVE, Value = Constants.BIRD_STATUS_INACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_ACTIVE, Value = Constants.BIRD_STATUS_ACTIVE, Selected = true });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INJURED, Value = Constants.BIRD_STATUS_INJURED });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_UNAVAILABLE, Value = Constants.BIRD_STATUS_UNAVAILABLE });
                        break;
                    }
                case var value when value.Equals(Constants.BIRD_STATUS_INJURED):
                    {
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INACTIVE, Value = Constants.BIRD_STATUS_INACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_ACTIVE, Value = Constants.BIRD_STATUS_ACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INJURED, Value = Constants.BIRD_STATUS_INJURED, Selected = true });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_UNAVAILABLE, Value = Constants.BIRD_STATUS_UNAVAILABLE });
                        break;
                    }
                case var value when value.Equals(Constants.BIRD_STATUS_UNAVAILABLE):
                    {
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INACTIVE, Value = Constants.BIRD_STATUS_INACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_ACTIVE, Value = Constants.BIRD_STATUS_ACTIVE });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_INJURED, Value = Constants.BIRD_STATUS_INJURED });
                        defaultBirdStatus.Add(new SelectListItem { Text = Constants.BIRD_STATUS_UNAVAILABLE, Value = Constants.BIRD_STATUS_UNAVAILABLE, Selected = true });
                        break;
                    }
            }
            return defaultBirdStatus;
        }
        public List<SelectListItem> GetReqEloRangeSelectableList(string reqEloRange)
        {
            List<SelectListItem> defaultReqEloRange = new();
            switch (reqEloRange)
            {
                case var value when value.Equals(Constants.REQUIRED_ELO_RANGE_DEFAULT):
                    {
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.REQUIRED_ELO_RANGE_DEFAULT, Selected = true });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.REQUIRED_ELO_RANGE_BELOW_1000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.REQUIRED_ELO_RANGE_1000_TO_1500 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_1500_TO_2000 });
                        defaultReqEloRange.Add(new SelectListItem {Text = Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_ABOVE_2000 });
                        break;
                    }
                case var value when value.Equals(Constants.REQUIRED_ELO_RANGE_BELOW_1000):
                    {
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.REQUIRED_ELO_RANGE_DEFAULT });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.REQUIRED_ELO_RANGE_BELOW_1000, Selected = true });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.REQUIRED_ELO_RANGE_1000_TO_1500 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_1500_TO_2000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_ABOVE_2000 });
                        break;
                    }
                case var value when value.Equals(Constants.REQUIRED_ELO_RANGE_1000_TO_1500):
                    {
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.REQUIRED_ELO_RANGE_DEFAULT });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.REQUIRED_ELO_RANGE_BELOW_1000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.REQUIRED_ELO_RANGE_1000_TO_1500, Selected = true });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_1500_TO_2000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_ABOVE_2000 });
                        break;
                    }
                case var value when value.Equals(Constants.REQUIRED_ELO_RANGE_1500_TO_2000):
                    {
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.REQUIRED_ELO_RANGE_DEFAULT });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.REQUIRED_ELO_RANGE_BELOW_1000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.REQUIRED_ELO_RANGE_1000_TO_1500 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_1500_TO_2000, Selected = true });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_ABOVE_2000 });
                        break;
                    }
                case var value when value.Equals(Constants.REQUIRED_ELO_RANGE_ABOVE_2000):
                    {
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_DEFAULT_NAME, Value = Constants.REQUIRED_ELO_RANGE_DEFAULT });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_BELOW_1000_NAME, Value = Constants.REQUIRED_ELO_RANGE_BELOW_1000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1000_TO_1500_NAME, Value = Constants.REQUIRED_ELO_RANGE_1000_TO_1500 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_1500_TO_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_1500_TO_2000 });
                        defaultReqEloRange.Add(new SelectListItem { Text = Constants.REQUIRED_ELO_RANGE_ABOVE_2000_NAME, Value = Constants.REQUIRED_ELO_RANGE_ABOVE_2000, Selected = true });
                        break;
                    }
            }
            return defaultReqEloRange;
        }

        public T? GetValidationTempData<T>(
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
                //viewObjectName is basically a prefix
                context.TryValidateModel(objectForValidation, viewObjectName);
                return objectForValidation;
            }
            return null;
        }

        public List<T> GetValidationTempDataList<T>(
            ControllerBase context,
            ITempDataDictionary tempData,
            string tempDataNamePrefix,
            string viewObjectNamePrefix,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            var list = tempData.Where(t => t.Key.StartsWith(tempDataNamePrefix + "_"));
            if (list != null)
            {
                List<T> result = new();
                foreach ( var item in list)
                {
                    var objectForValidation = JsonSerializer.Deserialize<T>(item.Value.ToString(), jsonOptions);
                    result.Add(objectForValidation);
                    tempData.Remove(item.Key);
                    context.TryValidateModel(objectForValidation, viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                }
                if( result.Count > 0 )
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        
        public Dictionary<string,string>? GetValidationModelStateErrorMessageList<T>(
            ControllerBase context,
            ITempDataDictionary tempData,
            string tempDataNamePrefix,
            string viewObjectNamePrefix,
            JsonSerializerOptions jsonOptions
            ) where T : class
        {
            var list = tempData.Where(t => t.Key.StartsWith(tempDataNamePrefix + "_"));
            if (list.Count() > 0)
            {
                Dictionary<string, string> result = new();
                foreach (var item in list)
                {
                    var objectForValidation = JsonSerializer.Deserialize<T>(item.Value.ToString(), jsonOptions);
                    context.TryValidateModel(objectForValidation, viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                    var listErrors = context.ModelState.FindKeysWithPrefix(viewObjectNamePrefix + "_" + item.Key.Split("_")[1]);
                    foreach (var erroritem in listErrors)
                    {
                        var errors = erroritem.Value.Errors;
                        if (errors != null)
                        {
                            if (errors.Count > 1)
                            {
                                string errorsList = "";
                                foreach (var error in errors)
                                {
                                    errorsList += error.ErrorMessage + ";";
                                }
                                result.Add(erroritem.Key, errorsList);
                            }
                            else
                            {
                                result.Add(erroritem.Key, errors.FirstOrDefault().ErrorMessage);
                            }
                        }
                    }
                }
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        public string? GetUrlStringIfUserSessionDataInValid(Controller context, string requireRole, bool mustBeRole = true)
        {
            string? accToken = context.HttpContext.Session.GetString(Constants.ACC_TOKEN);
            if (string.IsNullOrEmpty(accToken))
            {
                return Constants.LOGIN_URL;
            }

            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role))
            {
                return Constants.LOGIN_URL;
            }
            else if (!role.Equals(requireRole))
            {
                switch (role)
                {
                    case var value when value.Equals(Constants.MEMBER):
                        {
                            return Constants.MEMBER_URL;
                        }
                    case var value when value.Equals(Constants.STAFF):
                        {
                            return Constants.STAFF_URL;
                        }
                    case var value when value.Equals(Constants.MANAGER):
                        {
                            return Constants.MANAGER_URL;
                        }
                    case var value when value.Equals(Constants.ADMIN):
                        {
                            return Constants.ADMIN_URL;
                        }
                }
            }

            string? usrId = context.HttpContext.Session.GetString(Constants.USR_ID);
            if (string.IsNullOrEmpty(usrId))
            {
                return Constants.LOGIN_URL;
            }

            string? usrname = context.HttpContext.Session.GetString(Constants.USR_NAME);
            if (string.IsNullOrEmpty(usrname))
            {
                return Constants.LOGIN_URL;
            }
            string? imagepath = context.HttpContext.Session.GetString(Constants.USR_IMAGE);

            context.TempData[Constants.ROLE_NAME] = role;
            context.TempData[Constants.USR_NAME] = usrname;
            context.TempData[Constants.USR_IMAGE] = imagepath;

            return null;
        }
        public void SetUserDefaultData(Controller context)
        {
            string? accToken = context.HttpContext.Session.GetString(Constants.ACC_TOKEN);
            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role) || string.IsNullOrWhiteSpace(role))
            {
                role = Constants.GUEST;
                context.HttpContext.Session.SetString(Constants.ROLE_NAME, role);
            }
            string? usrId = context.HttpContext.Session.GetString(Constants.USR_ID);
            string? usrname = context.HttpContext.Session.GetString(Constants.USR_NAME);
            string? imagepath = context.HttpContext.Session.GetString(Constants.USR_IMAGE);

            context.TempData[Constants.ACC_TOKEN] = accToken;
            context.TempData[Constants.ROLE_NAME] = role;
            context.TempData[Constants.USR_ID] = usrId;
            context.TempData[Constants.USR_NAME] = usrname;
            context.TempData[Constants.USR_IMAGE] = imagepath;
        }
        public void SetUserRoleGuest(Controller context)
        {
            string? role = context.HttpContext.Session.GetString(Constants.ROLE_NAME);
            if (string.IsNullOrEmpty(role) || string.IsNullOrWhiteSpace(role))
            {
                role = Constants.GUEST;
                context.HttpContext.Session.SetString(Constants.ROLE_NAME, role);
            }
            context.TempData[Constants.ROLE_NAME] = role;
        }
        public List<string>? SetListStringsRatingDisplayByRating(decimal rating)
        {

            List<string> htmlTags = new List<string>();
            int fullStars = (int)rating;
            bool hasHalfStar = rating - fullStars >= 0.5m;

            for (int i = 0; i < fullStars; i++)
            {
                htmlTags.Add("<i class='bx bxs-star yellow'></i>");
            }

            if (hasHalfStar)
            {
                htmlTags.Add("<i class='bx bxs-star-half yellow'></i>");
            }

            for (int i = fullStars + (hasHalfStar ? 1 : 0); i < 5; i++)
            {
                htmlTags.Add("<i class='bx bxs-star'></i>");
            }

            return htmlTags;
        }
        public void LogOut(Controller context)
        {
            context.HttpContext.Session.Clear();
            context.TempData[Constants.ACC_TOKEN] = null;
            context.TempData[Constants.ROLE_NAME] = null;
            context.TempData[Constants.USR_ID] = null;
        }
        public ITempDataDictionary SetValidationTempData<T>(ITempDataDictionary tempData, string tempDataName, T objectForSerialize, JsonSerializerOptions jsonOptions) where T : class
        {
            string validJson = JsonSerializer.Serialize(objectForSerialize, jsonOptions);
            tempData[tempDataName] = validJson;
            return tempData;
        }
        public ITempDataDictionary SetValidationTempDataWithId<T>(ITempDataDictionary tempData, string tempDataName, int objectId, T objectForSerialize, JsonSerializerOptions jsonOptions) where T : class
        {
            string validJson = JsonSerializer.Serialize(objectForSerialize, jsonOptions);
            tempData[tempDataName + "_" + objectId] = validJson;
            return tempData;
        }
        public void SetCookieForTempFile(HttpResponse response, string key, IFormFile inputType, CookieOptions cookieOptions, JsonSerializerOptions jsonOptions, int? expireTime = null)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMP_FILE_LOCATION_FOLDER);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            /*FileInfo fileInfo = new FileInfo(inputType.FileName);
            string fileName = inputType. + fileInfo.Extension;*/

            string fileNameWithPath = Path.Combine(path, inputType.FileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                try
                {
                    inputType.CopyTo(stream);
                }
                catch (Exception e)
                {
                    stream.Dispose();
                    throw;
                }
                finally
                {
                    stream.Dispose();
                }
            }
            string json = JsonSerializer.Serialize(fileNameWithPath, jsonOptions);
            if (expireTime.HasValue)
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
        public async Task<IFormFile> GetCookieForTempFile(HttpRequest request, string key, JsonSerializerOptions jsonOptions)
        {
            string value = request.Cookies.FirstOrDefault(c => c.Key == key).Value;
            if (value == null) return null;
            var filepath = JsonSerializer.Deserialize<string>(value, jsonOptions);
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                var ms = new MemoryStream();
                try
                {
                    return new FormFile(stream, 0, stream.Length, null, Path.GetFileName( stream.Name));
                }
                catch (Exception e)
                {
                    stream.Dispose();
                    ms.Dispose();
                    throw;
                }
                finally
                {
                    ms.Dispose();
                    stream.Dispose();
                }
            }
        }
        public void RemoveCookieTempFile(HttpResponse response, string key, IFormFile inputType, CookieOptions cookieOptions)
        {
            response.Cookies.Delete(key, cookieOptions);
            string path = Path.Combine(Directory.GetCurrentDirectory(), Constants.TEMP_FILE_LOCATION_FOLDER);
            string fileNameWithPath = Path.Combine(path, inputType.FileName);
            if (File.Exists(fileNameWithPath))
            {
                File.Delete(fileNameWithPath);
            }
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
