using ParcialFinal.Models;
using SQLite;

namespace ParcialFinal.Services
{
    // Servicio para guardar y leer los lugares en SQLite
    public class BaseDatosService
    {
        private SQLiteAsyncConnection? conexion;

        private async Task Iniciar()
        {
            if (conexion != null)
                return;

            string ruta = Path.Combine(FileSystem.AppDataDirectory, "lugares.db3");
            conexion = new SQLiteAsyncConnection(ruta);
            await conexion.CreateTableAsync<Lugar>();

            // Si la tabla esta vacia se llenan los 7 lugares
            int cantidad = await conexion.Table<Lugar>().CountAsync();
            if (cantidad == 0)
            {
                await conexion.InsertAllAsync(new List<Lugar>
                {
                    new Lugar { Nombre = "Monserrate", Ciudad = "Bogota", Descripcion = "Cerro con un santuario y vista de toda la ciudad.", Latitud = 4.6058, Longitud = -74.0556 },
                    new Lugar { Nombre = "Ciudad Amurallada", Ciudad = "Cartagena", Descripcion = "Centro historico rodeado de murallas coloniales.", Latitud = 10.4236, Longitud = -75.5498 },
                    new Lugar { Nombre = "Comuna 13", Ciudad = "Medellin", Descripcion = "Barrio famoso por sus grafitis y escaleras electricas.", Latitud = 6.2556, Longitud = -75.6178 },
                    new Lugar { Nombre = "Valle de Cocora", Ciudad = "Salento", Descripcion = "Valle con las palmas de cera mas altas del mundo.", Latitud = 4.6383, Longitud = -75.4875 },
                    new Lugar { Nombre = "Parque Tayrona", Ciudad = "Santa Marta", Descripcion = "Parque natural con playas y selva.", Latitud = 11.3083, Longitud = -74.0306 },
                    new Lugar { Nombre = "Cristo Rey", Ciudad = "Cali", Descripcion = "Estatua en lo alto de un cerro sobre la ciudad.", Latitud = 3.4361, Longitud = -76.5642 },
                    new Lugar { Nombre = "Piedra del Penol", Ciudad = "Guatape", Descripcion = "Roca gigante con mas de 700 escalones.", Latitud = 6.2208, Longitud = -75.1789 }
                });
            }
        }

        public async Task<List<Lugar>> ObtenerLugares()
        {
            await Iniciar();
            return await conexion!.Table<Lugar>().ToListAsync();
        }

        public async Task Actualizar(Lugar lugar)
        {
            await Iniciar();
            await conexion!.UpdateAsync(lugar);
        }
    }
}
