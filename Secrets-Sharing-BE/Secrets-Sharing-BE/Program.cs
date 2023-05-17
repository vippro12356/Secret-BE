using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Secrets_Sharing_BE;
using Secrets_Sharing_BE.Interfaces.Repositories;
using Secrets_Sharing_BE.Interfaces.Services;
using Secrets_Sharing_BE.Repositories;
using Secrets_Sharing_BE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(option =>
{
    option.UseSqlServer("Data Source=LAPTOP-CVUOF421;Initial Catalog=Data;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.Authority = "http://localhost:60893";
        option.TokenValidationParameters.ValidateAudience = false;
        option.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
        option.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization();
IdentityModelEventSource.ShowPII = true;
builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

//regist repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ITextDataRepository), typeof(TextDataRepository));
builder.Services.AddScoped(typeof(IFileDataRepository), typeof(FileDataRepository));
//regist service
builder.Services.AddScoped(typeof(IService), typeof(Service));
builder.Services.AddScoped(typeof(ITextDataService), typeof(TextDataService));
builder.Services.AddScoped(typeof(IFileDataService), typeof(FileDataService));
builder.Services.AddScoped(typeof(IUserRepository),typeof(UserRepository)); 
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
