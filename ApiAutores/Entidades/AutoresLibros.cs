namespace ApiAutores.Entidades
{
    public class AutoreLibro
    {
        public int libroId { get; set; }
        public int AutorId { get; set; }

        public int Orden { get; set; }

        public Autores Autor { get; set; }
        public Libro Libro { get; set; }
    }
}
