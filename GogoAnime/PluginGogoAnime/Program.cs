using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginGogoAnime
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Se requiere un unico parametro 'baseUrl'");
                Console.WriteLine(args.Length);
                return;
            }
            
            string url = args[0];

            IPlugin plugin;

            if (url.Contains(url))
            {
                plugin = new Plugin(url);
            }
            else
            {
                throw new NotSupportedException("URL no soportada");
            }
            

            plugin.ListarVideosDisponibles("https://www.commoncraft.com/videolist");
            byte[] b = plugin.Bajar();
            //BinaryWriter writer = new BinaryWriter(
            //writer.Write(b);
        }
    }
}
