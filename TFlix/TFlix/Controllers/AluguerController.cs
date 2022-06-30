using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers
{
    public class AluguerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AluguerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aluguer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aluguers.Include(a => a.Filme).Include(a => a.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aluguer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aluguers == null)
            {
                return NotFound();
            }

            var aluga = await _context.Aluguers
                .Include(a => a.Filme)
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluga == null)
            {
                return NotFound();
            }

            return View(aluga);
        }

        // GET: Aluguer/Create
        public IActionResult Create()
        {
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Id");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View();
        }

        // POST: Aluguer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmeFK,UtilizadorFK,Preco,DataInicio,DataFim")] Aluga aluga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Id", aluga.FilmeFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", aluga.UtilizadorFK);
            return View(aluga);
        }

        // GET: Aluguer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aluguers == null)
            {
                return NotFound();
            }

            var aluga = await _context.Aluguers.FindAsync(id);
            if (aluga == null)
            {
                return NotFound();
            }
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Id", aluga.FilmeFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", aluga.UtilizadorFK);
            return View(aluga);
        }

        // POST: Aluguer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilmeFK,UtilizadorFK,Preco,DataInicio,DataFim")] Aluga aluga)
        {
            if (id != aluga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlugaExists(aluga.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Id", aluga.FilmeFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", aluga.UtilizadorFK);
            return View(aluga);
        }

        // GET: Aluguer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aluguers == null)
            {
                return NotFound();
            }

            var aluga = await _context.Aluguers
                .Include(a => a.Filme)
                .Include(a => a.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluga == null)
            {
                return NotFound();
            }

            return View(aluga);
        }

        // POST: Aluguer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aluguers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aluguers'  is null.");
            }
            var aluga = await _context.Aluguers.FindAsync(id);
            if (aluga != null)
            {
                _context.Aluguers.Remove(aluga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlugaExists(int id)
        {
          return _context.Aluguers.Any(e => e.Id == id);
        }
    }
}
