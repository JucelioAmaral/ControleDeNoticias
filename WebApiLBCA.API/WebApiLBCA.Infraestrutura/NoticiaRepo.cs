using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApiLBCA.Domain;
using WebApiLBCA.Infraestrutura.Contexto;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Infraestrutura
{
    public class NoticiaRepo : INoticiaRepo
    {
        private readonly WebApiLBCAContexto _context;

        public NoticiaRepo(WebApiLBCAContexto context)
        {
            _context = context;
        }

        public async Task<Noticia> SelecionaNoticiaRepoPorId(int id)
        {
            IDbConnection conn = _context.GetConnection();

            using (conn)
            {
                conn.Open();
                string query = @"SELECT * FROM tblNoticia WHERE Id = @id";
                Noticia noticia = await conn.QueryFirstOrDefaultAsync<Noticia>
                    (sql: query, param: new { id });
                conn.Close();
                return noticia;
            }
        }

        public async Task<int> AtualziaNoticiaRepo(int id, Noticia noticia)
        {
            IDbConnection conn = _context.GetConnection();

            using (conn)
            {
                conn.Open();
                string command = $@"UPDATE tblNoticia
                                  SET Titulo = @titulo,
                                      InfoNoticia = @infoNoticia,
                                      DataCadastro = @dataCadastro,                                  
                                      ClassificacaoId = @classificacaoId
                                  WHERE Id = {id} ";
                var valor = await conn.ExecuteAsync(sql: command, param: noticia);
                conn.Close();
                return valor;
            }
        }

        public async Task<Noticia> InsereNoticiaRepo(Noticia noticia)
        {
            IDbConnection conn = _context.GetConnection();

            using (conn)
            {
                conn.Open();
                string command = @"INSERT INTO tblNoticia(Titulo, InfoNoticia, DataCadastro, ClassificacaoId) VALUES(@titulo,@infoNoticia,@dataCadastro,@classificacaoId)";

                var resultado = await conn.ExecuteAsync(sql: command, param: noticia);
                if (resultado > 0)
                {
                    conn.Close();
                    return noticia;
                }
                conn.Close();
                return null;
            }
        }

        public async Task<int> RemoveNoticiaRepo(int id)
        {
            IDbConnection conn = _context.GetConnection();

            using (conn)
            {
                conn.Open();
                string command = @"DELETE FROM tblNoticia WHERE Id = @id";
                var valor = await conn.ExecuteAsync(sql: command, param: new { id });
                conn.Close();
                return valor;
            }
        }

        public async Task<IEnumerable<Noticia>> SelecionaNoticiaRepo()
        {
            try
            {
                IDbConnection conn = _context.GetConnection();
                try
                {
                    using (conn)
                    {
                        conn.Open();
                        return await conn.QueryAsync<Noticia>(@"SELECT * FROM tblNoticia ORDER BY DataCadastro DESC");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw new Exception("SelecionaNoticiaRepo1: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelecionaNoticiaRepo2: " + ex.Message);
            }
        }

        public async Task<IEnumerable<Noticia>> Seleciona10NoticiaRepo()
        {
            try
            {
                IDbConnection conn = _context.GetConnection();
                try
                {
                    using (conn)
                    {
                        conn.Open();
                        return await conn.QueryAsync<Noticia>(@"SELECT TOP 10 * FROM tblNoticia ORDER BY DataCadastro DESC");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    throw new Exception("Seleciona10NoticiaRepo1: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Seleciona10NoticiaRepo2: " + ex.Message);
            }
        }
    }
}
