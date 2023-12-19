using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;

namespace CaliberFS.Template.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbSession _session;

        public IBoardRepository BoardRepository { get; internal set; }

        public UnitOfWork(IDbSession session, IBoardRepository boardRepository)
        {
            _session = session;
            BoardRepository = boardRepository;
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
