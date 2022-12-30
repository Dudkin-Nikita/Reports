using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAdmin
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime LastSystemEnter { get; set; }
        public UserTypes UserType { get; set; } = UserTypes.Fresh;
        public string UserName { get; set; } = string.Empty;
        public string NormalizedUserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public byte EmailConfirmed { get; set; } = 1;
        public string PasswordHash { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public byte PhoneNumberConfirmed { get; set; } = 0;
        public byte TwoFactorEnabled { get; set; } = 0;
        public DateTimeOffset? LockoutEnd { get; set; }
        public byte LockoutEnabled { get; set; } = 0;
        public int AccessFailedCount { get; set; } = 0;          
    }
}