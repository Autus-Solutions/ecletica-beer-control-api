namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal abstract class BaseDatabaseOptions
    {
        public required string ConnectionString { get; set; } = string.Empty;
        public int MaxRetryCount { get; set; } = 3;
        public int CommandTimeout { get; set; } = 30;
    }
}