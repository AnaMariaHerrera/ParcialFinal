using ParcialFinal.ViewModels;

namespace ParcialFinal.Views
{
    public partial class ListaPage : ContentPage
    {
        private readonly ListaViewModel viewModel;

        public ListaPage(ListaViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        // Cada vez que se muestra la pantalla se recarga la lista
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.CargarLugaresCommand.Execute(null);
        }
    }
}
