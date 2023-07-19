using ApiAutores.DTOs;
using ApiAutores.Entidades;
using AutoMapper;

namespace ApiAutores.Utilidades
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {


            CreateMap<AutorCreacionDtos, Autores>();

            CreateMap<Autores, AutorDtos>();

            CreateMap<Autores, AutorDtosConLibros>()
                .ForMember(autorDTO => autorDTO.Libro, opcines => opcines.MapFrom(MapAutorDTOSLibros));

            CreateMap<LibroCreacionDTos, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));


            CreateMap<Libro, LibroDtos>();
            CreateMap<Libro, LibroDtosConAutores>()
                .ForMember(libroDTO => libroDTO.Autores, opcines => opcines.MapFrom(MapLibroDTOAutores));

            CreateMap<CreacionComentarioDtos, Comentario>();

            CreateMap<Comentario, ComentarioDtos>();



        }

        private List<LibroDtos> MapAutorDTOSLibros(Autores autor,AutorDtos autorDtos)
        {
            var resultado = new List<LibroDtos>();


            if (autor.AutoresLibros == null)
            {
                return resultado;
            }

            foreach (var autoreLibro in autor.AutoresLibros)
            {
                resultado.Add(new LibroDtos()
                {
                    Id = autoreLibro.libroId,
                    Titulo = autoreLibro.Libro.Titulo
                });
            }


            return resultado;

        }

        private List<AutorDtos> MapLibroDTOAutores(Libro libro,LibroDtos libroDtos)
        {
            var resultado = new List<AutorDtos>();

            if(libro.AutoresLibros == null)
            {
                return resultado;
            }

            foreach(var autoresLibro in libro.AutoresLibros)
            {
                resultado.Add(new AutorDtos()
                {
                    Id = autoresLibro.AutorId,
                    Nombre = autoresLibro.Autor.Nombre
                });
            }

            return resultado;
        }


         private List<AutoreLibro> MapAutoresLibros(LibroCreacionDTos libroCreacionDTos, Libro libro)
        {
            var resultado = new List<AutoreLibro>();

            if(libroCreacionDTos.AutoresIds == null) { return resultado; }


            foreach( var autorId in libroCreacionDTos.AutoresIds)
            {
                resultado.Add(new AutoreLibro() { AutorId = autorId });
            }

            return resultado;
        }
    }
}
