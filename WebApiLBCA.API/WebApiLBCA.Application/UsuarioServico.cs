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

        public bool VerificaPermissaoDoLoginDoUsuario(string verbo, string usuario)
        {
            try
            {
                switch (usuario)
                {
                    case "usuario_comum":
                        return false;
                    case "usuario_adm":
                        return VerificaPermissaoDeAlterarEExcluir(verbo, usuario);
                    case "usuario_editor":
                        return VerificaPermissaoDeInclusaoEAlteracao(verbo, usuario);
                    case "usuario_anonimo":
                        return Recupera10Noticas(verbo, usuario);
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

        public bool VerificaPermissaoDeInclusaoEAlteracao(string verbo, string usuario)
        {
            try
            {
                if (usuario == _configuration["Permissoes:DeInclusaoEAlteracao"] && verbo == "POST" || verbo == "PUT")
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

        public bool VerificaPermissaoDeAlterarEExcluir(string verbo, string usuario)
        {
            try
            {
                if (usuario == _configuration["Permissoes:DeAlterarEExcluir"] && verbo == "PUT" || verbo == "DELETE")
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

        public bool VerificaPermissaoDeRecuperarTodasNoticias(string verbo, string usuarioRecebido)
        {
            try
            {
                if (usuarioRecebido == _configuration["Permissoes:DeRecuperarTodasNoticias"] && verbo == "PUT" || verbo == "DELETE")
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

        public bool Recupera10Noticas(string verbo, string usuarioRecebido)
        {
            try
            {
                if (usuarioRecebido == _configuration["Permissoes:DeRecuperar10Noticias"] && verbo == "GET")
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
