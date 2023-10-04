using FluentValidation.AspNetCore;
using Rusada.Core;
using Rusada.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationPersistenceServices(builder.Configuration, builder.Environment)
    .AddApplicationCoreDependencies();


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();


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
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});
app.UseAuthorization();

app.MapControllers();

app.Run();