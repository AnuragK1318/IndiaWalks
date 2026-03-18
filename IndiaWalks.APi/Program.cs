using IndiaWalks.APi.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IndiaWalks.APi.Mapper;
using Microsoft.Extensions.DependencyInjection;
using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Concrete;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IndiaWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"));
});
builder.Services.AddScoped<IRegion, RegionRepo>();
builder.Services.AddScoped<IWalksRepo, WalksRepo>();

builder.Services.AddAutoMapper(x => 
{
    x.AddProfile<AutoMapperProfile>();
});
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
