using CaliberFS.Template.Domain.Entities;

namespace CaliberFS.Template.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        Task<bool> AddAsync(TEntity entity);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        Task<bool> DeleteById(int id);
    }
}
