using ApplicationLayer.DTOs;
using Challenge.Data;
using DataEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly ChallengeContext _context;

        public PermisosController(ChallengeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permisos>>> GetPermisos()
        {
          if (_context.Permisos == null)
          {
              return NotFound();
          }
            return await _context.Permisos.Include(m => m.TipoPermisos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Permisos>> GetPermisos(int id)
        {
            if (_context.Permisos == null)
            {
                return NotFound();
            }

            var permisos = await _context.Permisos.Include(m => m.TipoPermisos).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (permisos == null)
            {
                return NotFound();
            }

            return permisos;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermisos(int id, [FromBody] PermisosDTO permisos)
        {
            if (id != permisos.Id)
            {
                return BadRequest();
            }

            var infoPermisos = new Permisos
            {
                Id = permisos.Id,
                NombreEmpleado = permisos.NombreEmpleado,
                ApellidoEmpleado = permisos.ApellidoEmpleado,
                TipoPermiso = permisos.TipoPermiso
            };

            _context.Permisos.Update(infoPermisos);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Permisos>> PostPermisos([FromBody] PermisosDTO permisos)
        {
          
            var infoPermisos = new Permisos
            {
                NombreEmpleado = permisos.NombreEmpleado,
                ApellidoEmpleado = permisos.ApellidoEmpleado,
                FechaPermiso = DateTime.Now,
                TipoPermiso = permisos.TipoPermiso
            };

            _context.Permisos.Add(infoPermisos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermisosExists(int id)
        {
            return (_context.Permisos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Route("tipoPermisos")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPermisos>>> GetTipoPermisos()
        {
            if (_context.TipoPermisos == null)
            {
                return NotFound();
            }
            return await _context.TipoPermisos.ToListAsync();
        }
    }
}
