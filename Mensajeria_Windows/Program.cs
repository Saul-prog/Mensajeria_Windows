
using System.Xml.Xsl;

namespace Mensajeria_Windows
{
    //Internal->nivel de seguridad, solo accesible por miembros del mismo assembly

    internal class Program
    {
        protected Program()
        {
            
        }
        //metodo de arranque
        protected static void Main ( string[] args)
        {
            CreateHostBuilder(args).Build().Run();
                
        }

        private static IHostBuilder CreateHostBuilder (string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration(app => InitConfiguration(app ) ).
                ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }


        private static IConfigurationBuilder InitConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath (Directory.GetCurrentDirectory ());

            configurationBuilder.AddJsonFile($@"appsettings.{Environment.UserName}.json",true);

            configurationBuilder.AddEnvironmentVariables();

            return configurationBuilder;
        }
    }

}