using ApiAutores.Validaciones;

namespace ApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }


        public List<AutoreLibro> AutoresLibros { get; set; }

        //public List<Comentario> Comentarios { get; set; }
   
    }
}
