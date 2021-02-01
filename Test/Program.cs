using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stampanti.Data;
using Stampanti.Models;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stampante1 = new Stampante();
            stampante1.Nome = "DadPrinter";
            stampante1.IP = "3.3.33.3";
            stampante1.Port = 9100;

            var stampantiRepository = new StampantiRepository();
            //stampantiRepository.AddStampante(stampante1);
            ElencoStampanti();

            var stampante = stampantiRepository.GetStampante("Nome");
            
            stampantiRepository.UpdateStampante(nome:"Nome3", stampante);
            ElencoStampanti();
            Console.Read();

        }

        static void ElencoStampanti()
        {
            int count = 1;
            var repo = new StampantiRepository();
            foreach(var stamp in repo.GetStampanti())
            {
                Console.WriteLine($"{count} {stamp.Nome} - {stamp.IP}: {stamp.Port}"); 
                count++;
            }
        }
    }
}
