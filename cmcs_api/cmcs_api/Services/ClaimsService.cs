using cmcs_api.Models;
using cmcs_api;
using Microsoft.EntityFrameworkCore;

namespace cmcs_api.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly ApplicationDbContext _dbContext;

        public ClaimsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Claims>> GetAllClaimsAsync()
        {
            return await _dbContext.tblClaims.ToListAsync(); // Use tblClaims
        }

        public async Task<Claims?> GetClaimByIdAsync(int id)
        {
            return await _dbContext.tblClaims.FindAsync(id); // Use tblClaims
        }

        public async Task<Claims> CreateClaimAsync(Claims newClaim)
        {
            newClaim.Status ??= false; // Ensure default status is false
            _dbContext.tblClaims.Add(newClaim); // Use tblClaims
            await _dbContext.SaveChangesAsync();
            return newClaim;
        }

        public async Task UpdateClaimAsync(int id, Claims updatedClaim)
        {
            var existingClaim = await _dbContext.tblClaims.FindAsync(id); // Use tblClaims
            if (existingClaim != null)
            {
                existingClaim.SubmittedDate = updatedClaim.SubmittedDate;
                existingClaim.HourlyRate = updatedClaim.HourlyRate;
                existingClaim.SubmittedDocxPath = updatedClaim.SubmittedDocxPath;
                existingClaim.Status = updatedClaim.Status;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteClaimAsync(int id)
        {
            var claim = await _dbContext.tblClaims.FindAsync(id); // Use tblClaims
            if (claim != null)
            {
                _dbContext.tblClaims.Remove(claim); // Use tblClaims
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
