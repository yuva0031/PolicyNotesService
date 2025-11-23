using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Model;

namespace PolicyNotesService.Repository
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly PolicyDbContext _context;

        public PolicyRepository(PolicyDbContext context)
        {
            _context = context;
        }

        public async Task<Policy> AddAsync(Policy policy)
        {
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        public Task<List<Policy>> GetAllAsync()
        {
            return _context.Policies.ToListAsync();
        }

        public Task<Policy?> GetByIdAsync(int id)
        {
            return _context.Policies.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Policy?> UpdateAsync(int id, string policyNumber, string note)
        {
            var existing = await _context.Policies.FindAsync(id);
            if (existing is null) return null;

            existing.PolicyNumber = policyNumber;
            existing.Note = note;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Policies.FindAsync(id);
            if (existing is null) return false;

            _context.Policies.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}