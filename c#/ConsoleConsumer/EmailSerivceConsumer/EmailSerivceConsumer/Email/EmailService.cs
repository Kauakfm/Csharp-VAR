using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace EmailSerivceConsumer.Email
{
    public class EmailService
    {
        public void EnviarEmail(string nomeRemetente, string emailDestinatario, string assunto, string conteudo)
        {

            var porta = 587;
            var smtp = "smpt.titan.email";
            var isSSL = false;
            var usuario = "kaua.martins@varsolutions.com.br";
            var senha = "Var@1234";

            var objEmail = new MailMessage(usuario, emailDestinatario, assunto, conteudo);
            objEmail.From = new MailAddress(nomeRemetente + "<" + usuario + ">");
            objEmail.IsBodyHtml = true;
        }
    }
}
