using DB.DataAccess;
using DB.DataAccess.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ThingsLoan.WebAPI.Configuration;
using ThingsLoan.WebAPI.Handlers;
using ThingsLoan.WebAPI.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// JWT Config
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

// Swagger implemented
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Services 
builder.Services.AddAuthorization();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();




// Authentication with JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

// Database Context
builder.Services.AddDbContext<ThingsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThingsConnection"))
);
var corspolicy = "mycorspolicy";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "http://localhost:5000").AllowAnyHeader().AllowAnyMethod();
                      });
});
var app = builder.Build();



// Created when I made the project.
if (app.Environment.IsDevelopment())
{
    // Swagger implemented on development environment only
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapGrpcReflectionService();
app.MapGrpcService<ReturnLoanService>();


app.MapControllers();
app.Run();
