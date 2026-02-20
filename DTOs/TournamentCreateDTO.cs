namespace GameTournamentAPI.DTOs
{
    public class TournamentCreateDTO
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int MaxPlayers { get; set; }
        public DateTime Date { get; set; }
    }
}
