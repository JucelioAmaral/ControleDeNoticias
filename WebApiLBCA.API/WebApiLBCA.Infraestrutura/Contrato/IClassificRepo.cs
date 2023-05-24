using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Infraestrutura.Contrato
{
    public interface IClassificRepo
    {
        Task<Classificacao> ObtemClassificacoesRepo(string nomeClassificacao);
    }
}
