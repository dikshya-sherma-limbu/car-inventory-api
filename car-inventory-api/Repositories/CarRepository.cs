using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using car_inventory_api.Models;

namespace car_inventory_api.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly DynamoDBContext _dbContext;

        public CarRepository(DynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Implement the ICarRepository methods

        // Method to get all cars from the DynamoDB table
        public async Task<IEnumerable<Cars>> GetCarsAsync(string? makeFilter, string? yearFilter)
        {
            var conditions = new List<ScanCondition>();

            if (!string.IsNullOrEmpty(makeFilter))
            {
                conditions.Add(new ScanCondition("Make", ScanOperator.Equal, makeFilter));
            }

            if (!string.IsNullOrEmpty(yearFilter))
            {
                conditions.Add(new ScanCondition("Year", ScanOperator.GreaterThanOrEqual, yearFilter));
            }

            // Perform the scan operation with the conditions
            var cars = await _dbContext.ScanAsync<Cars>(conditions).GetRemainingAsync();
            return cars;
        }

        // Method to add a new car to the DynamoDB table
        public async Task AddCarAsync(Cars newCar)
        {

            // Save the car object to DynamoDB
            await _dbContext.SaveAsync(newCar);

        }

        public async Task DeleteCarAsync(string carId, string make)
        {
            var car = await _dbContext.LoadAsync<Cars>(carId, make);
            if (car != null)
            {
                await _dbContext.DeleteAsync(car);
            }else
            {
                throw new Exception("Car not found");
            }
        }

        // Method to get the details of a specific car
        public async Task<Cars> GetCarDetailsAsync(string CarID, string Make)
        {
            // Fetch the item from the database by ID
            var car = await _dbContext.LoadAsync<Cars>(CarID, Make);
            if (car != null)
            {
                return car;
            }
            else
            {
                throw new Exception($"Car not found with ID {CarID}");
            }
        }

        // Method to update the details of a car
        public async Task UpdateCarAsync(Cars car)
        {
            await _dbContext.SaveAsync(car);
        }
    }
}
