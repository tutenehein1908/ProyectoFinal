using System.ComponentModel.DataAnnotations;

namespace Licencias___PF.Model.DTO
{
    //Use DTO para que el usuario no vea toda la estructura de la clase
    public class LicenciaCreateDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 8)]
        public string Dni { get; set; }

        [Required]
        [RegularExpression("^(ordinaria|extraordinaria)$")]
        public string TipoDeLicencia { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Localidad { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string OD { get; set; }

        [Required]
        public DateOnly FechaInicio { get; set; }

        [Required]
        public DateOnly FechaFin { get; set; }
    }
}
