using System.Text;
using API.BusinessLayer.Abstract;
using API.BusinessLayer.Concrete;
using API.Data;
using API.DataLayer;
using API.Models.Other;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options=>options.
UseSqlServer(builder.Configuration.GetConnectionString("BlogAngularConnectionString")).UseLazyLoadingProxies());

builder.Services.AddDbContext<AuthDbContext>(options=> {

options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAngularConnectionString"));

});

builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<IBlogPostService,BlogPostManager>();
builder.Services.AddScoped<IImageService,ImageManager>();
builder.Services.AddScoped<ITokenService,TokenManager>();   
builder.Services.AddScoped<DomainSettings>();

builder.Services.AddIdentityCore<IdentityUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("AngularBlog")
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options=> {

options.Password.RequireDigit = false;
options.Password.RequireLowercase = false;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequiredLength=6;
options.Password.RequireUppercase = false;
options.Password.RequiredUniqueChars=1;

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=> {


options.TokenValidationParameters=new TokenValidationParameters
{
AuthenticationType="Jwt",
ValidateIssuer=true,
ValidateAudience=true,
ValidateLifetime=true,
ValidateIssuerSigningKey=true,
ValidIssuer= builder.Configuration["Jwt:Issuer"],
ValidAudience=builder.Configuration["Jwt:Audience"],
IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))



};

});


var domainSettings=builder.Configuration.GetSection("DomainSettings").Get<DomainSettings>();

builder.Services.AddCors(options=> 
{
    options.AddPolicy("AllowOriginPolicy",builder=>builder.WithOrigins(domainSettings.ClientDomain));
});



var app = builder.Build();

    app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions() {
               FileProvider =  new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
               RequestPath = new PathString("/images")
       });



app.UseCors(options=>options.WithOrigins(domainSettings.ClientDomain).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
