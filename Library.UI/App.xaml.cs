using Library.UI.AppFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using Library.UI.Services.LibraryService;

namespace Library.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>


    public partial class App : Application {
        public IServiceProvider? ServiceProvider { get; set; }
        public IConfiguration? Configuration { get; set; }
        public static IHost AppHost { get; private set; } = default!;

        public App() {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
                var exception = (Exception)args.ExceptionObject;
                //MessageBox.Show(exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var builder = new ConfigurationBuilder();
            if (!string.IsNullOrWhiteSpace(assemblyFolder)) {
                builder.SetBasePath(assemblyFolder);
            }
            builder.AddJsonFile("appsettings.json");
            Configuration = builder.Build();


            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug() // VS output debug window
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();


            Log.Information("------------------------------------ Starting LibraryUI WPF app -------------------------------------");
            Log.Information($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable(ApplicationConstants.ASPNETCORE_ENVIRONMENT)}");
            Log.Information($"LibraryUI version: {Assembly.GetExecutingAssembly().GetName().Version}");

            var hostBuilder = Host.CreateDefaultBuilder();
            hostBuilder.ConfigureLogging(logging => { logging.AddSerilog(Log.Logger); });
            AppHost = hostBuilder
                .UseSerilog()
                .ConfigureServices((hostContext, services) => {
                    ConfigureServices(hostContext, services);
                })
                .Build();
        }

        private void ConfigureServices(HostBuilderContext builder, IServiceCollection services) {
            services.AddHttpClient();

            services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

            services.AddSingleton<Serilog.ILogger>(provider => { return Log.Logger; });
            services.AddTransient<IMainWindow, MainWindow>();
            services.AddTransient<UsersPage>();
            services.AddTransient<AddUserDialog>();
            services.AddTransient<BooksPage>();
            services.AddTransient<AddBookDialog>();
            services.AddTransient<CheckoutBooksPage>();
            services.AddTransient<CheckinBooksPage>();
            services.AddScoped<ILibraryService, LibraryService>();
        }
        protected override async void OnStartup(StartupEventArgs e) {
            await AppHost!.StartAsync();
            var mainWindow = AppHost.Services.GetRequiredService<IMainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e) {
            await Task.CompletedTask;
            Log.Information("Stopping WPF app");
            Log.Information("------------------------------------ Exiting WPF app -------------------------------------");
            base.OnExit(e);

        }


    }
}
