using IndiaWalks.APi.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IndiaWalks.APi.Mapper;
using Microsoft.Extensions.DependencyInjection;
using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add serilog 
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();
//THis will clear any providers if the were their before
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo { Title = "IndiaWalksApi", Version = "v1" });
    Options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="Oauth2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<IndiaWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conStr"));
});
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("authStr"));
});
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("IndiaWalks")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddScoped<IRegion, RegionRepo>();
builder.Services.AddScoped<IWalksRepo, WalksRepo>();
builder.Services.AddScoped<ITokenRepo, TokenRepo>();

builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<AutoMapperProfile>();
});

var JwtKey = builder.Configuration["Jwt:Key"]; 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        AuthenticationType = "Jwt",
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudiences = new[] { builder.Configuration["Jwt:Audience"] },
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtKey))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
