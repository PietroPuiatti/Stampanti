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
            var stampantiRepository = new Stampanti.Data.StampantiRepository();
            stampantiRepository.AddStampante(stampante1);
            ElencoStampanti();
            stampantiRepository.DeleteStampante(stampante1);
            ElencoStampanti();
            Console.Read();

        }

        static void ElencoStampanti()
        {
            int count = 1;
            var repo = new Stampanti.Data.StampantiRepository();
            foreach(var stamp in repo.GetStampanti())
            {
                Console.WriteLine($"{count} {stamp.Nome} - {stamp.IP}: {stamp.Port}"); 
                count++;
            }
        }
    }
}
