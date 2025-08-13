using MangaReads.Services;
using MangaReads.Interfaces;
using MangaReads.Data;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{ 
	options.AddPolicy("AllowAll", 
			builder => 
			{
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
			});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<MangaReadsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure services based on storage provider
var storageProvider = builder.Configuration.GetValue<string>("StorageProvider");

builder.Services.AddSingleton<IMangaService, MangaDex>();

if (storageProvider == "PostgreSQL")
{
    builder.Services.AddScoped<IUserService, UserPostgreSqlService>();
    builder.Services.AddScoped<IMangaStorageService, MangaPostgreSqlService>();
}
else
{
    // Default to JSON services
    builder.Services.AddSingleton<IUserService, UserJsonService>();
    builder.Services.AddSingleton<IMangaStorageService, MangaJsonService>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
