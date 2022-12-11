namespace TestingWebApi.Core;

internal abstract class Program
{
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    private static IHostBuilder CreateHostBuilder(params string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());
}