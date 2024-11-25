using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace car_inventory_api.AWSConfiguration
{
    public class AWSDynamoDbConfig
    {
        private readonly IConfiguration _configuration;

        public AWSDynamoDbConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DynamoDBContext CreateDynamoDbContext()
        {
            var accessKey = _configuration["AWSCredentials:AccessKey"];
            var secretKey = _configuration["AWSCredentials:SecretKey"];
            var region = _configuration["AWSCredentials:Region"];

            // Creating BasicAWSCredentials using accessKey and secretKey
            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            // Creating the DynamoDB client with the credentials and region
            var dynamoDbClient = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.GetBySystemName(region));

            // Initializing DynamoDBContext
            return new DynamoDBContext(dynamoDbClient);
        }
    }
}
