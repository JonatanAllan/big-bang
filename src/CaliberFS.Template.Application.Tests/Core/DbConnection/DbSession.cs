using CaliberFS.Template.Domain.Interfaces.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CaliberFS.Template.Application.Tests.Core.DbConnection
{
    public class DbSession : IDbSession
    {
        public IDbConnection Connection { get; }

        public IDbTransaction? Transaction { get; set; }

        private bool _disposed;

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }

        public void Dispose() => Dispose(true);

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Transaction?.Dispose();
                    Connection?.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
