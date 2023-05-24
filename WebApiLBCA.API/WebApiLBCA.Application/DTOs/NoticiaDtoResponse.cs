using System;
using System.Collections.Generic;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Aplicacao.DTOs
{
    public class NoticiaDtoResponse
    {
        public string Titulo { get; set; }
        public string InfoNoticia { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? ClassificacaoId { get; set; }
    }
}
