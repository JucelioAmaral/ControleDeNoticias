using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.Contrato;
using WebApiLBCA.Aplicacao.DTOs;
using WebApiLBCA.Domain;
using WebApiLBCA.Dominio;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Application
{
    public class NoticiaServico : INoticiaServico
    {
        private readonly INoticiaRepo _noticiaRepo;
        private readonly IClassificRepo _classificRepo;
        private readonly IMapper _mapper;

        public NoticiaServico(INoticiaRepo noticiaRepo, IMapper mapper, IClassificRepo classificRepo)
        {
            _noticiaRepo = noticiaRepo;
            _classificRepo = classificRepo;
            _mapper = mapper;

        }

        public async Task<List<NoticiaDtoResponse>> ObtemNoticiaServico()
        {
            try
            {
                var ArrayNoticias = new List<NoticiaDtoResponse>();

                var noticias = await _noticiaRepo.SelecionaNoticiaRepo();
                if (noticias == null) return null;

                foreach (var noticia in noticias)
                {
                    var not = new NoticiaDtoResponse()
                    {
                        Titulo = noticia.Titulo,
                        InfoNoticia = noticia.InfoNoticia,
                        DataCadastro = DateTime.Now,
                        ClassificacaoId = noticia.ClassificacaoId
                    };
                    ArrayNoticias.Add(not);
                }
                return ArrayNoticias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }
        }

        public async Task<List<NoticiaDtoResponse>> Obtem10NoticiaServico()
        {
            try
            {
                var ArrayNoticias = new List<NoticiaDtoResponse>();

                var noticias = await _noticiaRepo.Seleciona10NoticiaRepo();
                if (noticias == null) return null;

                foreach (var noticia in noticias)
                {
                    var not = new NoticiaDtoResponse()
                    {
                        Titulo = noticia.Titulo,
                        InfoNoticia = noticia.InfoNoticia,
                        DataCadastro =noticia.DataCadastro,
                        ClassificacaoId = noticia.ClassificacaoId
                    };
                    ArrayNoticias.Add(not);
                }
                return ArrayNoticias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }
        }

        public async Task<List<NoticiaDtoResponse>> IncluirNoticiaServico(NoticiaDtoRequest model)
        {
            try
            {

                var ArrayNoticias = new List<NoticiaDtoResponse>();

                if (model.Classificacao.Count == 1)
                {
                    foreach (var classif in model.Classificacao)
                    {
                        Classificacao classificacao = await _classificRepo.ObtemClassificacoesRepo(classif.ClassifNoticia);
                        var novaNoticia = new Noticia()
                        {
                            Titulo = model.Titulo,
                            InfoNoticia = model.InfoNoticia,
                            DataCadastro = DateTime.Now,
                            ClassificacaoId = classificacao.Id
                        };
                        var noticia = _mapper.Map<Noticia>(novaNoticia);
                        var noticiaAdicionada = await _noticiaRepo.InsereNoticiaRepo(noticia);
                        if (noticiaAdicionada == null)
                        {
                            return null;
                        }
                        else
                        {
                            ArrayNoticias.Add(_mapper.Map<NoticiaDtoResponse>(noticia));
                            return ArrayNoticias;
                        }                        
                    }
                    return ArrayNoticias;
                }
                else
                {
                    foreach (var classif in model.Classificacao)
                    {
                        Classificacao classificacao = await _classificRepo.ObtemClassificacoesRepo(classif.ClassifNoticia);
                        var novaNoticia = new Noticia()
                        {
                            Titulo = model.Titulo,
                            InfoNoticia = model.InfoNoticia,
                            DataCadastro = DateTime.Now,
                            ClassificacaoId = classificacao.Id
                        };
                        var noticia = _mapper.Map<Noticia>(novaNoticia);
                        var noticiaAdicionada = await _noticiaRepo.InsereNoticiaRepo(noticia);
                        if (noticiaAdicionada == null)
                        {
                            return null;
                        }
                        else
                        {
                            ArrayNoticias.Add(_mapper.Map<NoticiaDtoResponse>(noticia));
                        }
                    }
                    return ArrayNoticias;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"IncluirNoticiaServico: Erro ao tentar Incluir Noticia. Erro: {ex.Message}");
            }
        }

        public async Task<bool> AtualizarNoticiaServico(int id, NoticiaDtoRequest model)
        {
            Noticia noticiaAtulizada = null;

            try
            {
                var noticia = await _noticiaRepo.SelecionaNoticiaRepoPorId(id);
                if (noticia != null)
                {
                    foreach (var classif in model.Classificacao)
                    {
                        Classificacao classificacao = await _classificRepo.ObtemClassificacoesRepo(classif.ClassifNoticia);
                        noticiaAtulizada = new Noticia()
                        {
                            Id = id,
                            Titulo = model.Titulo,
                            InfoNoticia = model.InfoNoticia,
                            DataCadastro = DateTime.Now,
                            ClassificacaoId = classificacao.Id
                        };
                    }
                    if (await _noticiaRepo.AtualziaNoticiaRepo(id, noticiaAtulizada) > 0) return true;
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoverNoticiaServico(int id)
        {
            try
            {
                var anuncio = await _noticiaRepo.SelecionaNoticiaRepoPorId(id);
                if (anuncio != null)
                {
                    if (await _noticiaRepo.RemoveNoticiaRepo(id) > 0) return true;
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
