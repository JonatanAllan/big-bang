using Enterprise.Template.Data.Context;
using Enterprise.Template.Domain.Entities;
using Enterprise.Template.Domain.Interfaces.Repositories;

namespace Enterprise.Template.Data.Repositories
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        private readonly AppDbContext _context;
        public BoardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
