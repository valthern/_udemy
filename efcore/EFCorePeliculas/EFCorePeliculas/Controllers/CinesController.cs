using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.DTOs;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.SinLlaves;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/cines")]
    public class CinesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("SinUbicacion")]
        public async Task<IEnumerable<CineSinUbicacion>> GetCinesSinUbicacion()
        {
            //return await context.Set<CineSinUbicacion>().ToListAsync();
            return await context.CinesSinUbicacion.ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<CineDTO>> Get()
        {
            return await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("cercanos")]
        public async Task<ActionResult> Get(double latitud, double longitud)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var miUbicacion = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));
            var distanciaMaximaEnMetros = 2000;

            var cines = await context.Cines
                .OrderBy(c => c.Ubicacion.Distance(miUbicacion))
                .Where(c => c.Ubicacion.IsWithinDistance(miUbicacion, distanciaMaximaEnMetros))
                //.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
                .Select(c => new
                {
                    c.Nombre,
                    Distancia = Math.Round(c.Ubicacion.Distance(miUbicacion))
                }).ToListAsync();

            return Ok(cines);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var ubicacionCine = geometryFactory.CreatePoint(new Coordinate(-69.896979, 18.476276));

            var Cine = new Cine
            {
                Nombre = "Mi Cine sin detalle",
                Ubicacion = ubicacionCine,
                CineDetalle = new()
                {
                    Historia = "Historia...",
                    CodigoDeEtica = "Código...",
                    Misiones = "Misiones..."
                },
                CineOferta = new CineOferta
                {
                    PorcentajeDescuento = 5,
                    FechaInicio = DateTime.Today,
                    FechaFin = DateTime.Today.AddDays(7)
                },
                SalasDeCine = new HashSet<SalaDeCine>
                {
                    new SalaDeCine {
                        Precio=200,
                        Moneda=Moneda.PesoDominicano,
                        TipoSalaDeCine=TipoSalaDeCine.DosDimensiones
                    },
                    new SalaDeCine {
                        Precio=350,
                        Moneda=Moneda.DolarEstadounidence,
                        TipoSalaDeCine=TipoSalaDeCine.TresDimensiones
                    }
                }
            };

            context.Add(Cine);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("conDTO")]
        public async Task<ActionResult> Post(CineCreacionDTO cineCreacion)
        {
            var cine = mapper.Map<Cine>(cineCreacion);
            context.Add(cine);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            //var cineDB = await context.Cines.AsTracking()
            //    .Include(c => c.SalasDeCine)
            //    .Include(c => c.CineOferta)
            //    .Include(c => c.CineDetalle)
            //    .FirstOrDefaultAsync(c => c.Id == id);

            var cineDB = await context.Cines
                .FromSqlInterpolated($"select * from Cines where id = {id}")
                .Include(c => c.SalasDeCine)
                .Include(c => c.CineOferta)
                .Include(c => c.CineDetalle)
                .FirstOrDefaultAsync();

            if (cineDB is null) return NotFound();

            cineDB.Ubicacion = null;
            return Ok(cineDB);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CineCreacionDTO cineCreacionDTO, int id)
        {
            var cineDB = await context.Cines.AsTracking()
                .Include(c => c.SalasDeCine)
                .Include(c => c.CineOferta)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cineDB is null) return NotFound();

            cineDB = mapper.Map(cineCreacionDTO, cineDB);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("cineOferta")]
        public async Task<ActionResult> PutCineOferta(CineOferta cineOferta)
        {
            context.Update(cineOferta);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cine = await context.Cines
                .Include(c => c.SalasDeCine)
                .Include(c => c.CineOferta)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cine is null) return NotFound();

            context.RemoveRange(cine.SalasDeCine);
            await context.SaveChangesAsync();

            context.Remove(cine);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
