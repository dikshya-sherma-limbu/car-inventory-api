using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using car_inventory_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace car_inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly DynamoDBContext _dbContext;

        public ListingController(IConfiguration configuration)
        {
            var accessKey = configuration["AWSCredentials:AccessKey"];
            var secretKey = configuration["AWSCredentials:SecretKey"];
            var region = configuration["AWSCredentials:Region"];

            // Creating BasicAWSCredentials using accessKey and secretKey
            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            // Creating the DynamoDB client with the credentials and region
            var dynamoDbClient = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.GetBySystemName(region));

            // Initializing DynamoDBContext
            _dbContext = new DynamoDBContext(dynamoDbClient);
        }
        //// GET: /api/listing/Cars
        [HttpGet("GetCars")]  // This defines the GET route for /Cars
        public async Task<IActionResult> GetCars([FromQuery] string? makeFilter, [FromQuery] string? yearFilter)
        {
            var conditions = new List<ScanCondition>();

            if (!string.IsNullOrEmpty(makeFilter))
            {
                conditions.Add(new ScanCondition("Make", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, makeFilter));
            }

            if (yearFilter != null)
            {
                conditions.Add(new ScanCondition("Year", Amazon.DynamoDBv2.DocumentModel.ScanOperator.GreaterThanOrEqual, yearFilter));
            }
            var cars = await _dbContext.ScanAsync<Cars>(conditions).GetRemainingAsync();

            return Ok(cars);
        }

        // POST: ListingController/Create
        [HttpPost("AddCar")]
        public async Task<IActionResult> Create([FromBody] Cars newCar)
        {
            try
            {
                // Validate the incoming car object
                if (newCar == null || string.IsNullOrEmpty(newCar.CarID))
                {
                    return BadRequest("Invalid car data. 'CarID' is required.");
                }

                // Save the car object to DynamoDB
                await _dbContext.SaveAsync(newCar);

                return Ok($"Car with ID '{newCar.CarID}' created successfully.");
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: ListingController/Details/5
        [HttpGet("Details/{CarID}/{Make}")]  // Define the specific route for this action
        public async Task<IActionResult> Details(String CarID, string Make)
        {
            // Fetch the item from the database by ID
            var car = await _dbContext.LoadAsync<Cars>(CarID,Make);

            // Check if the item exists
            if (car == null)
            {
                return NotFound($"Car with ID {CarID} not found.");
            }

            // Return the car details as an HTTP 200 response
            return Ok(car);
        }


        //// PATCH: ListingController/Update/234dsfh42/Audi
        [HttpPatch("Update/{CarID}/{Make}")]
        public async Task<IActionResult> Update(String CarID, [FromBody] Cars updatedCar)
        {
            try
            {
                // Load the existing car entry based on CarID and Make (partition and sort keys)
                var existingCar = await _dbContext.LoadAsync<Cars>(CarID, updatedCar.Make);

                if (existingCar == null)
                {
                    return NotFound($"Car with ID {CarID} and Make '{updatedCar.Make}' not found.");
                }

                // Update the properties of the existing car entry
                existingCar.Color = updatedCar.Color;
                existingCar.DateAdded = updatedCar.DateAdded;
                existingCar.Description = updatedCar.Description;
                existingCar.Mileage = updatedCar.Mileage;
                existingCar.Model = updatedCar.Model;
                existingCar.PhotoURL = updatedCar.PhotoURL;
                existingCar.Price = updatedCar.Price;
                existingCar.Status = updatedCar.Status;
                existingCar.Year = updatedCar.Year;

                // Save the updated car entry back to the DynamoDB table
                await _dbContext.SaveAsync(existingCar);

                return Ok("Car updated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: ListingController/Delete/5
        [HttpDelete("Delete/{CarID}/{Make}")]  // Define the specific route for this action
        public async Task<IActionResult> Delete(String CarID,String Make)
        {
            try
            {
                // Load the existing car entry based on CarID and Make (partition and sort keys)
                var existingCar = await  _dbContext.LoadAsync<Cars>(CarID, Make);
                if (existingCar == null)
                {
                    return NotFound($"Car with ID {CarID} and Make '{Make}' not found.");

                }

                // Delete the existing car entry from the DynamoDB table
                await _dbContext.DeleteAsync(existingCar);
                return Ok($"Car deleted successfully ");
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
    }
}
