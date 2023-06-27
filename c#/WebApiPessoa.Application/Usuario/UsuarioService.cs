using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiPessoa.Repository;
using WebApiPessoa.Repository.Models;

namespace WebApiPessoa.Application.Usuario
{
    public class UsuarioService
    {
        private readonly PessoaContext _context;
        public UsuarioService(PessoaContext context)
        {
            _context = context;
        }

        //métodos
        
        public bool InserirUsuario(UsuarioRequest request)
        {
            try  //tratamento de erros
            {
                var usuarioExiste = _context.Usuarios.FirstOrDefault(x => x.email == request.Email);
                
                if (usuarioExiste != null)
                {
                    return false;

                }
                var usuario = new TUsuario()
                {
                    nome = request.Nome,
                    usuario = request.Usuario,
                    senha = request.Senha,
                    email = request.Email,
                };
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TUsuario> ObterUsuarios()
        {
            try
            {
                var usuarios = _context.Usuarios.ToList();
                return usuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TUsuario ObterUsuario(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.id == id);
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AtualizarUsuario(int id, UsuarioRequest request)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(xx => xx.id == id);
                if (usuarioDb == null)
                    return false;

                usuarioDb.nome = request.Nome;
                usuarioDb.usuario = request.Usuario;
                usuarioDb.senha = request.Senha;
                usuarioDb.email = request.Email;

                _context.Usuarios.Update(usuarioDb);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoverUsuario(int id)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(xx => xx.id == id);
                if (usuarioDb == null)
                    return false;

                _context.Usuarios.Remove(usuarioDb);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
