namespace ICS.Workout;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), WorkoutDbContext.SchemaName);
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId);
    }
}