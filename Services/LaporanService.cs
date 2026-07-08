using Microsoft.EntityFrameworkCore;
using SistemRetribusiParkir.Data;
using SistemRetribusiParkir.Models;
using SistemRetribusiParkir.Models.ViewModels;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;


namespace SistemRetribusiParkir.Services
{
    public class LaporanService
    {
        private readonly ApplicationDbContext _context;

        public LaporanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LaporanViewModel> GetLaporanAsync(
            DateTime? awal,
            DateTime? akhir)
        {
            var query = _context.Retribusi
                .Include(x => x.LokasiParkir)
                .Include(x => x.JenisKendaraan)
                .Include(x => x.Petugas)
                .AsQueryable();

            if (awal.HasValue)
            {
                query = query.Where(x => x.Tanggal >= awal.Value);
            }

            if (akhir.HasValue)
            {
                query = query.Where(x => x.Tanggal <= akhir.Value);
            }

            var data = await query
                .OrderByDescending(x => x.Tanggal)
                .ToListAsync();

            return new LaporanViewModel
            {
                TanggalAwal = awal,
                TanggalAkhir = akhir,
                Data = data,
                TotalPendapatan = data.Sum(x => x.JumlahBayar)
            };
        }
        public byte[] ExportPdf(List<Retribusi> data)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("LAPORAN RETRIBUSI PARKIR")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Tanggal").Bold();
                            header.Cell().Text("No Polisi").Bold();
                            header.Cell().Text("Lokasi").Bold();
                            header.Cell().Text("Jenis").Bold();
                            header.Cell().AlignRight().Text("Bayar").Bold();
                        });

                        foreach (var item in data)
                        {
                            table.Cell().Text(item.Tanggal.ToString("dd/MM/yyyy"));

                            table.Cell().Text(item.NomorKendaraan);

                            table.Cell().Text(item.LokasiParkir?.NamaLokasi ?? "");

                            table.Cell().Text(item.JenisKendaraan?.NamaJenis ?? "");

                            table.Cell().AlignRight()
                                .Text($"Rp {item.JumlahBayar:N0}");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Halaman ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf();
        }
        public byte[] ExportExcel(List<Retribusi> data)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Laporan Retribusi");

            worksheet.Cell(1, 1).Value = "LAPORAN RETRIBUSI PARKIR";
            worksheet.Range(1, 1, 1, 7).Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(3, 1).Value = "Tanggal";
            worksheet.Cell(3, 2).Value = "No Polisi";
            worksheet.Cell(3, 3).Value = "Lokasi";
            worksheet.Cell(3, 4).Value = "Jenis Kendaraan";
            worksheet.Cell(3, 5).Value = "Petugas";
            worksheet.Cell(3, 6).Value = "Tarif";
            worksheet.Cell(3, 7).Value = "Jumlah Bayar";

            worksheet.Range(3, 1, 3, 7).Style.Font.Bold = true;
            worksheet.Range(3, 1, 3, 7).Style.Fill.BackgroundColor = XLColor.LightGray;

            int row = 4;

            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.Tanggal;
                worksheet.Cell(row, 1).Style.DateFormat.Format = "dd/MM/yyyy";

                worksheet.Cell(row, 2).Value = item.NomorKendaraan;
                worksheet.Cell(row, 3).Value = item.LokasiParkir?.NamaLokasi;
                worksheet.Cell(row, 4).Value = item.JenisKendaraan?.NamaJenis;
                worksheet.Cell(row, 5).Value = item.Petugas?.NamaPetugas;

                worksheet.Cell(row, 6).Value = item.Tarif;
                worksheet.Cell(row, 7).Value = item.JumlahBayar;

                worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0";
                worksheet.Cell(row, 7).Style.NumberFormat.Format = "#,##0";

                row++;
            }

            worksheet.Cell(row + 1, 6).Value = "TOTAL";
            worksheet.Cell(row + 1, 6).Style.Font.Bold = true;

            worksheet.Cell(row + 1, 7).Value = data.Sum(x => x.JumlahBayar);
            worksheet.Cell(row + 1, 7).Style.Font.Bold = true;
            worksheet.Cell(row + 1, 7).Style.NumberFormat.Format = "#,##0";

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}