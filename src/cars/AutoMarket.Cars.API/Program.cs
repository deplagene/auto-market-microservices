using AutoMarket.Cars.API.Profiles;
using AutoMarket.Cars.Application;
using AutoMarket.Cars.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication();
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<CarProfile>();
    options.AddProfile<BrandProfile>();
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
