using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeronyCruz_Ap1_P1.Models;

    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }

        [Required(ErrorMessage = "Favor ingresar la fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [ForeignKey("Deudores")]
        public int DeudorId { get; set; }
        public Deudores? Deudores { get; set; }

        [Required(ErrorMessage = "Debe de ingresar el valor cobrado")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public Double Monto { get; set; }

        [ForeignKey("CobroId")]
        public ICollection<CobroDetalle> CobroDetalles { get; set; } = new List<CobroDetalle>();
    }
