namespace Sportverein.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new HostBuilder();
        builder.UseContentRoot(Directory.GetCurrentDirectory());

        builder.ConfigureAppConfiguration((hostingContext, config) => {
            var env = hostingContext.HostingEnvironment;
            config
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional:true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
        });

        builder.ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });

        builder.ConfigureLogging(logging => {
            logging.AddConsole();
        });

        var host = builder.Build();
        host.Run();
    }
}