using System.Collections.Generic;
using TesteFagronTech.Models.Model;

namespace TesteFagronTech.Repositories.Interface
{
    public interface IAcompanhamentoRepository
    {
        bool Add(AcompanhamentoPartidaModel pontosPartida);

        List<AcompanhamentoPartidaModel> GetAllAcompanhamentoPartidas();
    }
}
