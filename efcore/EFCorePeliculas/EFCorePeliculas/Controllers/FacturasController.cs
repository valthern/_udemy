using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.Funciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/facturas")]
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public FacturasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}/detalle")]
        public async Task<ActionResult<IEnumerable<FacturaDetalle>>> GetDetalle(int id)
        {
            return await context.FacturaDetalles
                .Where(f=>f.FacturaId == id)
                .OrderByDescending(f=>f.Total)
                .ToListAsync();
        }

        [HttpGet("Funciones_escalares")]
        public async Task<ActionResult> GetFuncionesEscalares()
        {
            var facturas=await context.Facturas
                .Select(f => new
                {
                    f.Id,
                    Total=context.FacturaDetalleSuma(f.Id),
                    Promedio=Escalares.FacturaDetallePromedio(f.Id)
                })
                .OrderByDescending(f=>context.FacturaDetalleSuma(f.Id))
                .ToListAsync();

            return Ok(facturas);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                var factura = new Factura
                {
                    FechaCreacion = DateTime.Now
                };

                context.Add(factura);
                await context.SaveChangesAsync();

                throw new ApplicationException("Esto es una prueba");

                var facturaDetalle = new List<FacturaDetalle>()
                {
                    new FacturaDetalle()
                    {
                        Producto="Producto A",
                        Precio=123,
                        FacturaId=factura.Id
                    },
                    new FacturaDetalle
                    {
                        Producto="Producto B",
                        Precio=456,
                        FacturaId=factura.Id
                    }
                };

                context.AddRange(facturaDetalle);
                await context.SaveChangesAsync();
                await transaccion.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Hubo un error");
            }
        }
    }
}
 