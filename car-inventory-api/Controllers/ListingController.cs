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
        [HttpGet("Cars")]  // This defines the GET route for /Cars
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

        // GET: ListingController/Details/5
        [HttpGet("Details/{id}")]  // Define the specific route for this action
        public ActionResult Details(int id)
        {
            return Ok();
        }

        // GET: ListingController/Create
        [HttpGet("Create")]  // Define the specific route for this action
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: ListingController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return Ok();
        //    }
        //}

        // GET: ListingController/Edit/5
        [HttpGet("Edit/{id}")]  // Define the specific route for this action
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        //// POST: ListingController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return Ok();
        //    }
        //}

        // GET: ListingController/Delete/5
        [HttpGet("Delete/{id}")]  // Define the specific route for this action
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // POST: ListingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}
