using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeronyCruz_Ap1_P1.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el concepto")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el monto")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public double Monto { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el balance")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public double Balance { get; set;}

        [ForeignKey("Deudor")]
        public int DeudorId { get; set; }
        public Deudores? Deudor { get; set; }
    }
}
