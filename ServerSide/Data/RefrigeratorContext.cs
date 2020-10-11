using Microsoft.EntityFrameworkCore;
using Refrigerator.Models;

namespace Refrigerator.Data 
{
    public class RefrigeratorContext: DbContext
    {
        public RefrigeratorContext(DbContextOptions<RefrigeratorContext> opt) : base(opt) {}

        public DbSet<Place> Places {get; set;}
    }
}