using Microsoft.EntityFrameworkCore;

namespace AplikacjaRestAPI.Models
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Root-PC\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
