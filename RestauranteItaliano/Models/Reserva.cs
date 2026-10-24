namespace RestauranteItaliano.Models
{
    public class Reserva
    {
        public string NombreCliente { get; set; }
        public string Correo { get; set; }
        public DateTime FechaReserva { get; set; }
        public int NumPersonas { get; set; }
        public string Zona { get; set; }
    }
}