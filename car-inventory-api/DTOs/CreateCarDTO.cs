namespace car_inventory_api.DTOs
{
    public class CreateCarDTO
    {
        public string CarID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Mileage { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }
        public string Status { get; set; }
        public string DateAdded { get; set; }
    }
}
