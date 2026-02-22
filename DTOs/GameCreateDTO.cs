using GameTournamentAPI.Validation;
using System.ComponentModel.DataAnnotations;

namespace GameTournamentAPI.DTOs
{
    public class GameCreateDTO
    {
        [Required(ErrorMessage = "Title needs to be filled in")]
        [MinLength(3, ErrorMessage = "Title needs to be more than 3 characters long")]
        public string Title { get; set; } = "";

        [FutureDate]
        public DateTime Time { get; set; }


    }
}
