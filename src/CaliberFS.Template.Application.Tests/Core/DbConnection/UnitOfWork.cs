using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Data.Repositories;
using CaliberFS.Template.Domain.Interfaces.Repositories;

namespace CaliberFS.Template.Application.Tests.Core.DbConnection
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbSession _session;

        public IBoardRepository BoardRepository { get; internal set; }

        public UnitOfWork(IDbSession session)
        {
            _session = session;
            BoardRepository = new BoardRepository(_session);
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session?.Transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session?.Transaction?.Rollback();
        }

        public void Dispose() => _session.Dispose();
    }
}
