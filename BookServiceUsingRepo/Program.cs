using BookServiceUsingRepo.Context;
using BookServiceUsingRepo.Exceptions;
using BookServiceUsingRepo.IRepo;
using BookServiceUsingRepo.REpo;
using log4net.Config;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BookServiceUsingRepo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
          };
      });

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BookDbContext>
                (op => op.UseSqlServer(builder.Configuration["ConnectionStrings:BookConnection"]));

            builder.Services.AddScoped<IBookRepo,BookRepo>();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();


            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));



            var app = builder.Build();

            if (app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/error");

            }
            else
            {
                app.UseExceptionHandler(_ => { });
            }
            // Configure the HTTP request pipeline.
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
