using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Domain;

namespace WebApiLBCA.Infraestrutura.Contrato
{
    public interface INoticiaRepo
    {
        Task<IEnumerable<Noticia>> SelecionaNoticiaRepo();
        Task<IEnumerable<Noticia>> Seleciona10NoticiaRepo();
        Task<Noticia> SelecionaNoticiaRepoPorId(int id);
        Task<Noticia> InsereNoticiaRepo(Noticia noticia);
        Task<int> AtualziaNoticiaRepo(int id, Noticia noticia);
        Task<int> RemoveNoticiaRepo(int id);
    }
}
