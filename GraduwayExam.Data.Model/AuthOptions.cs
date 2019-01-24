using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GraduwayExam.Data.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "GWTestApp";
        public const string AUDIENCE = "https://graduway.azurewebsites.net/";
        const string KEY = "1B6210940E639B234A1ACED9C2A1404BC500E88608427619F0A99FB19F4D3AA2";
        public const int LIFETIME = 20;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}