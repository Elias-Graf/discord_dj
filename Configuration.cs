using Microsoft.Extensions.Configuration;

public class Configuration
{
    public readonly String DISCORD_CLIENT_TOKEN;

    public static Configuration Load()
    {
        // This just calls the (private) constructor.
        // The static load method just makes it cleared that it actually
        // fetches data.
        return new Configuration();
    }

    private Configuration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();

        DISCORD_CLIENT_TOKEN = builder.GetSection("DISCORD_CLIENT_TOKEN").Value;
    }
}