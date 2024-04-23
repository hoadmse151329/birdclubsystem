using BAL.ViewModels.Authenticates;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateJWTToken(string userID, string username, string role, IConfiguration config, DateTime? expir = null);
		string GenerateJWTToken(string username, string role, IConfiguration config, DateTime? expir = null);
        void RemoveJWTToken(string token, IConfiguration config);
		ObjectToken ExtractToken(string token, IConfiguration config);
    }
}
