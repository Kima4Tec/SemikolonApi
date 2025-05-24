using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Application.Services;
namespace SemikolonApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*-------------------------------Add services to the container.------------------------------------*/

            //Preparing app to handle[Authorize] attributes on controllers or endpoints.
            builder.Services.AddAuthorization();

            // Adding controller services
            builder.Services.AddControllers();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            //Adding CORS - Cross-Origin Resource Sharing. Allow all origins, methods and headers for communicating with application
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add Swagger/OpenAPI support (for API documentation and testing via Swagger UI)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Semikolon API",
                    Version = "v1",
                    Description = "API for Semikolon application"
                });
                var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    s.IncludeXmlComments(xmlPath);
                }
                var xmlDomainFile = "Domain.xml";
                var xmlDomainPath = Path.Combine(AppContext.BaseDirectory, xmlDomainFile);
                if (File.Exists(xmlDomainPath))
                {
                    s.IncludeXmlComments(xmlDomainPath);
                }
                var xmlApplicationFile = "Domain.xml";
                var xmlApplicationPath = Path.Combine(AppContext.BaseDirectory, xmlApplicationFile);
                if (File.Exists(xmlApplicationPath))
                {
                    s.IncludeXmlComments(xmlApplicationPath);
                }
            });

            var app = builder.Build();
            app.UseCors("AllowFrontend");

            /*----------------------------Configure the HTTP request pipeline..---------------------------------*/

            // Enable Swagger middleware to serve generated JSON document and the Swagger UI for frontend
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SemikolonApi v1"));
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();



            app.Run();
        }
    }
}
