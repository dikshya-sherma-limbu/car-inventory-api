using car_inventory_api.Models;

namespace car_inventory_api.Repositories
{
    public interface ISalesRepository 
    {
        Task<IEnumerable<Sales>> GetAllSalesAsync();
        Task<Sales> GetSaleDetailsAsync(string saleID);
        Task CreateSaleAsync(Sales sale);
        Task UpdateSaleAsync(Sales sale);
        Task DeleteSaleAsync(string saleID);
    }

}
