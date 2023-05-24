

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApiLBCA.Domain;
using WebApiLBCA.Dominio;

namespace WebApiLBCA.Infraestrutura.Contexto
{
    public class WebApiLBCAContexto : DbContext
    {
        public DbSet<Noticia> tblNoticia { get; set; }
        public DbSet<Usuario> tblUsuario { get; set; }
        public DbSet<Classificacao> tblClassificacao { get; set; }

        static string connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public WebApiLBCAContexto(IConfiguration configuration)
        {
            connectionString = configuration
                     .GetConnectionString("DefaultConnection").ToString();

        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
