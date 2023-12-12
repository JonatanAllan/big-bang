using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
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
