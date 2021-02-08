using System;
using System.Collections.Generic;
using System.Configuration;
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
            string _connectionString = ConfigurationManager.ConnectionStrings["dbStampanti"].ConnectionString;
            var stampante1 = new Stampante();
            stampante1.Nome = "DadPrinter";
            stampante1.IP = "3.3.33.3";
            stampante1.Port = 9100;
            stampante1.Id = 8;
            var stampantiRepository = new StampantiDbRepository(_connectionString);
            try
            {
                 stampantiRepository.UpdateStampante(stampante1);
                 ElencoStampanti();
            }
             catch(Exception e)          
            {
                Console.WriteLine(e.ToString());
            }
            Console.Read();
        }

        static void ElencoStampanti()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["dbStampanti"].ConnectionString;
            int count = 1;
            var repo = new StampantiDbRepository(_connectionString);
            foreach(var stamp in repo.GetStampanti())
            {
                Console.WriteLine($"{count} {stamp.Nome} - {stamp.IP}: {stamp.Port}"); 
                count++;
            }
        }
    }
}
