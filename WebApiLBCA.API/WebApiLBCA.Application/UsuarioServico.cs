using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Configuration;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.Contrato;
using WebApiLBCA.Aplicacao.DTOs;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Aplicacao
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepo _usuarioRepo;
        private IConfiguration _configuration;

        public UsuarioServico(IUsuarioRepo usuarioRepo, IConfiguration config)
        {
            _usuarioRepo = usuarioRepo;
            _configuration = config;
        }

        public async Task<bool> VerificaAutenticacaoUsuario(string login, string senha)
        {
            try
            {
                var loginRetorno = await _usuarioRepo.ObtemUsuario(login);
                if (loginRetorno == null)
                {
                    return false;
                }
                else
                {
                    if (loginRetorno.Login == login && loginRetorno.Senha == senha)
                    {
                        switch (loginRetorno.Login)
                        {
                            case "usuario_comum":
                                return true;
                            case "usuario_adm":
                                return true;
                            case "usuario_editor":
                                return true;
                            default:
                                return false;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaPermissaoDoLoginDoUsuario(string acao, string usuario)
        {
            try
            {
                switch (usuario)
                {
                    case "usuario_comum":
                        return false;
                    case "usuario_adm":
                        return VerificaPermissaoDeAlterarEExcluir(acao, usuario);
                    case "usuario_editor":
                        return VerificaPermissaoDeInclusaoEAlteracao(acao, usuario);
                    case "usuario_anonimo":
                        return Recupera10Noticas(acao, usuario);
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaPermissaoDeRecuperacaoDeNoticias(string acao, string usuario)
        {
            try
            {
                switch (usuario)
                {
                    case "usuario_comum":
                        return false;
                    case "usuario_adm":
                        return VerificaPermissaoDeRecuperarTodasNoticias(acao, usuario);
                    case "usuario_editor":
                        return VerificaPermissaoDeRecuperarTodasNoticias(acao, usuario);
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaPermissaoDeInclusaoEAlteracao(string acao, string usuario)
        {
            try
            {
                if (usuario == _configuration["Permissoes:DeInclusaoEAlteracao"] && acao == "Post" || acao == "Put")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaPermissaoDeAlterarEExcluir(string acao, string usuario)
        {
            try
            {
                if (usuario == _configuration["Permissoes:DeAlterarEExcluir"] && acao == "Put" || acao == "Delete")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaPermissaoDeRecuperarTodasNoticias(string acao, string usuarioRecebido)
        {
            try
            {
                if (usuarioRecebido == _configuration["Permissoes:DeRecuperarTodasNoticias"] && acao == "Put" || acao == "Delete")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Recupera10Noticas(string acao, string usuarioRecebido)
        {
            try
            {
                if (usuarioRecebido == _configuration["Permissoes:DeRecuperar10Noticias"] && acao == "Get")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
    }
}
