using ApiAutores.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiAutores.Entidades
{
    public class Autores 
    {
        public int Id { get; set; }


        public string Nombre { get; set; }


        public List<AutoreLibro> AutoresLibros { get; set; }

        
    }
}
