using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.DTOs;

namespace WebApiLBCA.Aplicacao.Contrato
{
    public interface IClassificServico
    {        
        bool VerificaClassificacoesServico(List<ClassificacaoDto> classificacao);
    }
}
