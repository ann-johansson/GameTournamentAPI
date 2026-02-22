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
            // FindAsync är super-effektiv för att leta efter just ID (Primary Key)
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            // 1. Lägg till i EF Cores "lista" (minnet)
            _context.Tournaments.Add(tournament);

            // 2. Spara ner till SQL Server på riktigt
            // Det är här ID:t skapas av databasen!
            await _context.SaveChangesAsync();

            return tournament;
        }
    }
}
