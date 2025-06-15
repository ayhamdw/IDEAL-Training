using Amazon.S3;
using Amazon;
using DataEntity.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using ProjectBase.Data;
using ProjectBase.Core;
using ProjectBase.Services.Helpers;
using ProjectBase.Services.IServices;
using ProjectBase.Services.Services;

using ProjectBase.Services.BackgroundServices;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectBase.Services.Interfaces;
using ProjectBase.Services;
using System.Threading.RateLimiting;
using Services.Helpers;

var builder = WebApplication.CreateBuilder(args);


// Get database connection string
string? connectionString = Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.DBConnectionString)
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Database connection string is missing.");
}

// **Configure database contexts**
builder.Services.AddDbContext<ProjectBaseContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddRoles<IdentityRole>() // If using role-based authentication
    .AddEntityFrameworkStores<ApplicationDbContext>();

// make rate limiting work for signin (5 attempts/15 mins)
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("login", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,       // 5 attempts
                Window = TimeSpan.FromMinutes(15) // per 15 mins
            }));
});



var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});


// **Register application services**
builder.Services.AddScoped<IAwsServices, AwsServices>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoriesService, CategoriesServices>();
builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
//** Promotion Service
builder.Services.AddScoped<IPromotionService, PromotionService>();


// **Add HttpClient support**
builder.Services.AddHttpClient();


builder.Services.AddSingleton<IAmazonS3>(provider =>
{
    return new AmazonS3Client(
        EnvironmentVariableHelper.GetEnvironmentVariableOrDefault(Constants.EnvironmentVariables.AWSAccessKey, Constants.EnvironmentVariables.AWSAccessKeyDefualtValue),
        EnvironmentVariableHelper.GetEnvironmentVariableOrDefault(Constants.EnvironmentVariables.AWSSecretAccessKey, Constants.EnvironmentVariables.AWSSecretAccessKeyDefualtValue),
        new AmazonS3Config
        {
            ServiceURL = "https://s3.eu-central-1.amazonaws.com",
            RegionEndpoint = RegionEndpoint.EUCentral1
        });
});
    
// **Register Background Services**
builder.Services.AddSingleton<QueuedBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<QueuedBackgroundService>());

// **Add MVC and Localization**
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// **Enable Swagger for API documentation**
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder
            .WithOrigins(
                "https://ddd5pfz6qm4m6.cloudfront.net",     // Production frontend
                "https://localhost:3000",                   // Local React/Angular dev server (common)
                "https://localhost:4200",                   // Optional: Angular default port
                "https://localhost:5001"                    // Optional: ASP.NET Core SPA dev
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Only if you use cookies/auth
    });
});



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectBaseContext>();
    DataBaseScriptsHelper.HandleDataBaseScripts(db);
}
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseForwardedHeaders(); // Ensure correct headers are passed from Nginx
}

// login Rate Limiting
app.UseRateLimiter();

// **Enable Middleware and Security**
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowFrontend"); // ? Place after UseRouting()

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization();
// **Map API controllers**
app.MapControllers();

// **Run the application**
app.Run();
