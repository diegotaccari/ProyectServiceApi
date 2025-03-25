using
Microsoft.EntityFrameworkCore;
using
Microsoft.Extensions.Options;
using ProyectServiceClient.Models;
using ProyectServiceShared.Models;

using
System.Net.Sockets;
namespace PoryectServiceApi.Server.Data
{
    public class
    ApplicationDbContext : DbContext
    {
        public
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        {
        }
        protected
        override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
       
        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services  { get; set; }
    }
}