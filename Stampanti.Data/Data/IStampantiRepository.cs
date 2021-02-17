using Stampanti.Models;
using System.Collections.Generic;

namespace Stampanti.Data
{
    public interface IStampantiRepository
    {
        void AddStampante(Stampante stampante);
        void DeleteStampante(int id);
        Stampante GetStampanteById(int id);
        Stampante GetStampanteByNome(string nome);
        List<Stampante> GetStampanti();
        void UpdateStampante(Stampante x);

    }
}