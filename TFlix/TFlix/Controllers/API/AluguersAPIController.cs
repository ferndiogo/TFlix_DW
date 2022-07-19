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
    public class AluguersAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AluguersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AluguersAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlugaViewModel>>> GetAluguers()
        {
            return await _context.Aluguers
                              .Include(a => a.Utilizador)
                              .Include(a => a.Filme)
                              .OrderByDescending(a => a.Id)
                              .Select(a => new AlugaViewModel
                              {
                                  Id = a.Id,
                                  NomeUtilizador = a.Utilizador.Nome,
                                  NomeFilme = a.Filme.Titulo,
                                  UtilizadorFK = a.UtilizadorFK,
                                  FilmeFK = a.FilmeFK,
                                  Preco = a.Preco,
                                  DataInicio = a.DataInicio,
                                  DataFim = a.DataFim,

                              })
                              .ToListAsync();
        }

        // GET: api/AluguersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlugaViewModel>> GetAluga(int id)
        {
            var aluga = await _context.Aluguers
                              .Include(a => a.Utilizador)
                              .Include(a => a.Filme)
                              .Select(a => new AlugaViewModel
                              {
                                  Id = a.Id,
                                  NomeUtilizador = a.Utilizador.Nome,
                                  NomeFilme = a.Filme.Titulo,
                                  UtilizadorFK = a.UtilizadorFK,
                                  FilmeFK = a.FilmeFK,
                                  Preco = a.Preco,
                                  DataInicio = a.DataInicio,
                                  DataFim = a.DataFim,

                              })
                              .Where(a => a.Id == id)
                              .FirstOrDefaultAsync();

            if (aluga == null)
            {
                return NotFound();
            }

            return aluga;
        }

        // PUT: api/AluguersAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluga(int id, Aluga aluga)
        {
            if (id != aluga.Id)
            {
                return BadRequest();
            }

            // transfere o valor do AuxPreco para Preco
            //aluga.Preco = Convert.ToDecimal(aluga.AuxPreco.Replace('.', ','));

            aluga.DataInicio = DateTime.Now;
            aluga.DataFim = aluga.DataInicio.AddMonths(12);

            _context.Entry(aluga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlugaExists(id))
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

        // POST: api/AluguersAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluga>> PostAluga([FromForm]Aluga aluga)
        {
            //aluga.AuxPreco = "14,99";
            // transfere o valor do AuxPreco para Preco
            //aluga.Preco = Convert.ToDecimal(aluga.AuxPreco.Replace('.', ','));

            aluga.DataInicio = DateTime.Now;
            aluga.DataFim = aluga.DataInicio.AddMonths(12);

            _context.Aluguers.Add(aluga);
            await _context.SaveChangesAsync();
            aluga = await _context.Aluguers.Include(x => x.Utilizador).Include(x => x.Filme).FirstAsync(x => x.Id == aluga.Id);
            return CreatedAtAction(nameof(GetAluga), new { id = aluga.Id }, aluga);
        }

        // DELETE: api/AluguersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluga(int id)
        {
            var aluga = await _context.Aluguers.FindAsync(id);
            if (aluga == null)
            {
                return NotFound();
            }

            _context.Aluguers.Remove(aluga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlugaExists(int id)
        {
            return _context.Aluguers.Any(e => e.Id == id);
        }
    }
}
