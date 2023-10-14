using Microsoft.AspNetCore.Identity;

namespace EcommercialWebApp.Data.Models.Commons
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string? IdentificationNumber { get; set; }
    }
}
