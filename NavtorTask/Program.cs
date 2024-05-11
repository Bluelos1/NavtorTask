using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using NavtorTask.Interface;
using NavtorTask.Service;
using NavtorTask.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    x.UseInlineDefinitionsForEnums();
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSingleton<IShipService, ShipService>();
builder.Services.AddSingleton<ITankerShipService, TankerShipService>();
builder.Services.AddSingleton<IContainerShipService, ContainerShipService>();
builder.Services.AddSingleton<ValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapControllers();
app.UseHttpsRedirection();


app.Run();
