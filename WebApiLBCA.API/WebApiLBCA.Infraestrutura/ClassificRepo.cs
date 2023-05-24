

using Dapper;
using System.Data;
using System.Threading.Tasks;
using WebApiLBCA.Dominio;
using WebApiLBCA.Infraestrutura.Contexto;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Infraestrutura
{
    public class ClassificRepo : IClassificRepo
    {
        private readonly WebApiLBCAContexto _contexto;

        public ClassificRepo(WebApiLBCAContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Classificacao> ObtemClassificacoesRepo(string nomeClassificacao)
        {
            IDbConnection conn = _contexto.GetConnection();

            using (conn)
            {
                conn.Open();
                string query = @"SELECT *
                                 FROM tblClassificacao
                                 WHERE ClassifNoticia like @nomeClassificacao";
                Classificacao classifi = await conn.QueryFirstOrDefaultAsync<Classificacao>
                    (sql: query, param: new { nomeClassificacao });
                conn.Close();
                return classifi;
            }
        }
    }
}
