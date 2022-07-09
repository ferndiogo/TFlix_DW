using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TFlix.Data;
using TFlix.Models;


namespace TFlix.Controllers
{
    public class SubscricoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        public SubscricoesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Subscricoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Subscricoes.Include(s => s.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        // GET: Subscricoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subscricoes == null)
            {
                return NotFound();
            }

            var subscricao = await _context.Subscricoes
                .Include(s => s.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscricao == null)
            {
                return NotFound();
            }

            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Subscricoes/Create
        public IActionResult Create()
        {
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome");

            ViewBag.Filmes = _context.Filmes.ToList();
            ViewBag.Series = _context.Series.ToList();

            return View();
        }

        [Authorize(Roles = "Administrador")]
        // POST: Subscricoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorFK,Filmes,Series,,Duracao,AuxPreco,Preco,DataInicio,DataFim")] Subscricao subscricao)
        {

            // transfer data from AuxPrice to Price
            subscricao.Preco = Convert.ToDecimal(subscricao.AuxPreco.Replace('.', ','));

            string lstTags = Request.Form["ckeckFilmes"];

            if (!string.IsNullOrEmpty(lstTags))
            {
                int[] splTags = lstTags.Split(',').Select(Int32.Parse).ToArray();

                if (splTags.Count() > 0)
                {
                    var PostTags = _context.Filmes.Where(w => splTags.Contains(w.Id)).ToList();

                    subscricao.Filmes.AddRange(PostTags);
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(subscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", subscricao.UtilizadorFK);
            ViewData["Filmes"] = new SelectList(_context.Filmes, "Id", "Nome", subscricao.Filmes);
            ViewData["Series"] = new SelectList(_context.Series, "Id", "Id", subscricao.Series);
            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Subscricoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subscricoes == null)
            {
                return NotFound();
            }

            var subscricao = await _context.Subscricoes.FindAsync(id);
            if (subscricao == null)
            {
                return NotFound();
            }
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id", subscricao.UtilizadorFK);
            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Subscricoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtilizadorFK,Duracao,Preco,DataInicio,DataFim")] Subscricao subscricao)
        {
            if (id != subscricao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subscricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscricaoExists(subscricao.Id))
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
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", subscricao.UtilizadorFK);
            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Subscricoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subscricoes == null)
            {
                return NotFound();
            }

            var subscricao = await _context.Subscricoes
                .Include(s => s.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscricao == null)
            {
                return NotFound();
            }

            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Subscricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subscricoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Subscricoes'  is null.");
            }
            var subscricao = await _context.Subscricoes.FindAsync(id);
            if (subscricao != null)
            {
                _context.Subscricoes.Remove(subscricao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscricaoExists(int id)
        {
            return _context.Subscricoes.Any(e => e.Id == id);
        }
        
        public async Task<IActionResult> AlterarRole()
        {
            var userData = await _userManager.GetUserAsync(User);
            await _userManager.RemoveFromRoleAsync(@userData, "Cliente");
            await _userManager.AddToRoleAsync(@userData, "Subscritor");
            return RedirectToAction(nameof(Index));
        }


    }
}
