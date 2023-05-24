using System;
using System.Collections.Generic;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Domain
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string InfoNoticia { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? ClassificacaoId { get; set; }        
        public Classificacao Classificacao { get; set; }        
    }
}
