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
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilmesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Filmes.ToListAsync());
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return NotFound();
            }

            var filme = await _context.Filmes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Imagem,Sinopse,DataCriacao,Classificacao,Elenco,Genero")] Filme filme, IFormFile novoFilme)
        {
            if (novoFilme == null)
            {
                filme.Imagem = "semFoto.png";
            }
            else
            {
                if (!(novoFilme.ContentType == "image/jpeg" || novoFilme.ContentType == "image/png" || novoFilme.ContentType == "image/jpg"))
                {
                    // write the error message
                    ModelState.AddModelError("", "Please, if you want to send a file, please choose an image...");
                    // resend control to view, with data provided by user
                    return View(filme);
                }
                else
                {
                    // define image name
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = filme.Titulo + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(novoFilme.FileName).ToLower();
                    imageName += extensionOfImage;
                    // add image name to vet data
                    filme.Imagem = imageName;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add vet data to database
                    _context.Add(filme);
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
                    return View(filme);
                }
                // save image file to disk
                // ********************************
                if (novoFilme != null)
                {
                    // ask the server what address it wants to use
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Filmes");
                    // see if the folder 'Photos' exists
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    // save image file to disk
                    newImageLocalization = Path.Combine(newImageLocalization, filme.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novoFilme.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return RedirectToAction("Index");
            }

            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
            {
                return RedirectToAction("Index");
            }

            /* O que quero fazer?
          * Guardar o ID do médico veterinário para assegurar que não há alterações no browser...
          */
            // Session["vet"]= medicoVeterinario.Id;
            // equivalente ao trabalho que antes era feito com as Var. Session
            HttpContext.Session.SetInt32("filmeD", filme.Id);

            HttpContext.Session.SetString("filmeImg", filme.Imagem);

            // envio dos dados para a View
            return View(filme);
        }

        // POST: Filmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Imagem,Sinopse,DataCriacao,Classificacao,Elenco,Genero")] Filme filme, IFormFile novoFilme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            var filmeIDGuardado = HttpContext.Session.GetInt32("filmeD");
            var filmeImgGuardada = HttpContext.Session.GetString("filmeImg");

            if (filmeIDGuardado == null)
            {
                // what we need to do?
                // we must decide...

                ModelState.AddModelError("", "You have spent more time than allowed...");
                return View(filme);
                // return RedirectToAction("Index");
            }

            if (filmeIDGuardado != filme.Id)
            {
                // if we enter here, something is wrong
                // what we need to do?????

                return RedirectToAction("Index");
            }

            if (filmeImgGuardada != "semFoto.png")
            {
                System.IO.File.Delete("wwwroot//Fotos//Filmes//" + Path.Combine(filmeImgGuardada));
            }


            if (!(novoFilme.ContentType == "image/jpeg" || novoFilme.ContentType == "image/png" || novoFilme.ContentType == "image/jpg"))
            {
                // write the error message
                ModelState.AddModelError("", "Please, if you want to send a file, please choose an image...");
                // resend control to view, with data provided by user
                return View(filme);
            }
            else
            {
                // define image name
                Guid g;
                g = Guid.NewGuid();
                string imageName = filme.Titulo + "_" + g.ToString();
                string extensionOfImage = Path.GetExtension(novoFilme.FileName).ToLower();
                imageName += extensionOfImage;
                // add image name to vet data
                filme.Imagem = imageName;
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExiste(filme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                // save image file to disk
                // ********************************
                if (novoFilme != null)
                {
                    // ask the server what address it wants to use
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Filmes");
                    // see if the folder 'Photos' exists
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    // save image file to disk
                    newImageLocalization = Path.Combine(newImageLocalization, filme.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novoFilme.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filmes == null)
            {
                return NotFound();
            }

            var filme = await _context.Filmes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("filmeImg", filme.Imagem);

            return View(filme);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme1 = await _context.Filmes.FindAsync(id);

            var filmeImgGuardada = HttpContext.Session.GetString("filmeImg");

            try
            {
                _context.Filmes.Remove(filme1);
                await _context.SaveChangesAsync();

                if (filmeImgGuardada != "semFoto.png")
                {
                    System.IO.File.Delete("wwwroot//Fotos//Filmes//" + Path.Combine(filmeImgGuardada));
                }


                /*
                 * you must delete the user's photo
                 * IF the user is not using the 'noVet.jpg'
                 */
            }
            catch (Exception)
            {
                // what is going to be done in the 'catch' code?
                //  throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExiste(int id)
        {
          return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
