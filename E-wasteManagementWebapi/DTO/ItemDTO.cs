namespace E_wasteManagementWebapi.DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }

        public string? ItemName { get; set; }
        public IFormFile? ItemImage { get; set; }

        public string? UniqueFileName { get; set; }
        public int ItemQuantity { get; set; }
        public string? ItemCondition { get; set; }
        public string? ItemLocation { get; set; }
        public string? RequestStatus { get; set; } = "Pending";
        public string? ApprovedItemStatus { get; set; } = "None";

        public int UserId { get; set; }
    }
}
