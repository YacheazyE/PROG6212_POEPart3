using cmcs_api.Models;

namespace cmcs_api.Services
{
    public interface IClaimsService
    {
        Task<IEnumerable<Claims>> GetAllClaimsAsync();
        Task<Claims?> GetClaimByIdAsync(int id);
        Task<Claims> CreateClaimAsync(Claims newClaim);
        Task UpdateClaimAsync(int id, Claims updatedClaim);
        Task DeleteClaimAsync(int id);
    }
}
