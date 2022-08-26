using System.Text.Json;
using MongoDB.Driver;
using Play.Catalog.Service.Configuration.MongoDbSettings;
using Play.Catalog.Service.Repository;
using Play.Catalog.Service.Entities;


var builder = WebApplication.CreateBuilder(args);

// setup json configuration
builder.Services.AddSingleton(serviceProvider =>
{
    var mongoSetting = builder.Configuration.GetSection(MongoDbSettings.Setting)
    .Get<MongoDbSettings>();

    var mongoClient = new MongoClient(mongoSetting.ConnectionString);
    return mongoClient.GetDatabase("Catalog");
});

builder.Services.AddSingleton<IRepository<Item>>(serviceProvider =>
{
    var database = serviceProvider.GetService<IMongoDatabase>();
    return new MongoRepository<Item>(database, "items");

});

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddJsonOptions(
        options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
