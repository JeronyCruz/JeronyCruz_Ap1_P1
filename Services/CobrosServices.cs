using JeronyCruz_Ap1_P1.DAL;
using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JeronyCruz_Ap1_P1.Services
{
    public class CobrosServices(Context context)
    {
        private readonly Context _context = context;


        public async Task<bool> Existe(int id)
        {
            return await _context.Cobros.AnyAsync(a => a.CobroId == id);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            _context.Cobros.Add(cobros);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cobros cobros)
        {
            var existingCobros = await _context.Cobros
                .Include(c => c.CobroDetalles)
                .FirstOrDefaultAsync(c => c.CobroId == cobros.CobroId);

            if (existingCobros != null)
            {
                
                _context.Entry(existingCobros).CurrentValues.SetValues(cobros);

               
                var detallesToRemove = existingCobros.CobroDetalles
                    .Where(d => !cobros.CobroDetalles.Any(cd => cd.DetalleId == d.DetalleId))
                    .ToList();

                foreach (var detalle in detallesToRemove)
                {
                    _context.CobroDetalle.Remove(detalle);
                }

                
                foreach (var nuevoDetalle in cobros.CobroDetalles)
                {
                    if (nuevoDetalle.DetalleId > 0)
                    {
                        var existingDetalle = existingCobros.CobroDetalles
                            .FirstOrDefault(d => d.DetalleId == nuevoDetalle.DetalleId);

                        if (existingDetalle != null)
                        {
                            
                            _context.Entry(existingDetalle).CurrentValues.SetValues(nuevoDetalle);
                        }
                    }
                    else 
                    {
                        existingCobros.CobroDetalles.Add(nuevoDetalle);
                    }
                }

                return await _context.SaveChangesAsync() > 0;
            }

            return false; 
        }


        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobroId))
                return await Insertar(cobros);
            else
                return await Modificar(cobros);
        }

        public async Task<bool> Eliminar(int id)
        {
            var Cobros = await _context.Cobros.FindAsync(id);
            if (Cobros != null)
            {
                _context.Cobros.Remove(Cobros);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Cobros> Buscar(int id)
        {
            return await _context.Cobros.Include(d => d.Deudores).Include(d => d.CobroDetalles).AsNoTracking().FirstOrDefaultAsync(a => a.CobroId == id);
        }

        public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
        {
            return await _context.Cobros.Include(d => d.Deudores).Include(d => d.CobroDetalles).AsNoTracking().Where(criterio).ToListAsync();
        }

        public async Task<Cobros> ObtenerPorId(int id)
        {
            return await _context.Cobros
                .Include(c => c.CobroDetalles) 
                .FirstOrDefaultAsync(c => c.CobroId == id);
        }

    }
}
