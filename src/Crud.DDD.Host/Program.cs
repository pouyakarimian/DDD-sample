using Crud.DDD.Application;
using Crud.DDD.Core;
using Crud.DDD.Host.Exctentions;
using Crud.DDD.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .RegsiterHostModule(builder.Configuration)
    .RegisterInfrastructure(builder.Configuration)
    .RegisterCoreLayer(builder.Configuration)
    .RegisterApplicationLayer(builder.Configuration);

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
