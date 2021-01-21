using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterView<TView, TViewModel>(this IServiceCollection services)
            where TView : FrameworkElement, new() where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient(provider =>
                {
                    var view = new TView
                    {
                        DataContext = provider.GetRequiredService<TViewModel>()
                    };
                    return view;
                }
            );
        }
    }
}