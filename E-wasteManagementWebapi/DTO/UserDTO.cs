using E_wasteManagementWebapi.Model;
using System.ComponentModel.DataAnnotations;

namespace E_wasteManagementWebapi.DTO
{
    public class UserDTO
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]

        public string? Password { get; set; }
        [Required]

        public string? Cpassword { get; set; }

        public int Item_Id { get; set; }    
    }
}
