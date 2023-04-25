namespace ICS.Auth0;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), Auth0DbContext.SchemaName);
        builder.HasKey(x => x.UserId);
        builder.HasAlternateKey(x => x.Email);

        builder.Property(x => x.UserId).ValueGeneratedNever();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.CreatedUtc).IsRequired();
    }
}