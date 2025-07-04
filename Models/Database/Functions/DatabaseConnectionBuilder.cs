namespace GlassApplication.Models.Database.DatabaseFunctions;

public class DatabaseConnectionBuilder
{
    private readonly IConfiguration _configuration;
    
    public DatabaseConnectionBuilder(IConfiguration configuration)
        => _configuration = configuration;

    public string GetConnectionString(string databaseName)
    {
        var baseConnection = _configuration.GetConnectionString("DefaultConnection");
        return $"{baseConnection};Database={databaseName}";
    }
}