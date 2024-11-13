using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;


    namespace ExpenseApp.Models
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string MobileNumber { get; set; } = string.Empty;

            public string? Token { get; set; }
            public string? RefreshToken { get; set; }
            public DateTime RefreshTokenExpiryTime { get; set; }
            public DateTime CreatedOn { get; set; }
            public UserType UserType { get; set; } = UserType.NONE;

            public List<GroupUser>? GroupUsers { get; set; }
        }

  
    
    }


