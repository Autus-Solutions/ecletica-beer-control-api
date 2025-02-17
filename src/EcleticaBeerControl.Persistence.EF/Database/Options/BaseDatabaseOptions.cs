namespace EcleticaBeerControl.Persistence.EF.Database.Options
{
    internal abstract class BaseDatabaseOptions
    {
        public required string ConnectionString { get; set; } = string.Empty;
    }
}