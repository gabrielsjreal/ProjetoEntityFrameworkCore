using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoEntityFramework.Models;

namespace ProjetoEntityFramework.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationContext _context;
        public CategoriaController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categoria = _context.Categoria.ToList();
            return View(categoria);
        }

        public IActionResult Editar(int id)
        {
            var categoria = _context.Categoria.First(c => c.Id == id);

            return View("Salvar", categoria);
        }


        public async Task<IActionResult> Excluir(int id)
        {
            var categoria = _context.Categoria.First(c => c.Id == id);
            _context.Categoria.Remove(categoria);
           await  _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Salvar()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria categoria)
        {
            // se o Id for 0 é porque o produto é novo, pois o id é preenchido automatizamente peo banco, e não pelo usuário
            if (categoria.Id == 0)
            {
                _context.Categoria.Add(categoria);
            }
            //Casa o Id não seja igual a 0, é porque é uma categoria já existente, e o usuário está querendo alterar o valor
            else
            {
                var categoriaJaSalva = _context.Categoria.First(c => c.Id == categoria.Id);
                categoriaJaSalva.Nome = categoria.Nome;
                categoriaJaSalva.PermiteEstoque = categoria.PermiteEstoque;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}