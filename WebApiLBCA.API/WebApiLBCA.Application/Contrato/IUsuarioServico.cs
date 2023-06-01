using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Aplicacao.DTOs;

namespace WebApiLBCA.Aplicacao.Contrato
{
    public interface IUsuarioServico
    {
        Task<bool> VerificaAutenticacaoUsuario(string login, string senha);
        bool VerificaPermissaoDoLoginDoUsuario(string verbo, string usuario);
        bool VerificaPermissaoDeRecuperarTodasNoticias(string verbo, string usuario);
    }
}
