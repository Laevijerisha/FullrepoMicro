using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_wasteManagementWebapi.Model
{
    public class Center
    {
        [Key]
        public int CenterId { get; set; }
        public string? Center_Name { get; set; }
        public string? CenterLocation { get; set; }
        public string? Phone_number { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
     
        public string ? personalEmail { get; set; }
             

        

    }
}
