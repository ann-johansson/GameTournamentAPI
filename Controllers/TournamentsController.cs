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


        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentResponseDTO>> GetById(int id)
        {
            var tournament = await _service.GetByIdAsync(id);

            if (tournament == null)
            {
                return NotFound(); 
            }

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
            
            var tournament = new Tournament
            {
                Title = createDto.Title,
                Description = createDto.Description,
                MaxPlayers = createDto.MaxPlayers,
                Date = createDto.Date
            };

           
            await _service.CreateAsync(tournament);

            
            var responseDto = new TournamentResponseDTO
            {
                Id = tournament.Id,
                Title = tournament.Title,
                Description = tournament.Description,
                Date = tournament.Date
            };

            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TournamentUpdateDTO updateDto)
        {
            
            var tournamentModel = new Tournament
            {
                Title = updateDto.Title,
                Description = updateDto.Description,
                MaxPlayers = updateDto.MaxPlayers,
                Date = updateDto.Date
            };

            var result = await _service.UpdateAsync(id, tournamentModel);

            if (result == null) return NotFound();

            return NoContent(); 
        }
    }
}