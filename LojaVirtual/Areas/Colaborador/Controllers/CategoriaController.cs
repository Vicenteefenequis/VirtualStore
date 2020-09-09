using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filtro;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    //TODO - Habilitar Verificação de Login
    //[ColaboradorAutorizacao]
    public class CategoriaController : Controller
    {
        
        private ICategoriaRepository _categoriasRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriasRepository = categoriaRepository;
        }
        public IActionResult Index(int? pagina)
        {
          
            var categorias = _categoriasRepository.ObterTodasCategorias(pagina);
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriasRepository.Cadastrar(categoria);


                TempData["MSG_SUCESSO"] = "Registro salvo com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm]Categoria categoria)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            return View();
        }

    }
}
