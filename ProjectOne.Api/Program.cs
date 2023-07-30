using AutoMapper;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectOne.Api.Controllers;
using ProjectOne.Api.Middleware;
using ProjectOne.Common;
using ProjectOne.Common.Helpers;
using ProjectOne.Data.DbEntities;
using ProjectOne.Repository;
using ProjectOne.Service;
using System.Text;
using Wkhtmltopdf.NetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectOneContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<UserResult, UserResult>();
builder.Services.AddTransient<CommandResult, CommandResult>();
builder.Services.AddTransient<ProjectOneContext, ProjectOneContext>();
builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(ProjectOne.Repository.Class1Repository)
    , typeof(ProjectOne.Service.Class1Service), typeof(ProjectOne.Api.Controllers.CheckController)).AddClasses().AsMatchingInterface());
var profiles = typeof(WeatherForecastController).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    foreach (var profile in profiles)
    {
        cfg.AddProfile(profile);
    }
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddWkhtmltopdf();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseMiddleware<ProjectOne.Api.Middleware.AuthorizationMiddleware>();
app.MapControllers();
//app.UseAuthorizationMiddleware();
app.Run();
