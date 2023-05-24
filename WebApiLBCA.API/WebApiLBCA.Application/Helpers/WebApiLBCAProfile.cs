using AutoMapper;
using WebApiLBCA.Aplicacao.DTOs;
using WebApiLBCA.Domain;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Aplicacao.Helpers
{
    public class WebApiLBCAProfile : Profile
    {
        public WebApiLBCAProfile()
        {
            CreateMap<Noticia, NoticiaDtoResponse>().ReverseMap();
            CreateMap<Noticia, NoticiaDtoRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Classificacao, ClassificacaoDto>().ReverseMap();            
        }
    }
}
