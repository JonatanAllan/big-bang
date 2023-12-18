using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public sealed class BoardRepository : IBoardRepository
    {
        private IDbSession _dbSession;

        public BoardRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<bool> AddAsync(Board entity)
        {
            var result = await _dbSession.Connection.ExecuteAsync("INSERT INTO Boards (Id, Name, Description) VALUES (@Id, @Name, @Description)", entity, _dbSession.Transaction);
            return result > 0;
        }

        public async Task<int> CountByNameAsync(string name)
        {
            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Boards WHERE Name like @Name", new { Name = "%" + name + "%" });
            return result;
        }

        public async Task<bool> DeleteById(int id)
        {
            var result = await _dbSession.Connection.ExecuteAsync("DELETE FROM Boards WHERE Id = @Id", new { Id = id }, _dbSession.Transaction);
            return result > 0;
        }

        public async Task<Board?> GetByIdAsync(int id)
        {
            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Board>("SELECT * FROM Boards WHERE Id = @Id", new { Id = id });
            return result;
        }

        public async Task<List<Board>> GetByNameAsync(string name)
        {
            var result = await _dbSession.Connection.QueryAsync<Board>("SELECT * FROM Boards WHERE Name like @Name", new { Name = "%" + name + "%" });
            return result.ToList();
        }

        public async Task<bool> UpdateAsync(Board entity)
        {
            var result = await _dbSession.Connection.ExecuteAsync("UPDATE Boards SET Name = @Name, Description = @Description WHERE Id = @Id", entity, _dbSession.Transaction);
            return result > 0;
        }
    }
}
