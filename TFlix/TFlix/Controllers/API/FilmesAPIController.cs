﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TFlix.Data;
using TFlix.Models;

namespace TFlix.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilmesAPIController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/FilmesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
            return await _context.Filmes
                              .OrderByDescending(a => a.Id)
                              .Select(a => new Filme
                              {
                                  Id = a.Id,
                                  Titulo = a.Titulo,
                                  Imagem = a.Imagem,
                                  Sinopse = a.Sinopse,
                                  DataLancamento = a.DataLancamento,
                                  Classificacao = a.Classificacao,
                                  Elenco = a.Elenco,
                                  Genero = a.Genero,
                              })
                              .ToListAsync();
        }

        // GET: api/FilmesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _context.Filmes
                                    .Select(a => new Filme
                                    {
                                        Id = a.Id,
                                        Titulo = a.Titulo,
                                        Imagem = a.Imagem,
                                        Sinopse = a.Sinopse,
                                        DataLancamento = a.DataLancamento,
                                        Classificacao = a.Classificacao,
                                        Elenco = a.Elenco,
                                        Genero = a.Genero,
                                    })
                                    .Where(a => a.Id == id)
                                    .FirstOrDefaultAsync(); ;

            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        // PUT: api/FilmesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, Filme filme)
        {
            if (id != filme.Id)
            {
                return BadRequest();
            }

            filme.Imagem = "semFoto.png";
            _context.Entry(filme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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

        // POST: api/FilmesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme([FromForm] Filme filme,IFormFile imagem)
        {

            if (imagem == null)
            {
                filme.Imagem = "semFoto.png";
            }
            else
            {
                if (!(imagem.ContentType == "image/jpeg" || imagem.ContentType == "image/png" || imagem.ContentType == "image/jpg"))
                {
                    // menssagem de erro
                    ModelState.AddModelError("", "Por favor, se pretende enviar um ficheiro, escolha uma imagem suportada.");
                }
                else
                {
                    // definir o nome da imagem
                    Guid g;
                    g = Guid.NewGuid();
                    string imageName = filme.Titulo + "_" + g.ToString();
                    string extensionOfImage = Path.GetExtension(imagem.FileName).ToLower();
                    imageName += extensionOfImage;
                    // adicionar o nome da imagem aos filmes
                    filme.Imagem = imageName;
                }

                // guardar a imagem no disco
                if (imagem != null)
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
                    await imagem.CopyToAsync(stream);
                }
            }




            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilme", new { id = filme.Id }, filme);
        }

        // DELETE: api/FilmesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmeExists(int id)
        {
            return _context.Filmes.Any(e => e.Id == id);
        }
    }
}
