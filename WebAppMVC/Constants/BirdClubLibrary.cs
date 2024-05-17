using Microsoft.AspNetCore.Mvc;
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
using WebAppMVC.Models;

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
                var error = JsonSerializer.Deserialize<T>(jsonResponse, options);
                return error;
            };
            var result = JsonSerializer.Deserialize<T>(jsonResponse, options);
            _logger.LogInformation("Processing your request Successfully!: " + response.StatusCode + "\t\nApi Url: " + url + "\t\nSuccess Message: " + jsonResponse);
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
            if (string.IsNullOrEmpty(role))
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
            if (string.IsNullOrEmpty(role))
            {
                role = Constants.GUEST;
            }
            context.TempData[Constants.ROLE_NAME] = role;
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
