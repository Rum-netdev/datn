using EcommercialWebApp.Core.Helpers.Email;
using EcommercialWebApp.Core.Helpers.Email.Models;
using EcommercialWebApp.Data;
using EcommercialWebApp.Data.Authentication;
using EcommercialWebApp.Data.Helpers;
using EcommercialWebApp.Data.Models.Commons;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Identity Configuration 
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = true;

    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var jwtConfiguration = builder.Configuration.GetSection("JwtConfiguration")
    .Get<JwtConfiguration>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = jwtConfiguration.ValidAudience,
            ValidIssuer = jwtConfiguration.ValidIssuer,
            ValidateAudience = jwtConfiguration.ValidateAudience,
            ValidateIssuer = jwtConfiguration.ValidateIssuer,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SignInKey))
        };
    });

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IBroker).Assembly));
builder.Services.AddAutoMapper(typeof(IBaseProfileTransient).Assembly);

builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add email configuration for config smtp later
var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);
builder.Services.AddTransient<IBroker, Broker>();

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "EcommercialWebAppAPI",
        Version = "v1"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();
//app.MapRazorPages();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommercialWebAppAPI v1");
});


var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if(context != null)
{
    Seed.SeedData(context);
}

app.Run();
