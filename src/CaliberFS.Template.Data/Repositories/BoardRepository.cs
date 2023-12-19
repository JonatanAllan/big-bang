using CaliberFS.Template.Data.Context;
using CaliberFS.Template.Domain.Entities;
using CaliberFS.Template.Domain.Interfaces.Repositories;

namespace CaliberFS.Template.Data.Repositories
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
