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

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            
            _context.Tournaments.Add(tournament);

            await _context.SaveChangesAsync();

            return tournament;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null) return false;

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
