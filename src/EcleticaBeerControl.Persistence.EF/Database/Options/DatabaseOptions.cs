namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal sealed class DatabaseOptions
    {
        public required string ConnectionString { get; set; } = string.Empty;
        public int MaxRetryCount { get; set; }
        public int CommandTimeout { get; set; }
        public bool EnableDetailedErrors { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
    }
}
