
namespace GzReservation.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .Property(e => e.UUID)
                .HasDefaultValueSql("uuid_generate_v4()"); // Configuring to use PostgreSQL UUID generation

            modelBuilder.Entity<Form>()
                .Property(e => e.actiondate)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Reservation>()
               .Property(r => r.action_date)
               .HasColumnType("timestamp without time zone")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");


        }
        public DbSet<Form> forms { get; set; }
        public DbSet<UserEntity> usersentity { get; set; }

        public DbSet<Entity> entities { get; set; }
        public DbSet<Reservation> reservations { get; set; }



    }
}
