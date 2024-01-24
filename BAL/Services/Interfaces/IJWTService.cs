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
        public string GenerateJWTToken(string userID, string username, string role, IConfiguration config);
        public ObjectToken ExtractToken(string token, IConfiguration config);
    }
}
