using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApiPessoa.Application.Eventos;
using WebApiPessoa.Application.Eventos.Models;
using WebApiPessoa.Repository;
using WebApiPessoa.Repository.Models;

namespace WebApiPessoa.Application.Autenticacao
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly PessoaContext _context;
        private readonly IRabbitMQProducer _producer;
    
        public AutenticacaoService(PessoaContext context, IRabbitMQProducer producer )
        {
            _context = context;
            _producer = producer;
            
        }   
        public bool EsqueciSenha(string email)
        {
            try
            {
                var usuarioExiste = _context.Usuarios.FirstOrDefault(x => x.email == email);
                if (usuarioExiste == null)
                {
                    return false;
                }

                var esqueciSenhaModel = new EsqueciSenhaModel()
                {
                    Email = email,
                    Assunto = "Acho que você esqueceu a senha pc ai to enviando ai pra tu certo tchau brigadu!",
                    Texto = $"AI como se pediu pfv to te mandando a senha irmão pdp ? { usuarioExiste.senha}  di nada ",
                };

                _producer.EnviarMensagem(esqueciSenhaModel, "Var.Notificacao.Email", "Var.Notificacao", "Var.Notificacao");
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        
        public string Autenticar(AutenticacaoRequest request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.usuario == request.UserName && x.senha == request.Password);  //
            if (usuario != null)
            {
                var tokenString = GerarTokenJwt(usuario);
                return tokenString;
            }
            else
            {
                return null;
            }
        }
        private string GerarTokenJwt(TUsuario usuario)
        {
            var issuer = "var";  //quem está emitindo o token
            var audience = "var";  //destinatário da api
            var key = "1c93a5c9-1b8d-4f3c-ba71-65954542cc4e";  //chave secreta

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("usuarioId", usuario.id.ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer, claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
