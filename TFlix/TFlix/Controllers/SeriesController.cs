using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "Administrador, Subscritor, Alugueres")]
        // GET: Series
        public async Task<IActionResult> Index()
        {
            return View(await _context.Series.ToListAsync());
        }

        [Authorize(Roles = "Administrador, Subscritor, Alugueres")]
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

        [Authorize(Roles = "Administrador")]
        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
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
                    // menssagem de erro
                    ModelState.AddModelError("", "Por favor, se pretende enviar um ficheiro, escolha uma imagem suportada.");
                    // reenvia o control para a view, com os dados do utilizador
                    return View(serie);
                }
                else
                {
                    // definir o nome da imagem
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = serie.Titulo + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(novaSerie.FileName).ToLower();
                    imageName += extensionOfImage;
                    // adicionar o nome da imagem aos filmes
                    serie.Imagem = imageName;
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // adiciona os dados do filme à base de dados
                    _context.Add(serie);
                    // commit
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Something went wrong. I can not store data on database");

                    return View(serie);
                }
                // guardar a imagem no disco
                if (novaSerie != null)
                {
                    // pergunta ao servidor que endereço quer usar
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Series");
                    // ver se a diretoria existe se não cria
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    //guarda a imagem no disco
                    newImageLocalization = Path.Combine(newImageLocalization, serie.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novaSerie.CopyToAsync(stream);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return RedirectToAction("Index");
            }

            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return RedirectToAction("Index");
            }

            // guarda o ID da serie e o nome da imagem para assegurar que não há alterações no browser...
            HttpContext.Session.SetInt32("serieID", serie.Id);

            HttpContext.Session.SetString("serieImg", serie.Imagem);

            // envio dos dados para a View
            return View(serie);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Imagem,Sinopse,DataCriacao,Classificacao,Elenco,Genero,Temporada,Episodio")] Serie serie, IFormFile novaSerie)
        {
            if (id != serie.Id)
            {
                return NotFound();
            }

            var seriesIDGuardado = HttpContext.Session.GetInt32("serieID");
            var seriesImgGuardada = HttpContext.Session.GetString("serieImg");

            if (seriesIDGuardado == null)
            {

                ModelState.AddModelError("", "Gastou mais tempo que o esperado...");
                return View(serie);

            }

            if (seriesIDGuardado != serie.Id)
            {
                ModelState.AddModelError("", "Algo deu errado.");
                return RedirectToAction("Index");
            }

            if (novaSerie == null)
            {
                serie.Imagem = "semFoto.png";
            }
            else if (seriesImgGuardada != "semFoto.png")
            {
                System.IO.File.Delete("wwwroot//Fotos//Series//" + Path.Combine(seriesImgGuardada));
            }else if (!(novaSerie.ContentType == "image/jpeg" || novaSerie.ContentType == "image/png" || novaSerie.ContentType == "image/jpg"))
            {
                // menssagem de erro
                ModelState.AddModelError("", "Por favor, se pretende enviar um ficheiro, escolha uma imagem suportada.");
                // reenvia o control para a view, com os dados do utilizador
                return View(serie);
            }
            else
            {
                // define o nome da imagem
                Guid g;
                g = Guid.NewGuid();
                string imageName = serie.Titulo + "_" + g.ToString();
                string extensionOfImage = Path.GetExtension(novaSerie.FileName).ToLower();
                imageName += extensionOfImage;
                // adiciona o nome da imagem aos dados da serie
                serie.Imagem = imageName;
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
                    if (!serieExiste(serie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (novaSerie != null)
                {
                    // pergunta ao servidor que endereço quer usar
                    string addressToStoreFile = _webHostEnvironment.WebRootPath;
                    string newImageLocalization = Path.Combine(addressToStoreFile, "Fotos//Series");
                    // verifica se a ditoria existe se não cria
                    if (!Directory.Exists(newImageLocalization))
                    {
                        Directory.CreateDirectory(newImageLocalization);
                    }
                    // guarda a imagem no disco
                    newImageLocalization = Path.Combine(newImageLocalization, serie.Imagem);
                    using var stream = new FileStream(newImageLocalization, FileMode.Create);
                    await novaSerie.CopyToAsync(stream);
                }


                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        [Authorize(Roles = "Administrador")]
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

            HttpContext.Session.SetString("serieImg", serie.Imagem);

            return View(serie);
        }

        [Authorize(Roles = "Administrador")]
        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Serie serie)
        {
            var serie1 = await _context.Series.FindAsync(id);

            var seriesImgGuardada = HttpContext.Session.GetString("serieImg");

            try
            {
                _context.Series.Remove(serie1);
                await _context.SaveChangesAsync();

                // apaga a imagem caso não seja a imagem default do disco ao eliminar uma série
                if (seriesImgGuardada != "semFoto.png")
                {
                    System.IO.File.Delete("wwwroot//Fotos//Series//" + Path.Combine(seriesImgGuardada));
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Algo deu errado ao apagar a foto.");
                return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));

        }

        private bool serieExiste(int id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
