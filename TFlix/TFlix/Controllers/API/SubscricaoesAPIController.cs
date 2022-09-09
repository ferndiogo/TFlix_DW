using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscricaoesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubscricaoesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubscricaoesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscricao>>> GetSubscricoes()
        {
            return await _context.Subscricoes
                              .Include(a => a.Utilizador)
                              .Include(a => a.Filmes)
                              .Include(a => a.Series)
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Subscricao
                              {
                                  Id = a.Id,
                                  Utilizador = a.Utilizador,
                                  Duracao = a.Duracao,
                                  Preco = a.Preco,
                                  DataInicio = a.DataInicio,
                                  DataFim = a.DataFim,
                                  Filmes = a.Filmes,
                                  Series = a.Series,
                              })
                              .ToListAsync();
        }

        // GET: api/SubscricaoesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscricao>> GetSubscricao(int id)
        {
            var subscricao = await _context.Subscricoes
                              .Include(a => a.Utilizador)
                              .Include(a => a.Filmes)
                              .Include(a => a.Series)
                                    .Select(a => new Subscricao
                                    {
                                        Id = a.Id,
                                        Utilizador = a.Utilizador,
                                        Preco = a.Preco,
                                        Duracao = a.Duracao,
                                        DataInicio = a.DataInicio,
                                        DataFim = a.DataFim,
                                        Filmes = a.Filmes,
                                        Series = a.Series,
                                    })
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync();

            if (subscricao == null)
            {
                return NotFound();
            }

            return subscricao;
        }

        // PUT: api/SubscricaoesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscricao(int id, Subscricao subscricao)
        {
            if (id != subscricao.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscricaoExists(id))
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

        // POST: api/SubscricaoesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscricao>> PostSubscricao([FromForm] Subscricao subscricao)
        {
            //subscricao.AuxPreco = "5,99";
            // transfere o valor do AuxPreco para Preco
            //subscricao.Preco = Convert.ToDecimal(subscricao.AuxPreco.Replace('.', ','));

           

            subscricao.DataInicio = DateTime.Now;
            subscricao.DataFim = DateTime.Now;
            subscricao.Duracao = 12;

            _context.Subscricoes.Add(subscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscricao", new { id = subscricao.Id }, subscricao);
        }

        // DELETE: api/SubscricaoesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscricao(int id)
        {
            var subscricao = await _context.Subscricoes.FindAsync(id);
            if (subscricao == null)
            {
                return NotFound();
            }

            _context.Subscricoes.Remove(subscricao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscricaoExists(int id)
        {
            return _context.Subscricoes.Any(e => e.Id == id);
        }
    }
}
