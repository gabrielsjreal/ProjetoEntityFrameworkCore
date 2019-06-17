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
            var categorias = _context.Categoria.ToList();
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Salvar()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}