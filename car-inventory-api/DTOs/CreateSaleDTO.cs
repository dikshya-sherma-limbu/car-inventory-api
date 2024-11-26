namespace car_inventory_api.DTOs
{
    public class CreateSaleDTO
    {
        public string CarID { get; set; }
        public string CustomerID { get; set; }
        public string SaleDate { get; set; }
        public string SalePrice { get; set; }
        public string SaleStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}
