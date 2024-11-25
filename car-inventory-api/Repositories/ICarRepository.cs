using Amazon.DynamoDBv2.DataModel;
using car_inventory_api.Models;

namespace car_inventory_api.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Cars>> GetCarsAsync(string? makeFilter, string? yearFilter);
        Task AddCarAsync(Cars car);
        Task<Cars> GetCarDetailsAsync(string carId, string make);
        Task UpdateCarAsync(Cars car);
        Task DeleteCarAsync(string carId, string make);
 
    }
}
