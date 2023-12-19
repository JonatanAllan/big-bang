namespace CaliberFS.Template.Application.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Starts a new database transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks the current transaction.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Gets an <see cref="IBoardRepository"/> instance.
        /// </summary>
        public IBoardRepository BoardRepository { get; }

    }
}
