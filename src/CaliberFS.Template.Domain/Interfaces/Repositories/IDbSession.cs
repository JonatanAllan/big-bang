using System.Data;

namespace CaliberFS.Template.Domain.Interfaces.Repositories
{
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction? Transaction { get; set; }
    }
}
