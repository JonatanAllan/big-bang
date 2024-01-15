using Enterprise.Template.Domain.Entities;

namespace Enterprise.Template.Domain.Interfaces.Repositories
{
    public interface IBoardRepository
    {
        Task AddAsync(Board entity);

        Task<bool> ExistsAsync(string name);

        Task<IEnumerable<Board>> GetManyAsync(string name, int skip, int take);

        Task<int> CountAsync(string name);
    }
}
