using CapstoneProject.Database.Model.Meta;
using CapstoneProject.Infrastructure;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using CapstoneProject.Business.Services;
using CapstoneProject.DTO.Request.Email;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationBuilder configurationBuilder = new();

configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = configurationBuilder.Build();
// Add services to the container.

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
            ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState, 422);

            return new ObjectResult(problemDetails)
            {
                StatusCode = 422
            };
        }
    );

builder.Services.AddEndpointsApiExplorer();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("petpal-c6642-firebase-adminsdk-45893-ad2d528fff.json")    
});

/*builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();*/

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureServiceManager();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "v1",
        Version = "v1",
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here enter JWT token."
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Petpal");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
/*
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/.well-known/pki-validation"))
    {
        // Allow the request to bypass authentication
        await next();
        return; // Exit middleware chain
    }
    await next(); // Continue to the next middleware
});*/

app.UseHttpsRedirection();
    
app.UseCors("AllowSpecificOrigin");

// Allow zerossl to read static file
/*app.UseStaticFiles();
*/
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
