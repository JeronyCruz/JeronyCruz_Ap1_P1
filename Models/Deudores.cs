using System.ComponentModel.DataAnnotations;

namespace JeronyCruz_Ap1_P1.Models;

public class Deudores
{
    [Key]
    public int DeudorId { get; set; }

    [Required(ErrorMessage = "Por favor ingresar el nombre del deudor")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Nombres { get; set;}
}
