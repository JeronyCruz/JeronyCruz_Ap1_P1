using JeronyCruz_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace JeronyCruz_Ap1_P1.DAL
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public DbSet<Prestamos> Registros { get; set; }

    }
}
