using System.Text.Json.Serialization;

namespace car_inventory_api.DTOs
{
    public class ListCarDTO
    {
        [JsonIgnore] // This will hide CarID in the JSON response
        public string CarID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
    }
}
