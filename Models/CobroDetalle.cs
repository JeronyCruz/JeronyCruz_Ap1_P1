using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeronyCruz_Ap1_P1.Models;

    public class CobroDetalle
    {
        [Key]
        public int DetalleId { get; set; }

        public int CobroId  { get; set; }

        [ForeignKey("Prestamos")]
        public int PrestamoId { get; set; }
        public Prestamos? Prestamos { get; set; }
        

        [Required(ErrorMessage ="Debe de ingresar el valor cobrado")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public Double ValorCobrado { get; set; }
    }