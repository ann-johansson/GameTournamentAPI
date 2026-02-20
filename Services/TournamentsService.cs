using GameTournamentAPI.Data;
using GameTournamentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTournamentAPI.Services
{
    public class TournamentsService
    {
        private readonly AppDbContext _context;
        public TournamentsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(string? search)
        {
            var query = _context.Tournaments.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            return await query.ToListAsync();
        }
    }
}
