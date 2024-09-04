using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemasIII.BaseDatos;
using ProyectoSistemasIII.Models;
using ProyectoSistemasIII.DTOs;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoSistemasIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Cors")]
    public class PrestamoController : ControllerBase
    {
        private readonly BibliotecaDbContest _context;

        public PrestamoController(BibliotecaDbContest context)
        {
            _context = context;
        }
        [HttpPost("ReservarLibro")]
        public async Task<ActionResult<Prestamo>> ReservarLibro([FromBody] PrestamoDto prestamoDto)
        {
            if (prestamoDto == null)
            {
                return BadRequest("El cuerpo de la solicitud está vacío.");
            }

            var libro = await _context.Libro.FindAsync(prestamoDto.LibroId);
            if (libro == null || !libro.Disponible)
            {
                return BadRequest("El libro no está disponible para préstamo.");
            }

            var usuario = await _context.Usuario.FindAsync(prestamoDto.UsuarioId);
            if (usuario == null)
            {
                return BadRequest("El usuario no existe.");
            }

            var prestamo = new Prestamo
            {
                LibroId = prestamoDto.LibroId,
                UsuarioId = prestamoDto.UsuarioId,
                FechaInicio = prestamoDto.FechaInicio,
                FechaFin = prestamoDto.FechaFin
            };

            libro.Disponible = false;
            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                _context.Prestamo.Add(prestamo);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.Id }, prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }



        // GET: api/Prestamo
        [HttpGet("ListarPrestamos")]
        public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
        {
            return await _context.Prestamo.Include(p => p.Libro).Include(p => p.Usuario).ToListAsync();
        }

        // GET: api/Prestamo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
        {
            var prestamo = await _context.Prestamo
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return prestamo;
        }

        [HttpPost("RegistrarPrestamo")]
        public async Task<ActionResult<Prestamo>> RegistrarPrestamo(Prestamo prestamo)
        {
            var libro = await _context.Libro.FindAsync(prestamo.LibroId);
            if (libro == null || !libro.Disponible)
            {
                return BadRequest("El libro no está disponible para préstamo.");
            }

            var usuario = await _context.Usuario.FindAsync(prestamo.UsuarioId);
            if (usuario == null)
            {
                return BadRequest("El usuario no existe.");
            }

            // Se crean las fechas de inicio y fin del préstamo
            var nuevoPrestamo = new Prestamo
            {
                LibroId = prestamo.LibroId,
                UsuarioId = prestamo.UsuarioId,
                FechaInicio = prestamo.FechaInicio,
                FechaFin = prestamo.FechaFin
            };

            // Se marca el libro como no disponible
            libro.Disponible = false;
            _context.Entry(libro).State = EntityState.Modified;

            // Se añade el préstamo a la base de datos
            _context.Prestamo.Add(nuevoPrestamo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrestamo), new { id = nuevoPrestamo.Id }, nuevoPrestamo);
        }

        // PUT: api/Prestamo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestamoExists(id))
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

        // DELETE: api/Prestamo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            var prestamo = await _context.Prestamo.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            var libro = await _context.Libro.FindAsync(prestamo.LibroId);
            if (libro != null)
            {
                libro.Disponible = true;
                _context.Entry(libro).State = EntityState.Modified;
            }

            _context.Prestamo.Remove(prestamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamo.Any(e => e.Id == id);
        }
    }
}