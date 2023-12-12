namespace Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        int SaveChanges();
        void Commit();
        void Rollback();
    }
}
