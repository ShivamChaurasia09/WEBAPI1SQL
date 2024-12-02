using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WEBAPI1SQL.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.Xml;

namespace WEBAPI1SQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
    securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the authorization : `Bearer generated-Jwt-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement

    {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new string[] { }

                    }
     });
});
            builder.Services.AddTransient<IEmployeeRepository, SQLEmployeeRepository>();
            builder.Services.AddDbContextPool<Appdbcontext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
