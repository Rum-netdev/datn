
namespace EcommercialWebApp.Data.Authentication
{
    public class JwtConfiguration
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public string SignInKey { get; set; }
    }
}
