using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Projects.Providers.Database
{
    public class MyContext : AuditDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        //public DbSet<EventEntry> Event { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<Audit_ContactEntity> Audit_Contacts { get; set; }


        public DbSet<ValueEntity> Values { get; set; }
        public DbSet<Audit_ValueEntity> Audit_Values { get; set; }
    }
}