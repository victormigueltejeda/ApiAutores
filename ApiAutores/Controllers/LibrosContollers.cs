using ApiAutores.DTOs;
using ApiAutores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAutores.Controllers
{
   
        [ApiController]
        [Route("api/libros")]
        public class LibrosController : ControllerBase
        {
            private readonly AplicationDBContext context;
            private readonly IMapper mapper;
 
            public LibrosController(AplicationDBContext context,IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;   
            }

            [HttpGet("{id:int}",Name ="obtenerLibro")]
            public async Task<ActionResult<LibroDtosConAutores>> Get(int id)
            {
                var libros = await context.Libros
                .Include(libroDB => libroDB.AutoresLibros)
                .ThenInclude(AutoreLibroDB => AutoreLibroDB.Autor)
                .FirstOrDefaultAsync(libroDB => libroDB.Id == id);

                if(libros == null)
                {
                return BadRequest("Esta llegando null la busqueda");
                
                }


               libros.AutoresLibros = libros.AutoresLibros.OrderBy(x => x.Orden).ToList();

                return mapper.Map<LibroDtosConAutores>(libros);
            }

            [HttpPost]
            public async Task<ActionResult> Post(LibroCreacionDTos libroCreacionDtos)
            {

                if (libroCreacionDtos.AutoresIds == null)
                {
                    return BadRequest("No Se puede crear un libro sin autores");

                }

               var autoresIds = await context.Autores
                .Where(autorDB => libroCreacionDtos.AutoresIds.Contains(autorDB.Id)).Select(x => x.Id).ToListAsync();

                if(libroCreacionDtos.AutoresIds.Count != autoresIds.Count)
                {
                 return BadRequest("No Exisite uno de los autores");

                }

                var libro = mapper.Map<Libro>(libroCreacionDtos);

                if(libro.AutoresLibros != null)
                {
                    for(int i = 0; i< libro.AutoresLibros.Count; i++)
                    {

                        libro.AutoresLibros[i].Orden = i;
                    }

                }

                context.Add(libro);
                await context.SaveChangesAsync();

                 LibroDtos libroDtos = mapper.Map<LibroDtos>(libro);
                return CreatedAtRoute("obtenerLibro",new {id = libro.Id}, libroDtos);
            }
        }
}
