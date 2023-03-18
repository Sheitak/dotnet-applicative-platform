using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddDbContext<SignatureContext>(options => options.UseInMemoryDatabase("SignatureContext"));
builder.Services.AddDbContext<SignatureContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DBConnection")
    )
);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
