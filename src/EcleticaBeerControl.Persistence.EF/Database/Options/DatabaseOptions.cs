namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal sealed class DatabaseOptions : BaseDatabaseOptions
    {
        public bool EnableDetailedErrors { get; set; } = true;
        public bool EnableSensitiveDataLogging { get; set; } = true;
    }
}
