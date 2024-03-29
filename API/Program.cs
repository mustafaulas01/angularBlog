using API.BusinessLayer.Abstract;
using API.BusinessLayer.Concrete;
using API.Data;
using API.Models.Other;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAngularConnectionString")));

builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<DomainSettings>();

var domainSettings=builder.Configuration.GetSection("DomainSettings").Get<DomainSettings>();

builder.Services.AddCors(options=> 
{
    options.AddPolicy("AllowOriginPolicy",builder=>builder.WithOrigins(domainSettings.ClientDomain));
}

);



var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(options=>options.WithOrigins(domainSettings.ClientDomain).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
