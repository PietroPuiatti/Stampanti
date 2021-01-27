using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stampante1 = new Stampanti.Models.Stampante();
            stampante1.Nome = "Boh3";
            stampante1.IP = "3.3.33.3";
            stampante1.Port = 9100;
            var stampante_1 = new Stampanti.Data.StampantiRepository();
            stampante_1.AddStampante(stampante1);
            Console.WriteLine(stampante1);
            Console.Read();

        }
    }
}
