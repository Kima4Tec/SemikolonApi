using System.ComponentModel.DataAnnotations;

namespace Domain.Entities 
{ 
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
