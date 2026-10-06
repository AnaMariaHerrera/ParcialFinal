using ParcialFinal.Views;

namespace ParcialFinal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registro de la ruta de la pantalla de detalle
            Routing.RegisterRoute(nameof(DetallePage), typeof(DetallePage));
        }
    }
}
