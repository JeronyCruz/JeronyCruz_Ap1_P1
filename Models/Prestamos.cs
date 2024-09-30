using System.ComponentModel.DataAnnotations;

namespace JeronyCruz_Ap1_P1.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage ="Por favor ingresar el nombre del deudor")]
        public string Dedudor { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el concepto")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el monto")]
        public double Monto { get; set; }
    }
}
