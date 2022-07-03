using System.ComponentModel.DataAnnotations;

namespace MicroservicesDemo.Users.Queries.Authentication
{
    public class AuthenticateResult
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public RoleType Role { get; set; }
        [Required]
        public string Email { get; set; }
    }
}