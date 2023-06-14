using ApiAutores.DTOs;
using ApiAutores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAutores.Controllers
{

    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentarioController : ControllerBase
    {
        private readonly AplicationDBContext context;
        private readonly IMapper mapper;


        public ComentarioController(AplicationDBContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        [HttpGet]

        public async Task<ActionResult<List<ComentarioDtos>>> Get(int libroId)
        {
            var comentarios = await context.Comentario.Where(comentarioDb => comentarioDb.LibroId == libroId).ToListAsync();

            return mapper.Map<List<ComentarioDtos>>(comentarios);
        }




        [HttpPost]
        public async Task<ActionResult> Post(int libroId, CreacionComentarioDtos creacionComentarioDtos)
        {

            var ExisteLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!ExisteLibro)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(creacionComentarioDtos);
            comentario.LibroId = libroId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
