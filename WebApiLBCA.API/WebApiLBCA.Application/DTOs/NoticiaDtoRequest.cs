using System.Collections.Generic;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Aplicacao.DTOs
{
    public class NoticiaDtoRequest
    {
        public string Titulo { get; set; }
        public string InfoNoticia { get; set; }        
        public List<ClassificacaoDto> Classificacao { get; set; }
    }
}
