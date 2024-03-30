using E_wasteManagementWebapi.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_wasteManagementWebapi.Model
{
    public class E_WasteItem
    {
        [Key]
        public int ItemId { get; set; }

        public string? ItemName { get; set; }

        [NotMapped]
        public IFormFile? ItemImage { get; set; }

        public string? UniqueFileName { get; set; }
        public int ItemQuantity { get; set; }
        public string? ItemCondition { get; set; }
        public string? ItemLocation { get; set; }
        public string? RequestStatus { get; set; } = "Pending";
        public string? ApprovedItemStatus { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User Users { get; set; }



    }
}
