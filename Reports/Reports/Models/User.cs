using Microsoft.AspNetCore.Identity;

namespace Reports.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public DateTime LastSystemEnter { get; set; }
        public UserTypes UserType { get; set; }
    }
}