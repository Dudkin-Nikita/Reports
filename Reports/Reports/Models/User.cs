using Microsoft.AspNetCore.Identity;

namespace Reports.Models
{
    public class User : IdentityUser
    {
        public DateTime LastSystemEnter { get; set; }
        public UserTypes UserType { get; set; }
    }
}
