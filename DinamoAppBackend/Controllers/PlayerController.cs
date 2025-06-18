using DinamoAppBackend.Models;
using DinamoAppBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinamoAppBackend.Controllers
{
    [Authorize] // toate rutele din acest controller necesită JWT
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAll() =>
            await _playerService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetById(string id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            await _playerService.CreateAsync(player);
            return CreatedAtAction(nameof(GetById), new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Player updated)
        {
            var existing = await _playerService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            updated.Id = existing.Id;
            await _playerService.UpdateAsync(id, updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _playerService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _playerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
