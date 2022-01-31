
using Microsoft.EntityFrameworkCore;

namespace esr_core
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event>? Event { get; set; } = default;
        public EventDbContext(DbContextOptions<EventDbContext> options) 
            : base(options)
        { 

        }
    }
}
