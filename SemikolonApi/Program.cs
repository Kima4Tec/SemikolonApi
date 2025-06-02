using Application.Dtos;
using Application.Interfaces.IArtists;
using Application.Interfaces.IAuthor;
using Application.Interfaces.IBook;
using Application.Interfaces.ICovers;
using Application.Interfaces.IUser;
using Application.Mappings;
using Application.Services;
using Application.Validation;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using static Infrastructure.Repositories.IRepository;


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
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IArtistService, ArtistService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICoverService, CoverService>();
            builder.Services.AddScoped<ICoverRepository, CoverRepository>();
            builder.Services.AddScoped<ICoverService, CoverService>();
            builder.Services.AddScoped<IValidator<CreateAuthorDto>, AuthorDtoValidator>();
            builder.Services.AddScoped<BookDtoValidator>();
            builder.Services.AddAutoMapper(typeof(AuthorProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ArtistProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            //adding JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
               };
           });

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
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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
