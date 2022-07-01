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
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SeriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
              return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Imagem,Sinopse,DataCriacao,Classificacao,Elenco,Genero,Temporada,Episodio")] Serie serie, IFormFile novaSerie)
        {


            if (novaSerie == null)
            {
                serie.Imagem = "semFoto.png";
            }
            else
            {
                if (!(novaSerie.ContentType == "image/jpeg" || novaSerie.ContentType == "image/png" || novaSerie.ContentType == "image/jpg"))
                {
                    // write the error message
                    ModelState.AddModelError("", "Please, if you want to send a file, please choose an image...");
                    // resend control to view, with data provided by user
                    return View(serie);
                }
                else
                {
                    // define image name
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = serie.Titulo + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(novaSerie.FileName).ToLower();
                    imageName += extensionOfImage;
                    // add image name to vet data
                    serie.Imagem = imageName;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add vet data to database
                    _context.Add(serie);
                    // commit
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // if the code arrives here, something wrong has appended
                    // we must fix the error, or at least report it

                    // add a model error to our code
                    ModelState.AddModelError("", "Something went wrong. I can not store data on database");
                    // eventually, before sending control to View
                    // report error. For instance, write a message to the disc
                    // or send an email to admin              

                    // send control to View
                    return View(serie);
                }
                // save image file to disk
                // ********************************
                if (novaSerie != null)
                {
                    // ask the server what address it wants to use
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Series");
                    // see if the folder 'Photos' exists
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    // save image file to disk
                    newImageLocalization = Path.Combine(newImageLocalization, serie.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novaSerie.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Imagem,Sinopse,DataCriacao,Classificacao,Elenco,Genero,Temporada,Episodio")] Serie serie)
        {
            if (id != serie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SerieExists(serie.Id))
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
            return View(serie);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Series == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Series'  is null.");
            }
            var serie = await _context.Series.FindAsync(id);
            if (serie != null)
            {
                _context.Series.Remove(serie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SerieExists(int id)
        {
          return _context.Series.Any(e => e.Id == id);
        }
    }
}
