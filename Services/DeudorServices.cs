using JeronyCruz_Ap1_P1.DAL;
using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JeronyCruz_Ap1_P1.Services
{
    public class DeudorServices(Context context)
    {
        private readonly Context _context = context;

        public async Task<List<Deudores>> Listar(Expression<Func<Deudores, bool>> criterio)
        {
            return await _context.Deudores.AsNoTracking().Where(criterio).ToListAsync();
        }
    }
}
