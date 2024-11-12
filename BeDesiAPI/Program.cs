using BeDesi.Core.Repository;
using BeDesi.Core.Repository.Contracts;
using BeDesi.Core.Services;
using BeDesi.Core.Services.Contracts;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});


// Read the connection string from appsettings.json
string connectionString = builder.Configuration.GetConnectionString("BeDesiDB");


builder.Services.AddSingleton<IBusinessRepository>(provider => new BusinessRepository(connectionString));
builder.Services.AddSingleton<ILocationRepository>(provider => new LocationRepository(connectionString));

builder.Services.AddSingleton<IBusinessService, BusinessService>();
builder.Services.AddSingleton<ILocationService, LocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
