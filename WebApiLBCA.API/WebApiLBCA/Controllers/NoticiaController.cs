using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.Contrato;
using WebApiLBCA.Aplicacao.DTOs;

namespace WebApiLBCA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaServico _noticiaServico;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IClassificServico _classificServico;

        public NoticiaController(INoticiaServico noticiaServico, IUsuarioServico usuarioServico, IClassificServico classificServico)
        {
            _noticiaServico = noticiaServico;
            _usuarioServico = usuarioServico;
            _classificServico = classificServico;
        }

        [HttpGet("BuscaNoticia")]
        public async Task<IActionResult> OtbemNoticia(string login, string senha)
        {
            try
            {
                string acao = "Get";
                if (_usuarioServico.VerificaPermissaoDoLoginDoUsuario(acao, login))
                {
                    if (_usuarioServico.VerificaPermissaoDeRecuperarTodasNoticias(acao, login))
                    {
                        var noticia = await _noticiaServico.ObtemNoticiaServico();
                        if (noticia == null) return NoContent();

                        return Ok(noticia);
                    }
                    else
                    {
                        var noticia = await _noticiaServico.Obtem10NoticiaServico();
                        if (noticia == null) return NoContent();

                        return Ok(noticia);
                    }
                }
                return BadRequest("Usuário sem permissão!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar buscar noticia. Erro: {ex.Message}");
            }
        }

        [HttpPost("InsereNoticia")]
        public async Task<IActionResult> AdicionaNoticia(string login, string senha, NoticiaDtoRequest noticia)
        {
            try
            {
                string acao = "Post";

                if (_usuarioServico.VerificaPermissaoDoLoginDoUsuario(acao, login))
                {
                    if (await _usuarioServico.VerificaAutenticacaoUsuario(login, senha))
                    {
                        if (_classificServico.VerificaClassificacoesServico(noticia.Classificacao))//Verifica de contém "Outros" e mais.
                        {
                            return BadRequest("A notícia não poderá ser incluída por ter mais de uma classificação incluindo Outros");
                        }
                        else
                        {
                            var noticiaReturno = await _noticiaServico.IncluirNoticiaServico(noticia);
                            if (noticiaReturno == null) return NoContent();

                            return Ok(noticiaReturno);

                        }
                    }
                }
                return BadRequest($"Usuário {login} sem permissão para adicionar Notícia!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar a noticia. Erro: {ex.Message}");
            }
        }

        [HttpPut("AtualizaNoticia")]
        public async Task<IActionResult> AtualizaNoticia(string login, string senha, int id, NoticiaDtoRequest model)
        {
            try
            {
                string acao = "Put";

                if (_usuarioServico.VerificaPermissaoDoLoginDoUsuario(acao, login))
                {
                    if (await _usuarioServico.VerificaAutenticacaoUsuario(login, senha))
                    {
                        bool anuncio = await _noticiaServico.AtualizarNoticiaServico(id, model);
                        if (anuncio == false) return NoContent();

                        return Ok(anuncio);
                    }
                }
                return BadRequest($"Usuário {login} sem permissão para atualizar Notícia!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar a noticia. Erro: {ex.Message}");
            }
        }


        [HttpDelete("DeletaNoticiaPeloId")]
        public async Task<IActionResult> DeletaNoticia(string login, string senha, int id)
        {
            try
            {
                string acao = "Delete";

                if (_usuarioServico.VerificaPermissaoDoLoginDoUsuario(acao, login))
                {
                    if (await _usuarioServico.VerificaAutenticacaoUsuario(login, senha))
                    {
                        bool anuncio = await _noticiaServico.RemoverNoticiaServico(id);
                        if (anuncio == false) return NoContent();

                        return Ok(anuncio);
                    }
                }
                return BadRequest($"Usuário {login} sem permissão para remover Notícia!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar a noticia. Erro: {ex.Message}");
            }
        }
    }
}
