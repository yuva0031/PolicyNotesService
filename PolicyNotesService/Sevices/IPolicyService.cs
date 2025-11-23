using PolicyNotesService.Model;

namespace PolicyNotesService.Sevices
{
    public interface IPolicyService
    {
        Task<Policy> AddPolicyNoteAsync(string policyNumber, string note);
        Task<List<Policy>> GetAllNotesAsync();
        Task<Policy?> GetNoteByIdAsync(int id);
        Task<Policy?> UpdatePolicyAsync(int id, string policyNumber, string note);
        Task<bool> DeletePolicyAsync(int id);
    }
}