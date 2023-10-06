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

// builder.Services.AddAuthentication(op =>
//     {
//         // op.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
//         // op.DefaultChallengeScheme = GoogleDefaults.AuthorizationEndpoint;
//         // op.DefaultScheme = GoogleDefaults.AuthorizationEndpoint;
//         op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = false,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = false,
//             ValidIssuer = "https://accounts.google.com", // Replace with your issuer
//             // ValidAudience = "m", // Replace with your audience
//             // IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(""))
//         };
//     });
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();