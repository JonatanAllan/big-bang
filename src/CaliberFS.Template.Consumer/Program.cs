using CaliberFS.Template.Bootstrapper.DependencyInjection;

namespace CaliberFS.Template.Consumer;

public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureSerilog()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}