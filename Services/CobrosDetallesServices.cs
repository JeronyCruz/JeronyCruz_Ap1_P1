using JeronyCruz_Ap1_P1.DAL;
using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JeronyCruz_Ap1_P1.Services
{
    public class CobrosDetallesServices(Context context)
    {
        private readonly Context _context = context;

        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos
                .AsNoTracking().Where(criterio).ToListAsync();
        }
    }
}
