
using Aplikacja_Desktopowa2.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace Aplikacja_Desktopowa2.Data
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext()
            : base("name=WarehouseContext")
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
    }
}
