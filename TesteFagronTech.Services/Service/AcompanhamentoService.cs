using System;
using System.Collections.Generic;
using System.Linq;
using TesteFagronTech.Models.Model;
using TesteFagronTech.Models.ViewModel;
using TesteFagronTech.Repositories.Interface;
using TesteFagronTech.Repositories.Repository;
using TesteFagronTech.Services.Interface;

namespace TesteFagronTech.Services.Service
{
    public class AcompanhamentoService : IAcompanhamentoService
    {
        public IAcompanhamentoRepository _acompanhamentoRepository { get; set; }

        public AcompanhamentoService()
        {
            _acompanhamentoRepository = new AcompanhamentoRepository();
        }

        public ResponseViewModel<bool> Add(AcompanhamentoPartidaModel pontosPartida)
        {
            var response = new ResponseViewModel<bool>() { Success = false, Content = false };
            try
            {
                if (pontosPartida.DataPartida == null)
                {
                    response.Errors.Add("A data da partida não pode ser vazia");
                }

                if (response.Errors?.Count == 0)
                {
                    response.Content = _acompanhamentoRepository.Add(pontosPartida);
                    response.Success = true;
                }

            }
            catch (Exception e)
            {
                response.Content = false;
                response.Errors.Add(e.Message);
            }
            return response;
        }

        public ResponseViewModel<ResultadosViewModel> VerResultados()
        {
            var response = new ResponseViewModel<ResultadosViewModel>() { Success = false };
            try
            {
                var partidas = _acompanhamentoRepository.GetAllAcompanhamentoPartidas();
                ResultadosViewModel resultados = new ResultadosViewModel();

                if (partidas == null || partidas.Count == 0)
                {
                    response.Errors.Add("Não encontramos nenhuma partida");
                }

                if (response.Errors?.Count == 0)
                {
                    resultados = new ResultadosViewModel()
                    {
                        DataInicio = partidas.Min(x => x.DataPartida).ToString("dd/MM/yyyy"),
                        DataFim = partidas.Max(x => x.DataPartida).ToString("dd/MM/yyyy"),
                        JogosDisputados = partidas.Count,
                        MaiorPontuacaoJogo = partidas.Max(x => x.QuantidadePontos),
                        MenorPontuacaoJogo = partidas.Min(x => x.QuantidadePontos),
                        MediaPorJogo = partidas.Average(x => x.QuantidadePontos),
                        TotalDePontosMarcados = partidas.Sum(x => x.QuantidadePontos),
                        QuantidadeVezesRecorde = GetQuantidadeVezesRecorde(partidas)
                    };
                    response.Success = true;
                }

                response.Content = resultados;
            }
            catch (Exception e)
            {
                response.Errors.Add(e.Message);
            }
            return response;
        }

        private int GetQuantidadeVezesRecorde(List<AcompanhamentoPartidaModel> partidas)
        {
            var maiorPontuacaoAnterior = partidas?.First().QuantidadePontos;
            var quantidadeVezesRecorde = 0;

            foreach (var partida in partidas)
            {
                if (partida.QuantidadePontos > maiorPontuacaoAnterior)
                {
                    maiorPontuacaoAnterior = partida.QuantidadePontos;
                    quantidadeVezesRecorde++;
                }
            }

            return quantidadeVezesRecorde;
        }
    }
}
