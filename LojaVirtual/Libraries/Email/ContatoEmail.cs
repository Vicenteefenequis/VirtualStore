using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
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
            mensagem.From = new MailAddress("efenequis19981@gmail.com");
            mensagem.To.Add("efenequis19981@gmail.com");
            mensagem.Subject = "Contato - LojaVirutal - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;


            //Enviar Mensagem Via SMT
            /*
             SMTP -> Servidor que vai enviar a mensagem.
             */
            using (SmtpClient client = new SmtpClient())
            {
               
            }

        }
    }
}
