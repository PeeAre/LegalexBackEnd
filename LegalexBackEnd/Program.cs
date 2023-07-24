using LegalexBackEnd.Services.Senders;
using Microsoft.EntityFrameworkCore;

namespace LegalexBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddSenders();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            var app = builder.Build();

            if (app.Environment.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                                                              .AllowAnyMethod()
                                                              .AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}