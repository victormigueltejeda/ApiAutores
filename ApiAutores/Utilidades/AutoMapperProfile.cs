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

            CreateMap<LibroCreacionDTos, Libro>();

            CreateMap<Libro, LibroDtos>();

            CreateMap<CreacionComentarioDtos, Comentario>();

            CreateMap<Comentario, ComentarioDtos>();

        }
    }
}
