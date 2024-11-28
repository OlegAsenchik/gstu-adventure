using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Listener> Listeners { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<ListenerCourse> ListenerCourses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Debtor> Debtors { get; set; }

    // Конструктор, принимающий DbContextOptions
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Убедитесь, что строка подключения указывается только при первом запуске (если не передана в конструктор)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=db8629.databaseasp.net; Database=db8629; User Id=db8629; Password=o-7XJ4n=_F2m; Encrypt=False; MultipleActiveResultSets=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ListenerCourse: Many-to-Many между Listener и Course
        modelBuilder.Entity<ListenerCourse>()
            .HasOne(lc => lc.Listener)
            .WithMany(l => l.ListenerCourses)
            .HasForeignKey(lc => lc.ListenerID);

        modelBuilder.Entity<ListenerCourse>()
            .HasOne(lc => lc.Course)
            .WithMany(c => c.ListenerCourses)
            .HasForeignKey(lc => lc.CourseID);

        // Debtor: One-to-One relationship с Listener
        modelBuilder.Entity<Debtor>()
            .HasOne(d => d.Listener)
            .WithOne(l => l.Debtor)
            .HasForeignKey<Debtor>(d => d.ListenerID);
    }
}
