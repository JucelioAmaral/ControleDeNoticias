using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.Contrato;
using WebApiLBCA.Aplicacao.DTOs;
using WebApiLBCA.Dominio;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Aplicacao
{
    public class ClassificServico : IClassificServico
    {
        private readonly IClassificRepo _classificRepo;

        public ClassificServico(IClassificRepo classificRepo)
        {
            _classificRepo = classificRepo;
        }

        public bool VerificaClassificacoesServico(List<ClassificacaoDto> classificacao)
        {
            try
            {
                bool encontradoOutros = false;

                if (classificacao.Count > 1)
                {
                    foreach (var classif in classificacao)
                    {
                        if (classif.ClassifNoticia.Equals("Outros"))
                        {
                            encontradoOutros = true;
                            break;                            
                        }                      
                    }                    
                }
                return encontradoOutros;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
