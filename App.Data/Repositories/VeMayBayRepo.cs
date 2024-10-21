using App.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public class VeMayBayRepo : IVeMayBayRepo
    {
        private readonly AppDbContext _context;
        public VeMayBayRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VeMayBay veMayBay)
        {
            await _context.VeMayBays.AddAsync(veMayBay);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var mayBay =  await _context.VeMayBays.FindAsync(id);
            if(mayBay != null)
            {
                _context.VeMayBays.Remove(mayBay);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<IEnumerable<VeMayBay>> GetAllAsync()
        {
            return await _context.VeMayBays.ToListAsync();  
        }

        public async Task<VeMayBay> GetByIdAsync(Guid id)
        {
            return await _context.VeMayBays.FindAsync(id);
        }

        public Task UpdateAsync(VeMayBay veMayBay)
        {
            _context.VeMayBays.Update(veMayBay);
            return _context.SaveChangesAsync();
        }
    }
}
