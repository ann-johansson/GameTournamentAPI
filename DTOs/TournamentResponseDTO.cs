namespace GameTournamentAPI.DTOs
{
    public class TournamentResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Date { get; set; }
    }
}
