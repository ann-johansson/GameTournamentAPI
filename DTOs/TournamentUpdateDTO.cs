namespace GameTournamentAPI.DTOs
{
    public class TournamentUpdateDTO
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int MaxPlayers { get; set; }
        public DateTime Date { get; set; }
    }
}
