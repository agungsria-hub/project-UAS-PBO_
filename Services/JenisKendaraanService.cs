using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Services
{
    public class JenisKendaraanService
    {
        private readonly ApplicationDbContext _context;

        public JenisKendaraanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JenisKendaraan>> GetAllAsync()
        {
            return await _context.JenisKendaraan
                .OrderBy(x => x.NamaJenis)
                .ToListAsync();
        }

        public async Task<JenisKendaraan?> GetByIdAsync(int id)
        {
            return await _context.JenisKendaraan
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(JenisKendaraan jenis)
        {
            _context.JenisKendaraan.Add(jenis);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JenisKendaraan jenis)
        {
            _context.JenisKendaraan.Update(jenis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.JenisKendaraan.FindAsync(id);

            if (data != null)
            {
                _context.JenisKendaraan.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<JenisKendaraan>> SearchAsync(string keyword)
        {
            return await _context.JenisKendaraan
                .Where(x => x.NamaJenis.Contains(keyword))
                .OrderBy(x => x.NamaJenis)
                .ToListAsync();
        }
    }
}