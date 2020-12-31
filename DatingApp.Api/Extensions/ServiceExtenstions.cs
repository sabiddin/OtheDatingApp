using System.Text;
using DatingApp.Api.Data;
using DatingApp.Api.Interfaces;
using DatingApp.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DatingApp.Api.Extensions
{
    public static class ServiceExtenstions
    {
        public static IServiceCollection AddIdentityModel(this IServiceCollection services, IConfiguration config){
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
            return services;            
        }
          public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration config){
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });                                    
            return services;            
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration config){
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DatingApp.Api", Version = "v1" });
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                    {
                        In = ParameterLocation.Header, 
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey 
                    });
                    c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement 
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
                                new string[] { } 
                            } 
                        });
            });
            return services;
        }
    }
}