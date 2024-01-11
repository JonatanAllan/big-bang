namespace Enterprise.Template.Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        int SaveChanges();
        void Commit();
        void Rollback();
    }
}
