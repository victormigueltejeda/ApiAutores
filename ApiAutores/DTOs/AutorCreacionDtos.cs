using ApiAutores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiAutores.DTOs
{
    public class AutorCreacionDtos
    {


        [PrimeraLetraMayuscula]
        [Required(ErrorMessage ="El Campo {0} es requerido")]
        public string Nombre { get; set; }
    }
}
