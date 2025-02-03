using AuthBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")  // Allow frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
