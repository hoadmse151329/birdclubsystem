using BAL.ViewModels.Authenticates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IJWTService
    {
        public string GenerateJWTToken(int userID, string username, string role);
        public ObjectToken ExtractToken(string token);
    }
}
