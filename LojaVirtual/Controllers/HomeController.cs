using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.Database;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsletterRepository _repositoryNewsletter;
        public HomeController(IClienteRepository repositoryCliente, INewsletterRepository repositoryNewsletter)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsletter = repositoryNewsletter;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsletter.Cadastrar(newsletter);
                TempData["MSG_SUCESSO"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
         
        }
        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();

                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];


                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid = Validator.TryValidateObject(contato, contexto, listaMensagens,true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_SUCESSO"] = "Mensagem de Contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_ERROR"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }

            }
            catch (Exception ex2)
            {
                ViewData["MSG_ERROR"] = "Opps! Tivemos um erro, tente novamente mais tarde!";
                 /*
                 * TODO - IMPLEMENTAR LOG
                 */
            }
            return View("Contato");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            if(cliente.Email == "vicente19981@live.com" && cliente.Senha == "1234")
            {
                //Email, Senha , ID , Nome, CPF
                HttpContext.Session.Set("ID",new byte[] {52});
                HttpContext.Session.SetString("Email",cliente.Email);
                HttpContext.Session.SetInt32("Idade",24);
                return new ContentResult() { Content="Logado!"};
                //Logado
            }
            else
            {
                return new ContentResult() { Content = "Não Logado!" };
                //Não Logado
            }
        }
        [HttpGet]
        public IActionResult Painel()
        {
            byte[] UsuarioID;
            if (HttpContext.Session.TryGetValue("ID", out UsuarioID))
            {
                return new ContentResult() { Content=" Usuário " + UsuarioID[0] + ".Logado !" + "Email: " + HttpContext.Session.GetString("Email") + "Idade: " + HttpContext.Session.GetString("Idade")};
            }
            else
            {
                return new ContentResult() { Content = "Acesso negado." };
            }

        }
        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(cliente);
                TempData["MSG_SUCESSO"] = "Cadastro realizado com Sucesso!";
                //TODO - Implementar redirecionamentos diferentes(Painel, Carrinho de Compras etc).

                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }
        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
