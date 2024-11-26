using Amazon.DynamoDBv2.DataModel;
using car_inventory_api.Models;

namespace car_inventory_api.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DynamoDBContext _context;

        public SalesRepository(DynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sales>> GetAllSalesAsync()
        {
            var query = _context.ScanAsync<Sales>(new List<ScanCondition>());
            var sales = await query.GetRemainingAsync();
            return sales;
        }

        public async Task<Sales> GetSaleDetailsAsync(string saleID)
        {
            return await _context.LoadAsync<Sales>(saleID);
        }

        public async Task CreateSaleAsync(Sales sale)
        {
            await _context.SaveAsync(sale);
        }

        public async Task UpdateSaleAsync(Sales sale)
        {
            await _context.SaveAsync(sale);
        }

        public async Task DeleteSaleAsync(string saleID)
        {
            await _context.DeleteAsync<Sales>(saleID);
        }
    }
}
