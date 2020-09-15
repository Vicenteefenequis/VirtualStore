using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Texto;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {

        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;


        public ColaboradorController(IColaboradorRepository colaboradorRepository,GerenciarEmail gerenciarEmail)
        {
            _colaboradorRepository = colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }
        public IActionResult Index(int? pagina)
        {
           IPagedList<Models.Colaborador> colaboradores =  _colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult GerarSenha(int id)
        {

            Models.Colaborador colaborador =_colaboradorRepository.ObterColaborador(id);
            colaborador.Senha = KeyGenerator.GetUniqueKey(8);
            _colaboradorRepository.Atualizar(colaborador);
            
            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

            TempData["MSG_SUCESSO"] = Mensagem.MSG_SUCESSO003;

            return RedirectToAction(nameof(Index));



        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                //TODO - GERAR SENHA ALEATORIO, SALVAR NOVA , ENIVAR O E-MAIL
                colaborador.Tipo = "C";
                _colaboradorRepository.Cadastrar(colaborador);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SUCESSO001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
           Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
           return View(colaborador);
        }
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador,int id)
        {
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                
                TempData["MSG_SUCESSO"] = Mensagem.MSG_SUCESSO001;
               
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            TempData["MSG_SUCESSO"] = Mensagem.MSG_SUCESSO002;

            return RedirectToAction(nameof(Index));
        }
    }
}
