using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;

namespace LojaVirtual.Libraries.Filtro
{

    /**
     Tipos de Filtros: 
     Autorização
     Recurso
     Ação
     Exceção
     Resultado
     */
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Cliente cliente = _loginCliente.GetCliente();
            if(cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso Negado" };
            }
        }
    };
}
