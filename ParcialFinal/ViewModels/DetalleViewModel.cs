using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ParcialFinal.Models;
using ParcialFinal.Services;

namespace ParcialFinal.ViewModels
{
    // Recibe el lugar que se envio desde la lista
    [QueryProperty(nameof(Lugar), "Lugar")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly BaseDatosService baseDatos;
        private readonly ClimaService clima;

        [ObservableProperty]
        private Lugar? lugar;

        [ObservableProperty]
        private string distancia = "Sin calcular";

        [ObservableProperty]
        private string temperatura = "Sin consultar";

        [ObservableProperty]
        private bool cargando;

        public DetalleViewModel(BaseDatosService baseDatos, ClimaService clima)
        {
            this.baseDatos = baseDatos;
            this.clima = clima;
        }

        // Funcion nativa: geolocalizacion del dispositivo
        [RelayCommand]
        private async Task CalcularDistancia()
        {
            if (Lugar == null)
                return;

            try
            {
                Cargando = true;

                var solicitud = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                Location? miUbicacion = await Geolocation.Default.GetLocationAsync(solicitud);

                if (miUbicacion == null)
                {
                    await Shell.Current.DisplayAlert("Aviso", "No se pudo obtener tu ubicacion", "OK");
                    return;
                }

                var destino = new Location(Lugar.Latitud, Lugar.Longitud);
                double km = Location.CalculateDistance(miUbicacion, destino, DistanceUnits.Kilometers);
                Distancia = $"{km:F1} km desde tu ubicacion";
            }
            catch (PermissionException)
            {
                await Shell.Current.DisplayAlert("Error", "Debes dar permiso de ubicacion a la app", "OK");
            }
            catch (FeatureNotEnabledException)
            {
                await Shell.Current.DisplayAlert("Error", "El GPS esta apagado", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Ocurrio un error: " + ex.Message, "OK");
            }
            finally
            {
                Cargando = false;
            }
        }

        // Consumo de API REST
        [RelayCommand]
        private async Task ConsultarClima()
        {
            if (Lugar == null)
                return;

            try
            {
                Cargando = true;
                double grados = await clima.ObtenerTemperatura(Lugar.Latitud, Lugar.Longitud);
                Temperatura = $"{grados} °C";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo consultar el clima: " + ex.Message, "OK");
            }
            finally
            {
                Cargando = false;
            }
        }

        // Guarda en SQLite si el lugar fue visitado
        [RelayCommand]
        private async Task MarcarVisitado()
        {
            if (Lugar == null)
                return;

            try
            {
                Lugar.Visitado = !Lugar.Visitado;
                await baseDatos.Actualizar(Lugar);
                OnPropertyChanged(nameof(Lugar));

                string mensaje = Lugar.Visitado ? "Lugar marcado como visitado" : "Lugar marcado como no visitado";
                await Shell.Current.DisplayAlert("Listo", mensaje, "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo guardar: " + ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
