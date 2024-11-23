using Amazon.DynamoDBv2.DataModel;

namespace car_inventory_api.Models
{
    [DynamoDBTable("Sales")]
    public class Sales
    {
        [DynamoDBHashKey]
        public required string SaleID { get; set; }

        [DynamoDBProperty("CarID")]
        public required string CarID { get; set; }

        [DynamoDBProperty("CustomerID")]
        public required  string customerID { get; set; }

        [DynamoDBProperty("SaleDate")]
        public required string SaleDate { get; set; }

        [DynamoDBProperty("SalePrice")]
        public required string SalePrice { get; set; }
        [DynamoDBProperty("SaleStatus")]
        public required string SaleStatus { get; set; }
        [DynamoDBProperty("PaymentMethod")]
        public required string PaymentMethod { get; set; }
    }
}
