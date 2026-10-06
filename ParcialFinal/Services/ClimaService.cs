using System.Globalization;
using System.Text.Json;

namespace ParcialFinal.Services
{
    // Consume la API publica de Open-Meteo (no necesita clave)
    public class ClimaService
    {
        private readonly HttpClient http = new HttpClient();

        public async Task<double> ObtenerTemperatura(double latitud, double longitud)
        {
            string lat = latitud.ToString(CultureInfo.InvariantCulture);
            string lon = longitud.ToString(CultureInfo.InvariantCulture);
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

            string json = await http.GetStringAsync(url);

            using JsonDocument documento = JsonDocument.Parse(json);
            return documento.RootElement
                .GetProperty("current_weather")
                .GetProperty("temperature")
                .GetDouble();
        }
    }
}
