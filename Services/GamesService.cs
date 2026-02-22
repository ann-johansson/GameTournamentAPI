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

        public async Task<Game> CreateAsync(Game game)
        {

            _context.Games.Add(game);

            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Game?> UpdateAsync(int id, Game updatedData)
        {
            var existing = await _context.Games.FindAsync(id);
            if (existing == null) return null;

            existing.Title = updatedData.Title;
            existing.Time = updatedData.Time;
            existing.TournamentId = updatedData.TournamentId;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
