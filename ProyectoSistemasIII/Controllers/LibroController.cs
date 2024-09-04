using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemasIII.BaseDatos;
using ProyectoSistemasIII.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoSistemasIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Cors")]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaDbContest _db;

        public LibroController(BibliotecaDbContest context)
        {
            _db = context;
        }
        // GET: api/Libro/Disponibles
        [HttpGet("Disponibles")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosDisponibles()
        {
            var librosDisponibles = await _db.Libro
                .Where(libro => libro.Disponible)
                .ToListAsync();

            return librosDisponibles;
        }

        // GET: api/Libro
        [HttpGet ("Listar")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _db.Libro.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _db.Libro.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // GET: api/Libro/BuscarDisponible/{id}
        [HttpGet("BuscarDisponible/{id}")]
        public async Task<ActionResult<Libro>> GetLibroDisponible(int id)
        {
            var libro = await _db.Libro
                .Where(l => l.Id == id && l.Disponible)
                .FirstOrDefaultAsync();

            if (libro == null)
            {
                return NotFound("El libro no está disponible o no existe.");
            }

            return libro;
        }
        // POST: api/Libro
        [HttpPost ("Agregar un nuevo libro")]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _db.Libro.Add(libro);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        // PUT: api/Libro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _db.Entry(libro).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
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

        // DELETE: api/Libro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _db.Libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _db.Libro.Remove(libro);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _db.Libro.Any(e => e.Id == id);
        }
    }
}
