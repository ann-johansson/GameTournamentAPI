using Azure;
using GameTournamentAPI.DTOs;
using GameTournamentAPI.Models;
using GameTournamentAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTournamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {

        private readonly TournamentsService _service;

        public TournamentsController(TournamentsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentResponseDTO>>> GetAll([FromQuery] string? search)
        {
            var tournaments = await _service.GetAllAsync(search);

            var response = tournaments.Select(t => new TournamentResponseDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Date = t.Date
            });

            return Ok(response);
        }


        [HttpGet("{id}")] // Detta gör att URL:en blir api/tournaments/5
        public async Task<ActionResult<TournamentResponseDTO>> GetById(int id)
        {
            var tournament = await _service.GetByIdAsync(id);

            if (tournament == null)
            {
                return NotFound(); // Returnerar 404 om ID:t inte finns
            }

            // Mappa till DTO
            var responseDto = new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                Date = tournament.Date
            };

            return Ok(responseDto);
        }


        [HttpPost]
        public async Task<ActionResult<TournamentResponseDTO>> Create(TournamentCreateDTO createDto)
        {
            // Mappa DTO -> Model
            var tournament = new Tournament
            {
                Title = createDto.Title,
                Description = createDto.Description,
                MaxPlayers = createDto.MaxPlayers,
                Date = createDto.Date
            };

            // Skicka till Service
            var createdTournament = await _service.CreateAsync(tournament);

            // Mappa Model -> ResponseDTO
            var responseDto = new TournamentResponseDTO
            {
                Id = createdTournament.Id, // Nu har den fått ett ID från databasen!
                Title = createdTournament.Title,
                Description = createdTournament.Description,
                Date = createdTournament.Date
            };

            // Returnera 201 Created med en länk till den nya resursen (GetById)
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}