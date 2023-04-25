namespace ICS.Auth0;

public class Auth0DbContext : DbContext
{
    public const string SchemaName = "auth0";

    public virtual DbSet<User> User { get; set; } = null!;
    public virtual DbSet<Identity> Identity { get; set; } = null!;

    public Auth0DbContext(DbContextOptions<Auth0DbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new IdentityConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}
