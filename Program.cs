using Licencia___PF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<LicenciaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LicenciaConnection"));
});

/*
builder.Services.AddDbContext<LicenciaContext>(options =>
    options.UseSqlServer("Server=DESKTOP-BIVD7J7\\SQLEXPRESS;Database=LicenciasDB;Trusted_Connection=True;TrustServerCertificate=True;"));
*/
builder.Services.AddControllers();


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
