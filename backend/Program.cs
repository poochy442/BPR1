using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Cors;

using Backend.Helpers;
using Backend.DataAccess;

var builder = WebApplication.CreateBuilder(args);
{
	var services = builder.Services;

	services.AddCors();
	services.AddControllers();

	services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
	services.AddDbContext<DBContext>(opt =>
		opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
		//opt.UseInMemoryDatabase("Database")
	);

	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();
}

var app = builder.Build();

{
    app.UseSwagger();
    app.UseSwaggerUI();
	
	app.UseCors(x => x
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());

	app.UseMiddleware<JwtMiddleware>();

	app.MapControllers();
}

// app.UseHttpsRedirection();
// app.UseAuthorization();

app.Run();
