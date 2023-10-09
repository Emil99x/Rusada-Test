using FluentValidation.AspNetCore;
using Rusada.API.Filters;
using Rusada.API.middlewares;
using Rusada.Core;
using Rusada.Infrastructure;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationPersistenceServices(builder.Configuration, builder.Environment)
    .AddApplicationCoreDependencies();


builder.Services.AddControllers(config =>
{
   // config.Filters.Add<ModalValidatorFilter>();
    config.Filters.Add<ResponseMappingFilter>();
});

builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) =>
{
    config.MinimumLevel.Debug();
    config.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    config.Enrich.FromLogContext();
    config.WriteTo.Console();
    config.WriteTo.File(
        Path.Combine("LogFiles", "Application", "diagnostics-.txt"),
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 10 * 1024 * 1024,
        flushToDiskInterval: TimeSpan.FromSeconds(1),
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:l} {Properties:j} {NewLine}"
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseApiExceptionHandler();

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});
app.UseAuthorization();

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();