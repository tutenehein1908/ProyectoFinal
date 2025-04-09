using Licencia___PF.Model;
using Microsoft.EntityFrameworkCore;

namespace Licencia___PF
{
    public class LicenciaContext : DbContext
    {
        public DbSet<Licencia> Licencias { get; set; }

        public LicenciaContext(DbContextOptions<LicenciaContext> options) : base(options)
        {

        }

    }
}

