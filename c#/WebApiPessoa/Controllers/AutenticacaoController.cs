using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApiPessoa.Application.Autenticacao;
using WebApiPessoa.Application.Eventos;
using WebApiPessoa.Application.Usuario;
using WebApiPessoa.Repository;

namespace WebApiPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {

       private readonly IAutenticacaoService _autenticacaoservice;
        public AutenticacaoController(IAutenticacaoService service)
        {
            
            _autenticacaoservice = service;
        
        }  //

        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            var resposta = _autenticacaoservice.Autenticar(request);

            if (resposta == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(new {token = resposta });
            }
        }

        [HttpPost]
        [Route("esquecisenha")]
        public IActionResult EsqueciSenha([FromBody] EsqueciSenhaRequest request)
        {
         var resposta = _autenticacaoservice.EsqueciSenha(request.Email);
            if (resposta)
                return NoContent();
            else
            {
                return BadRequest();
            }
        }
    }
}