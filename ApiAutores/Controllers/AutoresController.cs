using ApiAutores.DTOs;
using ApiAutores.Entidades;
using ApiAutores.Filtros;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAutores.Controllers
{

    [ApiController]
    [Route("api/Autores")]
    public class AutoresController : ControllerBase
    {

        private readonly AplicationDBContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;



        public AutoresController(
            AplicationDBContext context, 
            ILogger<AutoresController> logger,
            IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }



        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<List<AutorDtos>>> Get()
        {

            var autores = await context.Autores.ToListAsync();

            return mapper.Map<List<AutorDtos>>(autores);
        }

        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<AutorDtos>> Get(int id)
        {

            var ConsultarAutor = await context.Autores.FirstOrDefaultAsync(item => item.Id == id);

            if(ConsultarAutor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDtos>(ConsultarAutor);
        
        }


        [HttpGet("{nombre}")]
        //[Authorize]
        public async Task<ActionResult<List<AutorDtos>>> Get([FromRoute] string nombre)
        {

            var autores = await context.Autores.Where(auotrDB => auotrDB.Nombre.Contains(nombre)).ToListAsync();

            if (autores == null)
            {
                return NotFound();
            }


            return mapper.Map<List<AutorDtos>>(autores);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDtos autorCreacionDtos)
        {
            var exiteAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autorCreacionDtos.Nombre);

            if (exiteAutorConElMismoNombre)
            {
                return BadRequest($@"Existe  un Autor Con Ese Nombre {autorCreacionDtos.Nombre}");
            }

            var autor = mapper.Map<Autores>(autorCreacionDtos);

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autores autor,int id)
        {

            if(autor.Id != id)
            {
                return BadRequest("No Existe Un Cliente Con Este Id");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            return Ok();
        }



        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}
