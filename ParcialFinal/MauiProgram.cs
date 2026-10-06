using Microsoft.Extensions.Logging;
using ParcialFinal.Services;
using ParcialFinal.ViewModels;
using ParcialFinal.Views;

namespace ParcialFinal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servicios
            builder.Services.AddSingleton<BaseDatosService>();
            builder.Services.AddSingleton<ClimaService>();

            // ViewModels
            builder.Services.AddTransient<ListaViewModel>();
            builder.Services.AddTransient<DetalleViewModel>();

            // Pantallas
            builder.Services.AddTransient<ListaPage>();
            builder.Services.AddTransient<DetallePage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
