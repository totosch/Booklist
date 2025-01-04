using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiNET.Models;
using ApiNET.Data;

namespace ApiNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLibros()
        {
            var libros = await _context.Libros.ToListAsync();
            return Ok(libros);
        }

        [HttpPost]
        public async Task<IActionResult> AddLibro([FromBody] Libro nuevoLibro)
        {
            _context.Libros.Add(nuevoLibro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLibros), new { id = nuevoLibro.Id }, nuevoLibro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibro(int id, [FromBody] Libro libroActualizado)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Titulo = libroActualizado.Titulo;
            libro.Autor = libroActualizado.Autor;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("buscarPorAutor")]
        public async Task<IActionResult> GetLibrosPorAutor(string autor)
        {
            var libros = await _context.Libros
                .Where(l => l.Autor.Contains(autor))
                .ToListAsync();

            return Ok(libros);
        }

        [HttpGet("buscarPorTitulo")]
        public async Task<IActionResult> GetLibrosPorTitulo(string titulo)
        {
            var libros = await _context.Libros
                .Where(l => l.Titulo.Contains(titulo))
                .ToListAsync();

            return Ok(libros);
        }
    }
}
