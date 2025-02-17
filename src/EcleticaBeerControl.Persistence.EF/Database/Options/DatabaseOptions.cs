namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal sealed class DatabaseOptions : BaseDatabaseOptions
    {
        public int MaxRetryCount { get; set; }
        public int CommandTimeout { get; set; }
        public bool EnableDetailedErrors { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }
    }
}
