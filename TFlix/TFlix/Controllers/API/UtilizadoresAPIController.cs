using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadoresAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilizadoresAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UtilizadoresAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizador>>> GetUtilizadores()
        {
            return await _context.Utilizadores
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Utilizador
                              {
                                  Id = a.Id,
                                  Nome = a.Nome,
                                  Email = a.Email,
                                  Sexo = a.Sexo,
                                  DataNasc = a.DataNasc,
                                  NIF = a.NIF,
                                  Morada = a.Morada,
                                  CodPostal = a.CodPostal,
                                  Pais = a.Pais,
                                  UserF = a.UserF,
                              })
                              .ToListAsync();
        }

        // GET: api/UtilizadoresAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizador>> GetUtilizador(int id)
        {
            var utilizador = await _context.Utilizadores
                                    .Select(a => new Utilizador
                                    {
                                        Id = a.Id,
                                        Nome = a.Nome,
                                        Email = a.Email,
                                        Sexo = a.Sexo,
                                        DataNasc = a.DataNasc,
                                        NIF = a.NIF,
                                        Morada = a.Morada,
                                        CodPostal = a.CodPostal,
                                        Pais = a.Pais,
                                        UserF = a.UserF,
                                    })
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

            if (utilizador == null)
            {
                return NotFound();
            }

            return utilizador;
        }

        // PUT: api/UtilizadoresAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizador(int id, Utilizador utilizador)
        {
            if (id != utilizador.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilizador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizadorExists(id))
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

        // POST: api/UtilizadoresAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilizador>> PostUtilizador(Utilizador utilizador)
        {
            _context.Utilizadores.Add(utilizador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilizador", new { id = utilizador.Id }, utilizador);
        }

        // DELETE: api/UtilizadoresAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizador(int id)
        {
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            _context.Utilizadores.Remove(utilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }
    }
}
