using Microsoft.EntityFrameworkCore;

namespace SocomapLib
{
    public class TransmissionContext : DbContext
    {
        public TransmissionContext(DbContextOptions<TransmissionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inbox> Inboxes { get; set; }
        public virtual DbSet<Transmission> Transmissions { get; set; }
    }
}
