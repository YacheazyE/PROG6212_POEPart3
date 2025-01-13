using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cmcs_api.Models; 
using cmcs_api.Services; 

namespace cmcs_api.Controllers
{
    [Authorize] // Ensures all endpoints require authorization
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService _claimsService;

        public ClaimsController(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        // GET: api/Claims
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimsService.GetAllClaimsAsync();
            return Ok(claims);
        }

        // GET: api/Claims/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _claimsService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        // POST: api/Claims
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody] Claims newClaim)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the default status is false if not provided
            newClaim.Status ??= false;

            var createdClaim = await _claimsService.CreateClaimAsync(newClaim);
            return CreatedAtAction(nameof(GetClaimById), new { id = createdClaim.ClaimID }, createdClaim);
        }

        // PUT: api/Claims/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(int id, [FromBody] Claims updatedClaim)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingClaim = await _claimsService.GetClaimByIdAsync(id);
            if (existingClaim == null)
                return NotFound();

            await _claimsService.UpdateClaimAsync(id, updatedClaim);
            return NoContent();
        }

        // DELETE: api/Claims/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var existingClaim = await _claimsService.GetClaimByIdAsync(id);
            if (existingClaim == null)
                return NotFound();

            await _claimsService.DeleteClaimAsync(id);
            return NoContent();
        }
    }
}
