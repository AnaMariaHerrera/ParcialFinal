using SQLite;

namespace ParcialFinal.Models
{
    // Tabla de la base de datos SQLite
    public class Lugar
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public string Ciudad { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Visitado { get; set; }
    }
}
