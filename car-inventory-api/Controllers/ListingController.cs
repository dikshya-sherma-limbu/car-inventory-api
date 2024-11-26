using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using car_inventory_api.AWSConfiguration;
using car_inventory_api.DTOs;
using car_inventory_api.Models;
using car_inventory_api.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace car_inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        //private readonly DynamoDBContext _dbContext;

        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;


        public ListingController( ICarRepository carRepository,IMapper mapper)
        {
            
            // Injecting the CarRepository
            _carRepository = carRepository;
            // Injecting the IMapper to map DTOs to Models and vice versa
            _mapper = mapper;
        }

        //// GET: /api/listing/Cars
        [HttpGet("GetCars")]  // This defines the GET route for /Cars
        public async Task<IActionResult> GetCars(string? makeFilter, string? yearFilter)
        {
            // Call the repository to get the list of cars based on filters
            //listCarDTO.Make, listCarDTO.Year
            var cars = await _carRepository.GetCarsAsync(makeFilter,yearFilter);

            // Return the result
            return Ok(cars);
        }

        // POST: ListingController/Create
        [HttpPost("AddCar")]
        public async Task<IActionResult> Create([FromBody] CreateCarDTO createCarDTO)
        {
            try
            {
                // Validate the incoming car object
                if (createCarDTO == null || string.IsNullOrEmpty(createCarDTO.CarID))
                {
                    return BadRequest("Invalid car data. 'CarID' is required.");
                }

                // Map CreateCarDTO to Cars model
                var newCar = _mapper.Map<Cars>(createCarDTO);

                // Call the repository to add the new car to DynamoDB
                await _carRepository.AddCarAsync(newCar);

                return Ok($"Car with ID '{createCarDTO}' created successfully.");
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
            // Call the repository to get car details
            var car = await _carRepository.GetCarDetailsAsync(CarID, Make);

            if (car == null)
            {
                return NotFound($"Car with ID {CarID} and Make '{Make}' not found.");
            }

            return Ok(car);
        }


        //// PATCH: ListingController/Update/234dsfh42/Audi
        [HttpPatch("Update/{CarID}/{Make}")]
        public async Task<IActionResult> Update(String CarID, [FromBody] Cars updatedCar)
        {
            try
            {
                // Call the repository to update car details
                var existingCar = await _carRepository.GetCarDetailsAsync(CarID, updatedCar.Make);

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

                // Call the repository to save the updated car
                await _carRepository.UpdateCarAsync(existingCar);

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
        public async Task<IActionResult> Delete([FromBody] DeleteCarDTO deleteCarDTO)
        {
            try
            {
                // Delete the existing car entry from the DynamoDB table
                await _carRepository.DeleteCarAsync(deleteCarDTO.CarID, deleteCarDTO.Make);
                return Ok($"Car deleted successfully ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
