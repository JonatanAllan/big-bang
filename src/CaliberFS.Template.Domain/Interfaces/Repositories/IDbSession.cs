using System.Data;

namespace Domain.Interfaces.Repositories
{
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction? Transaction { get; set; }
    }
}
