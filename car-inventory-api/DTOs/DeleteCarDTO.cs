namespace car_inventory_api.DTOs
{
    public class DeleteCarDTO
    {
        public string CarID { get; set; }
        public string Make { get; set; }  // Optional: Include the sort key if needed
    }

}
