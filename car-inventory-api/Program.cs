using Amazon.DynamoDBv2.DataModel;
using car_inventory_api.AWSConfiguration;
using car_inventory_api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AWSDynamoDbConfig and DynamoDBContext
builder.Services.AddSingleton<AWSDynamoDbConfig>(); // Register your custom AWS config

// Register the DynamoDBContext as Scoped
builder.Services.AddScoped<DynamoDBContext>(sp =>
{
    var awsConfig = sp.GetRequiredService<AWSDynamoDbConfig>(); // Get the config instance
    return awsConfig.CreateDynamoDbContext(); // Create DynamoDBContext from custom config
});

// Register the ICarRepository interface and CarRepository class as Scoped
builder.Services.AddScoped<ICarRepository, CarRepository>();

// Configure AutoMapper for DTOs and Models
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
