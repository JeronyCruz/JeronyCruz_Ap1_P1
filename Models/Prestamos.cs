using System.ComponentModel.DataAnnotations;

namespace JeronyCruz_Ap1_P1.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage ="Por favor ingresar el nombre del deudor")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
        public string Deudor { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el concepto")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el monto")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public double Monto { get; set; }
    }
}
