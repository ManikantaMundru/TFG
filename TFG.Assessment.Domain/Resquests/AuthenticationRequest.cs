using System.ComponentModel.DataAnnotations;

namespace TFG.Assessment.Domain.Requests
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
