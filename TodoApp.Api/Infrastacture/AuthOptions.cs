using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoApp.Api.Infrastacture
{
    public class AuthOptions
    {
        public const string Issuer = "TodoAppServer";
        public const string Audience = "TodoAppClient";
        const string Key = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
