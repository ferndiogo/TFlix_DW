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
        public async Task<IActionResult> Index(Subscricao subscricao)
        {

            //ViewBag.Filmes = _context.Filmes.ToList();
            var applicationDbContext = _context.Subscricoes
                .Include(s => s.Utilizador)
                .Include(s => s.Filmes)
                .Include(s => s.Series);
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
                .Include(s => s.Filmes)
                .Include(s => s.Series)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscricao == null)
            {
                return NotFound();
            }


            return View(subscricao);
        }

        [Authorize(Roles = "Administrador, Cliente")]
        // GET: Subscricoes/Create
        public IActionResult Create()
        {
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome");

            ViewBag.Filmes = _context.Filmes.ToList();
            ViewBag.Series = _context.Series.ToList();

            return View();
        }

        [Authorize(Roles = "Administrador, Cliente")]
        // POST: Subscricoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorFK,Filmes,Series,Duracao,Preco,DataInicio,DataFim")] Subscricao subscricao)
        {
            // transfer data from AuxPrice to Price
            //subscricao.Preco = Convert.ToDecimal(subscricao.AuxPreco.Replace('.', ','));

            subscricao.DataInicio = DateTime.Now;

            if (subscricao.Preco < 12)
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(1);
                subscricao.Duracao = 1;
            }
            else if (subscricao.Preco < 45 && subscricao.Preco > 13)
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(6);
                subscricao.Duracao = 6;
            }
            else 
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(12);
                subscricao.Duracao = 12;
            }
            

            string lstTags = Request.Form["ckeckFilmes"];
            string lstTags1 = Request.Form["ckeckSeries"];

            if (!string.IsNullOrEmpty(lstTags))
            {
                int[] splTags = lstTags.Split(',').Select(Int32.Parse).ToArray();

                if (splTags.Count() > 0)
                {
                    var PostTags = _context.Filmes.Where(w => splTags.Contains(w.Id)).ToList();

                    subscricao.Filmes.AddRange(PostTags);
                }
            }

            if (!string.IsNullOrEmpty(lstTags1))
            {
                int[] splTags1 = lstTags1.Split(',').Select(Int32.Parse).ToArray();

                if (splTags1.Count() > 0)
                {
                    var PostTags1 = _context.Series.Where(w => splTags1.Contains(w.Id)).ToList();

                    subscricao.Series.AddRange(PostTags1);
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(subscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Nome", subscricao.UtilizadorFK);
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

            ViewBag.Filmes = _context.Filmes.ToList();
            ViewBag.Series = _context.Series.ToList();

            var subscricao = await _context.Subscricoes.FindAsync(id);
            if (subscricao == null)
            {
                return NotFound();
            }
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "Id", "Id");
            return View(subscricao);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Subscricoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UtilizadorFK,Duracao,Preco,DataInicio,DataFim,Filmes,Series")] Subscricao subscricao)
        {
            if (id != subscricao.Id)
            {
                return NotFound();
            }

            // transfer data from AuxPrice to Price
            //subscricao.Preco = Convert.ToDecimal(subscricao.AuxPreco.Replace('.', ','));

            subscricao.DataInicio = DateTime.Now;

            if (subscricao.Preco < 12)
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(1);
                subscricao.Duracao = 1;
            }
            else if (subscricao.Preco < 45 && subscricao.Preco > 13)
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(6);
                subscricao.Duracao = 6;
            }
            else
            {
                subscricao.DataFim = subscricao.DataInicio.AddMonths(12);
                subscricao.Duracao = 12;
            }

            string lstTags = Request.Form["ckeckFilmes"];
            string lstTags1 = Request.Form["ckeckSeries"];

            if (!string.IsNullOrEmpty(lstTags))
            {
                int[] splTags = lstTags.Split(',').Select(Int32.Parse).ToArray();

                if (splTags.Count() > 0)
                {
                    var PostTags = _context.Filmes.Where(w => splTags.Contains(w.Id)).ToList();

                    
                    subscricao.Filmes.AddRange(PostTags);
                }
            }

            if (!string.IsNullOrEmpty(lstTags1))
            {
                int[] splTags1 = lstTags1.Split(',').Select(Int32.Parse).ToArray();

                if (splTags1.Count() > 0)
                {
                    var PostTags1 = _context.Series.Where(w => splTags1.Contains(w.Id)).ToList();

                    subscricao.Series.AddRange(PostTags1);
                }
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
                .Include(s => s.Filmes)
                .Include(s => s.Series)
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
