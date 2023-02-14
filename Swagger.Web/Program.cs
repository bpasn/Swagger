using Microsoft.Extensions.DependencyInjection;
using Swagger.Core;
using Swagger.Core.AuthService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbClient, DbClient>();
builder.Services.AddSingleton<IDbClientAuth, DbClientAuth>();
builder.Services.Configure<StoreDbConfig>(builder.Configuration);

//auth services to
//builder.Services.Configure<AuthstoreDbConfig>(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IBookServices, BookServices>();
builder.Services.AddTransient<IAuthService, AuthServices>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
