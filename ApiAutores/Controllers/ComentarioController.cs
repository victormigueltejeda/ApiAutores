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


        [HttpGet("{id:int}",Name = "obtenerComentario")]
        public async Task<ActionResult<ComentarioDtos>> GetPorId(int id)
        {
            var comentario = await context.Comentario.FirstOrDefaultAsync(comentarioDB => comentarioDB.Id == id);

            if (comentario == null)
            {
                return NotFound();

            }


            return mapper.Map<ComentarioDtos>(comentario);

        }




        [HttpPost]
        public async Task<ActionResult> Post(int libroId, CreacionComentarioDtos creacionComentarioDtos)
        {

            var ExisteLibro = await context.Autores.AnyAsync(libroDB => libroDB.Id == libroId);

            if (!ExisteLibro)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(creacionComentarioDtos);
            comentario.LibroId = libroId;
            context.Add(comentario);
            await context.SaveChangesAsync();


            ComentarioDtos comentarioDtos = mapper.Map<ComentarioDtos>(comentario);

            return CreatedAtRoute("obtenerComentario", new { id = comentario.Id, libroId =libroId}, comentarioDtos);

        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(int libroId, int id,CreacionComentarioDtos creacionComentarioDtos)
        {

            var existeLibro = await context.Libros.AnyAsync(libroDB => libroDB.Id == id);

            if (!existeLibro)
            {
                return NotFound();
            }

            var existeCometario = await context.Comentario.AnyAsync(cometarioDB => cometarioDB.Id == id);

            if (!existeCometario)
            {
                return NotFound();
            }


            var cometario = mapper.Map<Comentario>(creacionComentarioDtos);
            cometario.Id = id;
            cometario.LibroId = libroId;

            context.Update(cometario);
            context.SaveChangesAsync();
            return NoContent();
        }
    }
}
