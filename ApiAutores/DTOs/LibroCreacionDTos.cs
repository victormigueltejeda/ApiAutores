using ApiAutores.Validaciones;

namespace ApiAutores.DTOs
{
    public class LibroCreacionDTos
    {
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }

        public List<int> AutoresIds { get; set; }
    }
}
