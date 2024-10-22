using JeronyCruz_Ap1_P1.DAL;
using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq.Expressions;

namespace JeronyCruz_Ap1_P1.Services
{
    public class CobrosServices(Context context)
    {
        private readonly Context _context = context;


        public async Task<bool> Existe(int id)
        {
            return await _context.Cobros
                .AnyAsync(a => a.CobroId == id);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            context.Cobros.Add(cobros);
            await AfectarPrestamos(cobros.CobroDetalles.ToArray(), TipoOperacion.Resta);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cobros cobros)
        {
            context.Update(cobros);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task AfectarPrestamos(CobroDetalle[] detalle, TipoOperacion tipoOperacion)
        {
            foreach (var item in detalle)
            {
                var prestamo = await context.Prestamos.SingleAsync(p => p.PrestamoId == item.PrestamoId);
                if (tipoOperacion == TipoOperacion.Resta)
                    prestamo.Balance -= item.ValorCobrado;
                else
                    prestamo.Balance += item.ValorCobrado;

            }
        }

        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobroId))
                return await Insertar(cobros);
            else
                return await Modificar(cobros);
        }

        public async Task<bool> Eliminar(int cobroId)
        {
            
            var cobro = await context.Cobros
                .Include(c => c.CobroDetalles)
                .FirstOrDefaultAsync(c => c.CobroId == cobroId);

            if (cobro == null) return false;

            await AfectarPrestamos(cobro.CobroDetalles.ToArray(), TipoOperacion.Suma);

            context.CobroDetalle.RemoveRange(cobro.CobroDetalles);
            context.Cobros.Remove(cobro);
            var cantidad = await context.SaveChangesAsync();
            return cantidad > 0;
        }

        public async Task<Cobros> Buscar(int id)
        {
            return await _context.Cobros
                .Include(d => d.Deudores)
                .Include(d => d.CobroDetalles)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CobroId == id);
        }

        public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
        {
            return await _context.Cobros
                .Include(d => d.Deudores)
                .Include(d => d.CobroDetalles)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Cobros> ObtenerPorId(int id)
        {
            return await _context.Cobros
                .Include(c => c.CobroDetalles) 
                .FirstOrDefaultAsync(c => c.CobroId == id);
        }

    }

    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }
}
