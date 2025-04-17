using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EFCore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cria e executa o servidor web
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>(); // Configuração do Startup.cs
    }
}
