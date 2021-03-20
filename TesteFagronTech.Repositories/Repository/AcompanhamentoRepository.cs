using Dapper;
using System.Collections.Generic;
using System.Linq;
using TesteFagronTech.Models.Model;
using TesteFagronTech.Repositories.Interface;

namespace TesteFagronTech.Repositories.Repository
{
    public class AcompanhamentoRepository : Database, IAcompanhamentoRepository
    {
        public bool Add(AcompanhamentoPartidaModel pontosPartida)
        {
            var query = "INSERT INTO AcompanhamentoPartida VALUES (@QuantidadePontos, @DataPartida)";

            return _conn.Execute(query, pontosPartida) > 0;
        }

        public List<AcompanhamentoPartidaModel> GetAllAcompanhamentoPartidas()
        {
            var query = "SELECT * FROM AcompanhamentoPartida ORDER BY DataPartida";

            return _conn.Query<AcompanhamentoPartidaModel>(query).ToList();
        }
    }
}
