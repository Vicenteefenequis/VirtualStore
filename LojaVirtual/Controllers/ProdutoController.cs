using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        /*
         * Action Result
         * IActionResult
         */
        public ActionResult Visualizar()
        {
            Produto produto  = GetProduto();
            return View(produto);
           // return new ContentResult() { Content = "" , ContentType = "text/html"};
        }

        private Produto GetProduto()
        {
            return new Produto()
            {
                Id = 1,
                Nome = "Playstation 5",
                Descricao = "Jogue em 8 dimensoes",
                Valor = 2000.00M
            };
        }
    }
}
