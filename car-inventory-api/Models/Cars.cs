using Amazon.DynamoDBv2.DataModel;

namespace car_inventory_api.Models
{
    [DynamoDBTable("Cars")]
    public class Cars
    {
        [DynamoDBProperty("CarID")]
        public string CarID { get; set; }
        [DynamoDBProperty("Make")]
        public string Make { get; set; }
        [DynamoDBProperty("Color")]
        public string Color { get; set; }
        [DynamoDBProperty("DateAdded")]
        public string DateAdded { get; set; }
        [DynamoDBProperty("Description")]
        public string Description { get; set; }
        [DynamoDBProperty("Mileage")]
        public string Mileage { get; set; }
        [DynamoDBProperty("Model")]
        public string Model { get; set; }
        [DynamoDBProperty("PhotoURL")]
        public string PhotoURL { get; set; }
        [DynamoDBProperty("Price")]
        public string Price { get; set; }
        [DynamoDBProperty("Status")]
        public string Status { get; set; }
        [DynamoDBProperty("Year")]
        public string Year { get; set; }
    }
}
