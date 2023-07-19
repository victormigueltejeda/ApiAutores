using ApiAutores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Titulo { get; set; }


        public List<AutoreLibro> AutoresLibros { get; set; }

        public List<Comentario> Comentarios { get; set; }
   
    }
}
