using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.DTOs;
using WebApiLBCA.Domain;

namespace WebApiLBCA.Aplicacao.Contrato
{
    public interface INoticiaServico
    {
        Task<List<NoticiaDtoResponse>> ObtemNoticiaServico();
        Task<List<NoticiaDtoResponse>> Obtem10NoticiaServico();        
        Task<List<NoticiaDtoResponse>> IncluirNoticiaServico(NoticiaDtoRequest model);
        Task<bool> AtualizarNoticiaServico(int id, NoticiaDtoRequest model);
        Task<bool> RemoverNoticiaServico(int id);        
    }
}
