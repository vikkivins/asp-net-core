using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFCore.WebAPI.Data;
using EFCore.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioContext _context;

        public FuncionarioController(FuncionarioContext context)
        {
            _context = context;
        }

        // GET: api/Funcionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.Include(f => f.Departamento).ToListAsync();

            // Se a lista de funcionários estiver vazia, reinicia o contador do ID
            if (!funcionarios.Any())
            {
                await _context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Funcionarios', RESEED, 0)");
            }

            return funcionarios;
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.Include(f => f.Departamento)
                                                         .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // PUT: api/Funcionario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionario
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
        }

        // GET: api/Funcionario/departamento/5
        [HttpGet("departamento/{departamentoId}")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionariosByDepartamento(int departamentoId)
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Departamento)
                .Where(f => f.DepartamentoId == departamentoId)
                .ToListAsync();

            if (!funcionarios.Any())
            {
                return NotFound($"Nenhum funcionário encontrado para o departamento com ID {departamentoId}");
            }

            return funcionarios;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFoto([FromForm] IFormFile foto, [FromForm] string nomeFuncionario, [FromForm] string departamentoNome)
        {
            if (foto == null || foto.Length == 0)
            {
                return Ok(new { caminho = string.Empty });
            }

            // Obter o caminho base da aplicação
            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadPath = Path.Combine(webRootPath, "assets", "funcionarios-fotos");

            // Criar diretório se não existir
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Sanitizar o nome do funcionário (remover espaços e deixar tudo minúsculo)
            var nomeSanitizado = nomeFuncionario.ToLower().Replace(" ", "");

            // Verificar se o arquivo já existe, e caso exista, adicionar um número sequencial
            var fileName = $"{nomeSanitizado}{Path.GetExtension(foto.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);
            int count = 1;

            // Verificar se o arquivo já existe e adicionar um número entre parênteses se necessário
            while (System.IO.File.Exists(filePath))
            {
                fileName = $"{nomeSanitizado}({count}){Path.GetExtension(foto.FileName)}";
                filePath = Path.Combine(uploadPath, fileName);
                count++;
            }

            // Salvar a foto no diretório especificado
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Retorna o caminho relativo para o frontend
            var fotoPath = $"/assets/funcionarios-fotos/{fileName}";
            return Ok(new { caminho = fotoPath });
        }



        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcionario>> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return funcionario;
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
