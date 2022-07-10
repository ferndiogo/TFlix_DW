﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscricoesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubscricoesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubscricoesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscricao>>> GetSubscricoes()
        {
            return await _context.Subscricoes
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Subscricao
                              {
                                  Id = a.Id,
                                  Duracao = a.Duracao,
                                  Preco = a.Preco,
                                  DataInicio = a.DataInicio,
                                  DataFim = a.DataFim,
                              })
                              .ToListAsync();
        }

        // GET: api/SubscricoesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscricao>> GetSubscricao(int id)
        {
            var subscricao = await _context.Subscricoes
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Subscricao
                              {
                                  Id = a.Id,
                                  Duracao = a.Duracao,
                                  Preco = a.Preco,
                                  DataInicio = a.DataInicio,
                                  DataFim = a.DataFim,
                              })
                              .Where(a => a.Id == id)
                              .FirstOrDefaultAsync(); ;

            if (subscricao == null)
            {
                return NotFound();
            }

            return subscricao;
        }

        // PUT: api/SubscricoesAPI/5
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

        // POST: api/SubscricoesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscricao>> PostSubscricao(Subscricao subscricao)
        {
            _context.Subscricoes.Add(subscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscricao", new { id = subscricao.Id }, subscricao);
        }

        // DELETE: api/SubscricoesAPI/5
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
