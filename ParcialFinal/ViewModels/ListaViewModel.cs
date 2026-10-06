using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ParcialFinal.Models;
using ParcialFinal.Services;
using ParcialFinal.Views;

namespace ParcialFinal.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly BaseDatosService baseDatos;

        public ObservableCollection<Lugar> Lugares { get; } = new();

        [ObservableProperty]
        private bool cargando;

        public ListaViewModel(BaseDatosService baseDatos)
        {
            this.baseDatos = baseDatos;
        }

        [RelayCommand]
        private async Task CargarLugares()
        {
            try
            {
                Cargando = true;
                Lugares.Clear();

                var lista = await baseDatos.ObtenerLugares();
                foreach (var lugar in lista)
                    Lugares.Add(lugar);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudieron cargar los lugares: " + ex.Message, "OK");
            }
            finally
            {
                Cargando = false;
            }
        }

        [RelayCommand]
        private async Task VerDetalle(Lugar lugar)
        {
            if (lugar == null)
                return;

            // Se pasa el lugar seleccionado a la pantalla de detalle
            var parametros = new Dictionary<string, object>
            {
                { "Lugar", lugar }
            };
            await Shell.Current.GoToAsync(nameof(DetallePage), parametros);
        }
    }
}
