using System.ComponentModel.DataAnnotations;

namespace RestauranteItaliano.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(60)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Descripcion { get; set; }

        // Propiedad de navegación: una categoría tiene muchos platillos (1:N)
        public ICollection<Platillo> Platillos { get; set; } = new List<Platillo>();
    }
}