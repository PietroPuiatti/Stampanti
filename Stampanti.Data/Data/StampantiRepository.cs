﻿using Stampanti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Web.Hosting;
using System.Configuration;

namespace Stampanti.Data
{
    public class StampantiRepository : IStampantiRepository
    {

        private string Path = HostingEnvironment.MapPath(@"\Lista.xml");
        private List<Stampante> _stampanti;
        //string Path = ConfigurationManager.AppSettings["xmlPath"].ToString();



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
            var printer = GetStampanteByNome(stampante.Nome);
            if (printer == null)
            {
                var ids = (from s in _stampanti
                           select s.Id).ToList();

                var nextID = ids.Any() ? ids.Max() + 1 : 1;
                stampante.Id = nextID;

                _stampanti.Add(stampante);
                SaveStampanti();
            }

        }

        private void SaveStampanti()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Stampante>));
            using (TextWriter writer = new StreamWriter(Path))
            {
                serializer.Serialize(writer, _stampanti);
            }
        }

        public void UpdateStampante(Stampante x)
        {
            var printer = GetStampanteById(x.Id);


            if (printer != null)
            {
                printer.Nome = x.Nome;
                printer.IP = x.IP;
                printer.Port = x.Port;

                SaveStampanti();
            }

        }


        public void DeleteStampante(int id)
        {
            var printer = GetStampanteById(id);

            if (printer != null)
            {
                _stampanti.Remove(printer);
                SaveStampanti();
            }


        }

        private List<Stampante> ReadStampanti()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Stampante>));
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as List<Stampante>;
            }
        }

        public Stampante GetStampanteByNome(string nome)
        {
            return _stampanti.Find(p => p.Nome == nome);
        }

        public Stampante GetStampanteById(int id)
        {
            return _stampanti.Find(p => p.Id == id);
        }

        public void Save()
        {
            SaveStampanti();
        }
        public void MangaeExtras() { }

        //public void ManageExtras(Stampante stampante)
        //{
        //    throw new NotImplementedException();
        //}
    }
}