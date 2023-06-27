using System;
using System.Collections.Generic;
using System.Text;
using WebApiPessoa.Repository.Models;

namespace WebApiPessoa.Application.Autenticacao
{
    public interface IAutenticacaoService
    {
        bool EsqueciSenha(string email);
        string Autenticar(AutenticacaoRequest request);
    }

    }
  

