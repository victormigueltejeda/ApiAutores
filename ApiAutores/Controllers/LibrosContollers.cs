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

            [HttpGet("{id:int}")]
            public async Task<ActionResult<LibroDtos>> Get(int id)
            {
                var libros = await context.Libros.FirstOrDefaultAsync(libroDB => libroDB.Id == id);

                return mapper.Map<LibroDtos>(libros);
            }

            [HttpPost]
            public async Task<ActionResult> Post(LibroCreacionDTos libroCreacionDtos)
            {

                var libro = mapper.Map<Libro>(libroCreacionDtos);

                context.Add(libro);
                await context.SaveChangesAsync();
                return Ok();
            }
        }
}
