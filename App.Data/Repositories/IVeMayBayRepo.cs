using App.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Repositories
{
    public interface IVeMayBayRepo
    {
        Task<IEnumerable<VeMayBay>> GetAllAsync();
        Task<VeMayBay> GetByIdAsync(Guid id);
        Task AddAsync (VeMayBay veMayBay);  
        Task UpdateAsync (VeMayBay veMayBay);
        Task DeleteAsync (Guid id);
    }
}
