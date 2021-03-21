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
                //Valida se a data foi enviada
                if (pontosPartida.DataPartida == default(DateTime))
                {
                    response.Errors.Add("A data da partida não pode ser vazia");
                }

                //Caso não tenha nenhum erro, adiciona a partida ao banco
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
            ResultadosViewModel resultados = new ResultadosViewModel();

            var response = new ResponseViewModel<ResultadosViewModel>() { Success = false , Content = resultados };

            try
            {
                //Busca todas as partidas
                var partidas = _acompanhamentoRepository.GetAllAcompanhamentoPartidas();

                //Valida se existe alguma partida
                if (partidas == null || partidas.Count == 0)
                {
                    response.Errors.Add("Não encontramos nenhuma partida");
                }

                //Caso não tenha nenhum erro, faz os calculos
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

        /// <summary>
        /// Calcula quantas vezes o jogador bateu o proprio record
        /// </summary>
        /// <param name="partidas"></param> Lista de resultados do jogador
        /// <returns></returns>
        private int GetQuantidadeVezesRecorde(List<AcompanhamentoPartidaModel> partidas)
        {
            //Ordena a lista por DataPartida
            partidas = partidas.OrderBy(x => x.DataPartida).ToList();
            
            //Salva a primeira pontuação do jogador
            var maiorPontuacaoAnterior = partidas?.First().QuantidadePontos;
            var quantidadeVezesRecorde = 0;

            //Verifica todas as partidas e verifica cada vez que o recorde foi batido
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
