using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Services.LibraryRepository {
    public static class LibraryExtensions {
        public static void AddLibraryServices(this IServiceCollection services) {
            services.AddDbContext<LibraryDbContext>(options => options.UseInMemoryDatabase("LibraryDatabase"));
            services.AddScoped<LibraryRepository>();
        }
    }
}
