namespace ICS;

public static class Constants
{
    public const int NameLength = 256;
    public const int DescriptionLength = 256;

    public const string DefaultCulture = "en-US";
    public static readonly string[] SupportedCultures = { DefaultCulture, "es" };

    public static string EfMigrationTable = "__EFMigrationsHistory";
    public static string DebugConnectionString = "Data Source=localhost; Integrated Security=true; Initial Catalog=IronClanStudios;";

    public static class AuthRoles
    {
        public static string Member = "Member";
        public static string Admin = "Admin";
    }
}