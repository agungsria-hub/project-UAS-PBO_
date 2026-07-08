using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Services
{
    public class LokasiService
    {
        private readonly ApplicationDbContext _context;

        public LokasiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LokasiParkir>> GetAllAsync()
        {
            return await _context.LokasiParkir
                .OrderBy(x => x.NamaLokasi)
                .ToListAsync();
        }

        public async Task<LokasiParkir?> GetByIdAsync(int id)
        {
            return await _context.LokasiParkir
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(LokasiParkir lokasi)
        {
            _context.LokasiParkir.Add(lokasi);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LokasiParkir lokasi)
        {
            _context.LokasiParkir.Update(lokasi);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lokasi = await _context.LokasiParkir.FindAsync(id);

            if (lokasi != null)
            {
                _context.LokasiParkir.Remove(lokasi);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LokasiParkir>> SearchAsync(string keyword)
        {
            return await _context.LokasiParkir
                .Where(x => x.NamaLokasi.Contains(keyword))
                .ToListAsync();
        }
    }
}