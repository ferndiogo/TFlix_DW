using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers
{
    public class AluguerController : Controller
    {
        /// <summary>
        /// Este atributo referencia a base de dados do projeto
        /// </summary>
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public AluguerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Aluguer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aluguers.Include(a => a.Filme).Include(a => a.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        // GET: Aluguer/Create
        public IActionResult Create()
        {
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Titulo");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "Administrador")]
        // POST: Aluguer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmeFK,UtilizadorFK,AuxPreco,Preco,DataInicio,DataFim")] Aluga aluga)
        {

            // transfere o valor do AuxPreco para Preco
            aluga.Preco = Convert.ToDecimal(aluga.AuxPreco.Replace('.', ','));

            aluga.DataInicio = DateTime.Now;
            aluga.DataFim = aluga.DataInicio.AddMonths(12);
            
            if (ModelState.IsValid)
            {
                _context.Add(aluga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", aluga.UtilizadorFK);
            ViewData["Filmes"] = new SelectList(_context.Filmes, "Id", "Id", aluga.FilmeFK);
            return View(aluga);
        }

        [Authorize(Roles = "Administrador")]
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
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Titulo", aluga.FilmeFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", aluga.UtilizadorFK);
            return View(aluga);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Aluguer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilmeFK,UtilizadorFK,AuxPreco,Preco,DataInicio,DataFim")] Aluga aluga)
        {
            // transfere o valor do AuxPreco para Preco
            aluga.Preco = Convert.ToDecimal(aluga.AuxPreco.Replace('.', ','));

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
            ViewData["FilmeFK"] = new SelectList(_context.Filmes, "Id", "Titilo", aluga.FilmeFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", aluga.UtilizadorFK);
            return View(aluga);
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
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

        public async Task<IActionResult> AlterarRole()
        {
            var userData = await _userManager.GetUserAsync(User);
            await _userManager.RemoveFromRoleAsync(@userData, "Cliente");
            await _userManager.AddToRoleAsync(@userData, "Alugueres");
            return RedirectToAction(nameof(Index));

        }

        
    }
}
