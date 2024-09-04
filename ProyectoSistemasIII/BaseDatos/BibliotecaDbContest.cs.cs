using Microsoft.EntityFrameworkCore;
using ProyectoSistemasIII.Models;
namespace ProyectoSistemasIII.BaseDatos
{
    public class BibliotecaDbContest : DbContext
    {

        public BibliotecaDbContest(DbContextOptions options) : base(options) { }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }

    }
}
