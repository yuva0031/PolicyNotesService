using PolicyNotesService.Model;

namespace PolicyNotesService.Repository
{
    public interface IPolicyRepository
    {
        Task<Policy> AddAsync(Policy policy);
        Task<List<Policy>> GetAllAsync();
        Task<Policy?> GetByIdAsync(int id);
        Task<Policy?> UpdateAsync(int id, string policyNumber, string note);
        Task<bool> DeleteAsync(int id);
    }
}