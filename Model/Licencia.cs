using System;
using System.ComponentModel.DataAnnotations;

namespace Licencia___PF.Model
{
    public class Licencia
    {

        //required son practicamente todos los campos menos Id que por convencion tengo entendido que es identity
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "El DNI debe tener entre 8 y 9 caracteres.")] //use la anotacion [StringLength(valorMáximo, MinimumLength = valorMínimo)] que nos deja poner valor maximo permitido y valor minimo
        public string Dni { get; set; }

        [Required(ErrorMessage = "El tipo de licencia es obligatorio.")]
        [RegularExpression("^(ordinaria|extraordinaria)$", ErrorMessage = "El tipo de licencia debe ser 'ordinaria' o 'extraordinaria'.")] //esto se vaida mediante una expresion regular que solo admite los valores exactos ordinaria o extraordinaria
        public string TipoDeLicencia { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "La localidad es obligatoria.")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La orden del día es obligatoria.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "La OD debe tener entre 6 y 10 caracteres.")] //use la anotacion [StringLength(valorMáximo, MinimumLength = valorMínimo)] que nos deja poner valor maximo permitido y valor minimo
        public string OD { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        public DateOnly FechaFin { get; set; }
    }
}
