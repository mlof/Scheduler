using System;
using System.IO;
using System.Windows;
using Chroniton;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scheduler.Extensions;
using Scheduler.ViewModels;
using Scheduler.Views;

namespace Scheduler
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {
        private readonly IHost _host;

        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;


            _host = new HostBuilder()
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", false, true);
                    }
                )
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime()
                .Build();
            _host.StartAsync().GetAwaiter().GetResult();

            var singularity = Singularity.Instance;
            singularity.Start();

        }

        /// <inheritdoc />
        public void Dispose()
        {
            _host?.Dispose();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var mainWindow =
                _host.Services.GetRequiredService<ShellWindow>();


            mainWindow.Show();
            mainWindow.Closed += MainWindowOnClosed;


        }

        private void MainWindowOnClosed(object? sender, EventArgs e)
        {
            Shutdown();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {

            services.AddSingleton<ISingularity, Singularity>(serviceProvider => Singularity.Instance);

            services.RegisterView<ShellWindow, ShellWindowViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Dispose();

            base.OnExit(e);
        }
    }
}