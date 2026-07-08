using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models;

namespace SistemRetribusiParkir.Services
{
    public class RetribusiService
    {
        private readonly ApplicationDbContext _context;

        public RetribusiService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================
        // CRUD
        // ==========================

        public async Task<List<Retribusi>> GetAllAsync()
        {
            return await _context.Retribusi
                .Include(x => x.LokasiParkir)
                .Include(x => x.JenisKendaraan)
                .Include(x => x.Petugas)
                .OrderByDescending(x => x.Tanggal)
                .ToListAsync();
        }

        public async Task<Retribusi?> GetByIdAsync(int id)
        {
            return await _context.Retribusi
                .Include(x => x.LokasiParkir)
                .Include(x => x.JenisKendaraan)
                .Include(x => x.Petugas)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Retribusi data)
        {
            _context.Retribusi.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Retribusi data)
        {
            _context.Retribusi.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.Retribusi.FindAsync(id);

            if (data != null)
            {
                _context.Retribusi.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        // ==========================
        // Dropdown
        // ==========================

        public async Task<IEnumerable<SelectListItem>> GetLokasiAsync()
        {
            return await _context.LokasiParkir
                .Where(x => x.Status)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NamaLokasi
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetJenisKendaraanAsync()
        {
            return await _context.JenisKendaraan
                .Where(x => x.Status)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NamaJenis
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetPetugasAsync()
        {
            return await _context.Petugas
                .Where(x => x.Status)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NamaPetugas
                })
                .ToListAsync();
        }

        // ==========================
        // AJAX Tarif
        // ==========================

        public async Task<decimal> GetTarifAsync(int id)
        {
            var data = await _context.JenisKendaraan.FindAsync(id);

            if (data == null)
                return 0;

            return data.Tarif;
        }
    }
}