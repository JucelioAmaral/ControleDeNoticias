using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiLBCA.Dominio;
using WebApiLBCA.Infraestrutura.Contexto;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Infraestrutura
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly WebApiLBCAContexto _contexto;

        public UsuarioRepo(WebApiLBCAContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> ObtemUsuario(string login)
        {
            IDbConnection conn = _contexto.GetConnection();

            using (conn)
            {
                conn.Open();
                string query = @"SELECT *
                                 FROM tblUsuario
                                 WHERE Login like @login";
                Usuario usuario = await conn.QueryFirstOrDefaultAsync<Usuario>
                    (sql: query, param: new { login });
                conn.Close();
                return usuario;
            }
        }
    }
}
