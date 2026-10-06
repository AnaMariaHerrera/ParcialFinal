using ParcialFinal.ViewModels;

namespace ParcialFinal.Views
{
    public partial class DetallePage : ContentPage
    {
        public DetallePage(DetalleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
