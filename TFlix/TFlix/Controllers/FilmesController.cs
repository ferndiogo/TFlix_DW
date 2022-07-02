using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "Administrador, Subscritor, Alugueres")]
        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filmes.ToListAsync());
        }

        [Authorize(Roles = "Administrador, Alugueres")]
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

        [Authorize(Roles = "Administrador")]
        // GET: Filmes/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
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
                    // menssagem de erro
                    ModelState.AddModelError("", "Por favor, se pretende enviar um ficheiro, escolha uma imagem suportada.");
                    // reenvia o control para a view, com os dados do utilizador
                    return View(filme);
                }
                else
                {
                    // definir o nome da imagem
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = filme.Titulo + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(novoFilme.FileName).ToLower();
                    imageName += extensionOfImage;
                    // adicionar o nome da imagem aos filmes
                    filme.Imagem = imageName;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // adiciona os dados do filme à base de dados
                    _context.Add(filme);
                    // commit
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    
                    ModelState.AddModelError("", "Algo deu errado.");
                    
                    return View(filme);
                }
                // guardar a imagem no disco
                if (novoFilme != null)
                {
                    // pergunta ao servidor que endereço quer usar
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Filmes");
                    // ver se a diretoria existe se não cria
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    //guarda a imagem no disco
                    newImageLocalization = Path.Combine(newImageLocalization, filme.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novoFilme.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        [Authorize(Roles = "Administrador")]
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

            
            // guarda o ID do filme e o nome da imagem para assegurar que não há alterações no browser...
            HttpContext.Session.SetInt32("filmeD", filme.Id);

            HttpContext.Session.SetString("filmeImg", filme.Imagem);

            // envio dos dados para a View
            return View(filme);
        }

        [Authorize(Roles = "Administrador")]
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

                ModelState.AddModelError("", "Gastou mais tempo que o esperado...");
                return View(filme);
            }

            if (filmeIDGuardado != filme.Id)
            {
                ModelState.AddModelError("", "Algo deu errado.");
                return RedirectToAction("Index");
            }

            if (filmeImgGuardada != "semFoto.png")
            {
                System.IO.File.Delete("wwwroot//Fotos//Filmes//" + Path.Combine(filmeImgGuardada));
            }


            if (!(novoFilme.ContentType == "image/jpeg" || novoFilme.ContentType == "image/png" || novoFilme.ContentType == "image/jpg"))
            {
                // menssagem de erro
                ModelState.AddModelError("", "Por favor, se pretende enviar um ficheiro, escolha uma imagem suportada.");
                // reenvia o control para a view, com os dados do utilizador
                return View(filme);
            }
            else
            {
                // define o nome da imagem
                Guid g;
                g = Guid.NewGuid();
                string imageName = filme.Titulo + "_" + g.ToString();
                string extensionOfImage = Path.GetExtension(novoFilme.FileName).ToLower();
                imageName += extensionOfImage;
                // adiciona o nome da imagem aos dados do filme
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

                if (novoFilme != null)
                {
                    // pergunta ao servidor que endereço quer usar
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Filmes");
                    // verifica se a ditoria existe se não cria
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    // guarda a imagem no disco
                    newImageLocalization = Path.Combine(newImageLocalization, filme.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novoFilme.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
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
                // apaga a imagem caso não seja a imagem default do disco ao eliminar um filme
                if (filmeImgGuardada != "semFoto.png")
                {
                    System.IO.File.Delete("wwwroot//Fotos//Filmes//" + Path.Combine(filmeImgGuardada));
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Algo deu errado ao apagar a foto.");
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExiste(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
