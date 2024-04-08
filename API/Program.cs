using API.BusinessLayer.Abstract;
using API.BusinessLayer.Concrete;
using API.Data;
using API.Models.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.
UseSqlServer(builder.Configuration.GetConnectionString("BlogAngularConnectionString")).UseLazyLoadingProxies());

builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<IBlogPostService,BlogPostManager>();
builder.Services.AddScoped<IImageService,ImageManager>();
builder.Services.AddScoped<DomainSettings>();

var domainSettings=builder.Configuration.GetSection("DomainSettings").Get<DomainSettings>();

builder.Services.AddCors(options=> 
{
    options.AddPolicy("AllowOriginPolicy",builder=>builder.WithOrigins(domainSettings.ClientDomain));
}

);


var app = builder.Build();

    app.UseStaticFiles();

     app.UseStaticFiles(new StaticFileOptions() {
                FileProvider =  new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
                RequestPath = new PathString("/images")
        });



app.UseCors(options=>options.WithOrigins(domainSettings.ClientDomain).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAuthorization();



app.MapControllers();

app.Run();
