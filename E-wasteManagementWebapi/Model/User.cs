using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_wasteManagementWebapi.Model
{
    public class User
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

        
      

    }

}
