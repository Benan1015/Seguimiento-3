using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteItaliano.Models
{
    public class Platillo
    {
        [Key]
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre del platillo es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Range(1000, 1000000, ErrorMessage = "Ingresa un precio válido")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; } = true;

        // Clave foránea hacia Categoria
        [Required(ErrorMessage = "Debes seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        // Propiedad de navegación hacia la categoría padre
        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }
    }
}