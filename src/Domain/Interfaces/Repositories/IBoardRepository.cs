using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IBoardRepository : IBaseRepository<Board>
    {
        /// <summary>
        /// Counts number of boards by name
        /// </summary>
        Task<int> CountByNameAsync(string name);

        // gets boards by name
        Task<List<Board>> GetByNameAsync(string name);
    }
}
