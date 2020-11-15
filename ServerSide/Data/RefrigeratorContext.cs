using Microsoft.EntityFrameworkCore;
using RemoteProvider.Models;
using RefrigeratorServerSide;

namespace RefrigeratorServerSide.Data 
{
    public class RefrigeratorContext: DbContext
    {
        public RefrigeratorContext(DbContextOptions<RefrigeratorContext> opt) : base(opt) {}
        
        public DbSet<Refrigerator> Refrigerator { get; set; }
        public DbSet<RefrigeratorBlock> RefrigeratorBlock { get; set; }
        public DbSet<SensorData> SensorData { get; set;}
    }
}