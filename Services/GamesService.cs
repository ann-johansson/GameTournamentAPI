using GameTournamentAPI.Data;
using GameTournamentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTournamentAPI.Services
{
    public class GamesService
    {
        private readonly AppDbContext _context;
        public GamesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(string? search)
        {
            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);

        }
            

    }
}
