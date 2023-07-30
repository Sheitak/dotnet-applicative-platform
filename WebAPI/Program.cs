using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.Data;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:7167")
                .AllowAnyMethod()
                .AllowAnyHeader()
            ;
        });
});

// Add services to the container.
builder.Services.AddControllers();
/*
builder.Services.AddControllers().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
*/
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddDbContext<SignatureContext>(options => options.UseInMemoryDatabase("SignatureContext"));
builder.Services.AddDbContext<SignatureContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBConnection")
    )
);
builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<SignatureContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "3iL .NET Applicative Platform API",
        Description = "Web API for connecting various client and build applicative .NET Platform.",
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.3il-ingenieurs.fr/")
        },
        License = new OpenApiLicense
        {
            Name = "GPLv3 License",
            Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.en.html")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

var app = builder.Build();

app.UseCors();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Custom extension method to seed the DB
    app.UseItToSeedSqlServer();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// TODO: Comment� pour le probl�me li� aux v�rifications de certificats de MAUI
// https://stackoverflow.com/questions/71047509/trust-anchor-for-certification-path-not-found-in-a-net-maui-project-trying-t/71196389#71196389
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
