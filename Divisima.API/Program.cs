using Divisima.BL.Repositories;
using Divisima.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Divisima.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddDbContext<SQLContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Divisima API - Version 1",
                    Description = "Bu projede .net core 7.0 kullanýlmýþtýr",
                    TermsOfService = new Uri("http://www.cantacim.com/sozlesme"),
                    Contact = new OpenApiContact
                    {
                        Name = "Developer Ali",
                        Email = "ali@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT Licence",
                        Url = new Uri("http://www.cantacim.com/sozlesme")
                    }
                });
                sw.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,Assembly.GetExecutingAssembly().GetName().Name+".xml"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "izinVerilenOriginler",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:18489").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                                  });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5270",
                    ValidAudience = "n11",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BubenimözelsigninKey"))
                };
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("izinVerilenOriginler");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}