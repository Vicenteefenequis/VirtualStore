using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp,IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public void EnviarContatoPorEmail(Contato contato)
        {
            string corpoMsg = string.Format("<h3>Contato - Loja Virutal</h3>" +
                "<b>Nome : </b> {0} <br/>" +
                "<b>E-mail : </b> {1} <br/>" +
                "<b>Texto : </b> {2} <br/>" +
                "<br/> E-mail enviado automaticamente do site LojaVirutal.",
                contato.Nome,
                contato.Email,
                contato.Texto
            );

            /*
             * MailMessage
             * 
             * Construir a mensagem
            */

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:ServerSMTP"));
            mensagem.To.Add("efenequis19981@gmail.com");
            mensagem.Subject = "Contato - LojaVirutal - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;


            //Enviar Mensagem Via SMT
            /*
             SMTP -> Servidor que vai enviar a mensagem.
             */

            _smtp.Send(mensagem);
           

        }
    }
}
