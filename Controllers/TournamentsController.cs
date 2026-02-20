using Azure;
using GameTournamentAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameTournamentAPI.Services;

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
    }

}
