﻿using Stampanti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Stampanti.Data
{
    public class StampantiRepository
    {
        
        private List<Stampante> _stampanti;

        public StampantiRepository()
        {
            _stampanti = ReadStampanti();

            
        }

        public List<Stampante> GetStampanti()
        {
            return _stampanti;
            
        }

        public void AddStampante(Stampante stampante)
        {
            var printer = GetStampante(stampante.Nome);
            if (printer == null)
            {

                _stampanti.Add(stampante);
                SaveStampanti();
            }
            
        }

        private void SaveStampanti()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Stampante>));
            using (TextWriter writer = new StreamWriter(@"C:\Users\Pietro\source\repos\Stampanti\Lista.xml"))
            {
                serializer.Serialize(writer, _stampanti);
            }
        }

        public void UpdateStampante(Stampante x)
        {
            
            
            if (x.Nome != null)
            {
                GetStampante(x.Nome);
                
            }
        }

        
        public void DeleteStampante(Stampante x)
        {
            var printer = GetStampante(x.Nome);
            
            if (x.Nome!= null)
            {
                _stampanti.Remove(printer);
                SaveStampanti();
            }
           
           
        }

        private List<Stampante> ReadStampanti()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Stampante>));
            using (var stream = new FileStream(@"C:\Users\Pietro\source\repos\Stampanti\Lista.xml", FileMode.Open))
            {
                return serializer.Deserialize(stream)as List<Stampante>;
            }
        }

        public Stampante GetStampante(string nome)
        {
           
            
            return _stampanti.Find(p=>p.Nome==nome);

        }

        public string GetNome(int i)
        { 
            return _stampanti[i].Nome;
        }

        public string GetIP(int x)
        {

            return _stampanti[x].IP;
        }

       
    }
}