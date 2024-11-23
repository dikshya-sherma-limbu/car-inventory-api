namespace car_inventory_api.Models;
using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Users")]
public class Users
{
    [DynamoDBHashKey]
    public required string UserID { get; set; }

    [DynamoDBProperty("username")]
    public required string Username { get; set; }

    [DynamoDBProperty("email")]
    public required string email { get; set; }

    [DynamoDBProperty("password")]
    public  required string password { get; set; }
}
