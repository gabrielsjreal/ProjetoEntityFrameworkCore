using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEntityFramework.Models;

namespace ProjetoEntityFramework.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationContext _context;
        public ProdutoController(ApplicationContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            // Como a tabela produto possui atributos ligado com categoria, é preciso dá um 'include(tabela desejada)'
            // Ou seja, toda a vez que for precisar listar um objeto e este objeto tiver relacionado a outro, tem que usar o include
            //var produtos = _context.Produto.Include(p => p.Categoria).ToList();

            var queryDeProduto = _context.Produto
               .Where(p => p.Ativo && p.Categoria.PermiteEstoque)
                 .OrderBy(p => p.Nome);

            

            if (!queryDeProduto.Any())
                return View(new List<Produto>());

            return View(queryDeProduto.ToList());

        }

        [HttpGet]
        public  IActionResult Editar(int id)
        {
            ViewBag.Categoria = _context.Categoria.ToList();
            var produto = _context.Produto.First(p => p.Id == id);
            return View("Salvar", produto);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var produto = _context.Produto.First(p => p.Id == id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            ViewBag.Categoria = _context.Categoria.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}