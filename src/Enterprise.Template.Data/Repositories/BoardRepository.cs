using Enterprise.GenericRepository.Interfaces;
using Enterprise.Template.Domain.Entities;
using Enterprise.Template.Domain.Interfaces.Repositories;

namespace Enterprise.Template.Data.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IGenericRepository _genericRepository;

        public BoardRepository(IGenericRepositoryFactory genericRepositoryFactory)
        {
            _genericRepository = genericRepositoryFactory.GetRepository(ConnectionStrings.TemplateApp);
        }

        public async Task AddAsync(Board entity)
        {
            var spName = "usp_AddNewBoard";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Name", entity.Name),
                new KeyValuePair<string, object>("Description", entity.Description),
                new KeyValuePair<string, object>("CreatedAt", entity.CreatedAt),
                new KeyValuePair<string, object>("LastUpdatedAt", entity.LastUpdatedAt)
            };

            var result = await _genericRepository.ExecuteAsync(spName, parameters);

            if (result.Failed)
                throw new InvalidOperationException(result.Message);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var query = "select Count(*) from Board where UPPER([Name]) = @Name";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Name", name)
            };

            var result = await _genericRepository.GetSingleAsync<int>(query, System.Data.CommandType.Text, parameters);

            if (result.Failed)
                throw new InvalidOperationException(result.Message);

            return result.Result > 0;
        }

        public async Task<IEnumerable<Board>> GetManyAsync(string name, int skip, int take)
        {
            var query = "select * from Board where UPPER([Name]) = @Name";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Name", name)
            };

            var result = await _genericRepository.GetAllAsync<Board>(query, System.Data.CommandType.Text, parameters);

            if (result.Failed)
                throw new InvalidOperationException(result.Message);

            return result.Result.Skip(skip).Take(take);
        }

        public async Task<int> CountAsync(string name)
        {
            var query = "select Count(*) from Board where UPPER([Name]) = @Name";
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Name", name)
            };

            var result = await _genericRepository.GetSingleAsync<int>(query, System.Data.CommandType.Text, parameters);

            if (result.Failed)
                throw new InvalidOperationException(result.Message);

            return result.Result;
        }
    }
}
