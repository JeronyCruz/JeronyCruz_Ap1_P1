using JeronyCruz_Ap1_P1.DAL;
using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JeronyCruz_Ap1_P1.Services
{
    public class PrestamosServices(Context context)
    {
        private readonly Context _context = context;


        public async Task<bool> Existe(int id)
        {
           return await _context.Prestamos
                .AnyAsync(a => a.PrestamoId == id);
        }

        private async Task<bool> Insertar(Prestamos prestamos)
        {
            _context.Prestamos.Add(prestamos);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prestamos prestamos)
        {
            _context.Update(prestamos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Prestamos prestamos)
        {
            prestamos.Balance = prestamos.Monto;
            if (!await Existe(prestamos.PrestamoId))
                return await Insertar(prestamos);
            else
                return await Modificar(prestamos);
        }

        public async Task<Prestamos> Buscar(int prestamoId)
        {
            return await _context.Prestamos.Include(d => d.Deudor)
                .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
        }

        public async Task<bool> Eliminar(int prestamoId)
        {
            return await _context.Prestamos
                .Where(p => p.PrestamoId == prestamoId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos
                .Include(d => d.Deudor)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Prestamos?> BuscarPrestamo(int id)
        {
            return await _context.Prestamos
                .Include(p => p.Deudor)
                .FirstOrDefaultAsync(p => p.PrestamoId == id);
        }

        public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos
                .Include(d => d.Deudor)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Prestamos>> ObtenerPrestamosPorDeudor(int deudorId)
        {
            
            return await _context.Prestamos
                                 .Where(p => p.DeudorId == deudorId)
                                 .ToListAsync();
        }

        public async Task<Prestamos> GetCliente(int deudorId)
        {
            return await _context.Prestamos
                .FirstOrDefaultAsync(p => p.DeudorId == deudorId);
        }

        public async Task<Prestamos> ObtenerPorId(int id)
        {
            return await _context.Prestamos
                .FirstOrDefaultAsync(p => p.PrestamoId == id);
        }

        public async Task<List<Prestamos>> GetPrestamosPendientes(int deudorId)
        {
            return await _context.Prestamos
                .Where(p => p.DeudorId == deudorId && p.Balance > 0)
                .OrderBy(p => p.PrestamoId)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
