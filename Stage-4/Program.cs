using FluentValidation;
using FluentValidation.AspNetCore;
using Stage_4.Validators;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICES SECTION ---

// 1. Controllers add karein
builder.Services.AddControllers();

// 2. FluentValidation ko register karein (Automatic validation ke liye)
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TodoCreateDtoValidator>();

// 3. OpenAPI aur Swagger documentation setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- MIDDLEWARE PIPELINE SECTION ---

// 4. Development environment mein Swagger UI enable karein
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger(); // Swagger JSON generate karne ke liye
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = "swagger"; // Browser mein /swagger likhne se khulega
	});
}

app.UseHttpsRedirection();

// 5. Routing aur Controllers ko map karein
app.UseAuthorization();
app.MapControllers();

app.Run();
