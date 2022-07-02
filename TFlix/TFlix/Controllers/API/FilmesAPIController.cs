using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FilmesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
            return await _context.Filmes
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Filme
                              {
                                  Id = a.Id,
                                  Titulo = a.Titulo,
                                  Imagem = a.Imagem,
                                  Sinopse = a.Sinopse,
                                  DataCriacao = a.DataCriacao,
                                  Classificacao = a.Classificacao,
                                  Elenco = a.Elenco,
                                  Genero = a.Genero,
                              })
                              .ToListAsync();
        }

        // GET: api/FilmesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _context.Filmes
                                    .Select(a => new Filme
                                    {
                                        Id = a.Id,
                                        Titulo = a.Titulo,
                                        Imagem = a.Imagem,
                                        Sinopse = a.Sinopse,
                                        DataCriacao = a.DataCriacao,
                                        Classificacao = a.Classificacao,
                                        Elenco = a.Elenco,
                                        Genero = a.Genero,
                                    })
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync(); ;

            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        // PUT: api/FilmesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, Filme filme)
        {
            if (id != filme.Id)
            {
                return BadRequest();
            }

            _context.Entry(filme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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

        // POST: api/FilmesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilme", new { id = filme.Id }, filme);
        }

        // DELETE: api/FilmesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmeExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
