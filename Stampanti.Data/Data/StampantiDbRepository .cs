using Stampanti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Web.Hosting;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Stampanti.Data
{
    public class StampantiDbRepository : IStampantiRepository
    {
        private readonly string _connectionString;

        public StampantiDbRepository(string connectionString)
        {
            _connectionString = connectionString;
            
        }

        public List<Stampante> GetStampanti()
        {
            return ReadStampanti();
            
        }

        public void AddStampante(Stampante stampante)
        {

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Insert<Stampante>(stampante);
                }
                ReadStampanti();
            }
            catch(Exception e)
            {
                throw new Exception("Errore nell'Add", e);
            }
        }

        
        public void UpdateStampante(Stampante x)
        {

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Update(x);
                }
            }
            catch(Exception e)
            {
                throw new Exception("Errore nell'Update", e);
            }
        }

        
        public void DeleteStampante(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Delete(new Stampante { Id = id });
                }
            }
            catch(Exception e)
            {
                throw new Exception("Errore nel Delete", e);

            }

        }

        private List<Stampante> ReadStampanti()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT id, RTRIM(Nome) as Nome, RTRIM(IP) as IP, Port FROM Stampanti";
                    return (connection.Query<Stampante>(query).ToList());
                }
            }
            catch(Exception e) 
            {
                throw new Exception("Errore nel Read", e);
            }
        }

        public Stampante GetStampanteByNome(string nome)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var stampante = connection.Get<Stampante>(nome);
                    stampante.Nome = stampante.Nome.TrimEnd();
                    stampante.IP = stampante.IP.TrimEnd();

                    return stampante;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Errore nel GetStampanteByNome", e);
            }

        }

        public Stampante GetStampanteById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var stampante = connection.Get<Stampante>(id);
                    stampante.Nome = stampante.Nome.TrimEnd();
                    stampante.IP = stampante.IP.TrimEnd();

                    return stampante;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Errore nel GetStampanteById", e);
            }
        }
    }
}