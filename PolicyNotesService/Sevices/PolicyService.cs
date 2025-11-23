using PolicyNotesService.Model;
using PolicyNotesService.Repository;

namespace PolicyNotesService.Sevices
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repo;

        public PolicyService(IPolicyRepository repo)
        {
            _repo = repo;
        }

        public Task<Policy> AddPolicyNoteAsync(string policyNumber, string note)
        {
            var policy = new Policy
            {
                PolicyNumber = policyNumber,
                Note = note
            };

            return _repo.AddAsync(policy);
        }

        public Task<List<Policy>> GetAllNotesAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<Policy?> GetNoteByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public Task<Policy?> UpdatePolicyAsync(int id, string policyNumber, string note)
        {
            return _repo.UpdateAsync(id, policyNumber, note);
        }

        public Task<bool> DeletePolicyAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }
    }
}