using GameTournamentAPI.DTOs;
using GameTournamentAPI.Models;
using GameTournamentAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameTournamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesService _service;

        public GamesController(GamesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResponseDTO>>> GetAll([FromQuery] string? search)
        {
            var games = await _service.GetAllAsync(search);

            var response = games.Select(g => new GameResponseDTO
            {
                Id = g.Id,
                Title = g.Title,
                Time = g.Time,
                TournamentId = g.TournamentId
            });

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameResponseDTO>> GetById(int id)
        {
            var game = await _service.GetByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            var responseDto = new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
            };

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponseDTO>> Create(GameCreateDTO createDto)
        {

            var game = new Game
            {
                Title = createDto.Title,
                Time = createDto.Time,
                TournamentId = createDto.TournamentId
            };


            var success = await _service.CreateAsync(game);

            if (!success)
            {
                return BadRequest($"Tournament with ID {createDto.TournamentId} does not exist.");
            }

            var responseDto = new GameResponseDTO
            {
                Id = game.Id,
                Title = game.Title,
                Time = game.Time,
                TournamentId = game.TournamentId
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
        public async Task<IActionResult> Update(int id, GameUpdateDTO updateDto)
        {

            var gameModel = new Game
            {
                Title = updateDto.Title,
                Time = updateDto.Time,
                TournamentId = updateDto.TournamentId
            };

            var result = await _service.UpdateAsync(id, gameModel);

            if (result == null) return NotFound();

            return NoContent();
        }

    }
}
