namespace GameTournamentAPI.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int MaxPlayers { get; set; }
        public DateTime Date { get; set; }
    }
}
