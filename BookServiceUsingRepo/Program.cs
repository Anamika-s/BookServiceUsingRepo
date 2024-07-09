using BookServiceUsingRepo.Context;
using BookServiceUsingRepo.IRepo;
using BookServiceUsingRepo.REpo;
using Microsoft.EntityFrameworkCore;

namespace BookServiceUsingRepo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BookDbContext>
                (op => op.UseSqlServer(builder.Configuration["ConnectionStrings:BookConnection"]));

            builder.Services.AddScoped<IBookRepo,BookRepo>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
