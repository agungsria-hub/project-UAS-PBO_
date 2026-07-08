using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Services
{
    public class PetugasService
    {
        private readonly ApplicationDbContext _context;

        public PetugasService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Petugas>> GetAllAsync()
        {
            return await _context.Petugas
                .OrderBy(x => x.NamaPetugas)
                .ToListAsync();
        }

        public async Task<Petugas?> GetByIdAsync(int id)
        {
            return await _context.Petugas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Petugas petugas)
        {
            _context.Petugas.Add(petugas);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Petugas petugas)
        {
            _context.Petugas.Update(petugas);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.Petugas.FindAsync(id);

            if (data != null)
            {
                _context.Petugas.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Petugas>> SearchAsync(string keyword)
        {
            return await _context.Petugas
                .Where(x => x.NamaPetugas.Contains(keyword))
                .OrderBy(x => x.NamaPetugas)
                .ToListAsync();
        }
    }
}