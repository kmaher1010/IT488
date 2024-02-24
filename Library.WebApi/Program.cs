using Library.WebApi.Services.LibraryRepository;
using Microsoft.OpenApi.Models;

namespace Library.WebApi {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLibraryServices();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.WebApi", Version = "v1" });
            });

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.

            using (var scope = app.Services.CreateScope()) {
                var dbcontext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                dbcontext.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}