using GameTournamentAPI.Models;

namespace GameTournamentAPI.Services
{
    public class TournamentsService
    {
        private readonly List<Tournament> _tournaments = new List<Tournament>();
        private int _nextId = 1;

    }
}
