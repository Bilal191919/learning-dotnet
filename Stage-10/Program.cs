using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Stage_4.Middleware;
using Stage_4.Middlewares; // Plural namespace (image_7d5dfd)
using Stage_7.Application;
using Stage_7.Domain;
using Stage_7.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. LOGGING
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

// 2. DATABASE (Error Fix: image_7cda7d)
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

// 3. IDENTITY & AUTH
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

// 4. PERFORMANCE & CACHING
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAppDbContext).Assembly));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. MIDDLEWARES (Ordering is key)
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

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