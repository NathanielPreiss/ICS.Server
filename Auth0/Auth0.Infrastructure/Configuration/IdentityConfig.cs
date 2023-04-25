namespace ICS.Auth0;

public class IdentityConfig : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.ToTable(nameof(Identity), Auth0DbContext.SchemaName);
        builder.HasKey(x => new { x.UserId, x.Provider, x.ProviderId });
        builder.HasAlternateKey(x => new { x.UserId, x.Provider });

        builder.Property(x => x.UserId).ValueGeneratedNever();
        builder.Property(x => x.Provider).IsRequired();
        builder.Property(x => x.ProviderId).IsRequired();
        builder.Property(x => x.CreatedUtc).IsRequired();
        builder.Ignore(x => x.ProviderIdentity);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Identities)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}