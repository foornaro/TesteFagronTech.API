using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteFagronTech.Models.Model;
using TesteFagronTech.Models.ViewModel;
using TesteFagronTech.Repositories.Interface;
using TesteFagronTech.Services.Service;

namespace TesteFagronTech.Tests
{
    [TestClass]
    public class AcompanhamentoServiceTest
    {
        public Mock<IAcompanhamentoRepository> _acompanhamentoRepositoryMock { get; set; }
        public AcompanhamentoService _acompanhamentoService { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _acompanhamentoRepositoryMock = new Mock<IAcompanhamentoRepository>();

            _acompanhamentoService = new AcompanhamentoService()
            {
                _acompanhamentoRepository = _acompanhamentoRepositoryMock.Object
            };
        }

        [TestMethod]
        public void TesteAddSucesso()
        {
            //Arrange
            AcompanhamentoPartidaModel partida = new AcompanhamentoPartidaModel()
            {
                DataPartida = DateTime.Now,
                QuantidadePontos = 10
            };

            _acompanhamentoRepositoryMock.Setup(x => x.Add(partida)).Returns(true);

            //Act
            var response = _acompanhamentoService.Add(partida);

            //Assert
            Assert.IsTrue(response.Success && response.Content);
            Assert.IsTrue(response.Errors.Count == 0);
        }

        [TestMethod]
        public void TesteAddComErroDeDataInvalida()
        {
            //Arrange
            AcompanhamentoPartidaModel partida = new AcompanhamentoPartidaModel()
            {
                DataPartida = default(DateTime),
                QuantidadePontos = 10
            };

            _acompanhamentoRepositoryMock.Setup(x => x.Add(partida)).Returns(true);

            //Act
            var response = _acompanhamentoService.Add(partida);

            //Assert
            Assert.IsFalse(response.Success && response.Content);
            Assert.IsTrue(response.Errors.Count == 1);
            Assert.IsTrue(response.Errors.First().Equals("A data da partida não pode ser vazia"));
        }

        [TestMethod]
        public void TesteAddException()
        {
            //Arrange
            AcompanhamentoPartidaModel partida = new AcompanhamentoPartidaModel()
            {
                DataPartida = DateTime.Now,
                QuantidadePontos = 10
            };

            _acompanhamentoRepositoryMock.Setup(x => x.Add(partida)).Throws(new Exception("Unhandled database exception"));

            //Act
            var response = _acompanhamentoService.Add(partida);

            //Assert
            Assert.IsFalse(response.Success && response.Content);
            Assert.IsTrue(response.Errors.Count == 1);
            Assert.IsTrue(response.Errors.First().Equals("Unhandled database exception"));
        }


        [TestMethod]
        public void TesteVerResultadosSucesso()
        {
            //Arrange
            List<AcompanhamentoPartidaModel> partidas = new List<AcompanhamentoPartidaModel>()
            {
                new AcompanhamentoPartidaModel()
                {
                    Id = 1,
                    QuantidadePontos = 25,
                    DataPartida = DateTime.Now.AddDays(-1)
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 2,
                    QuantidadePontos = 50,
                    DataPartida = DateTime.Now
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 3,
                    QuantidadePontos = 35,
                    DataPartida = DateTime.Now.AddDays(1)
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 4,
                    QuantidadePontos = 75,
                    DataPartida = DateTime.Now.AddDays(2)
                },
            };

            _acompanhamentoRepositoryMock.Setup(x => x.GetAllAcompanhamentoPartidas()).Returns(partidas);

            //Act
            var response = _acompanhamentoService.VerResultados();
            
            //Assert
            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Errors.Count == 0);

            Assert.IsTrue(response.Content.DataInicio.Equals(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy")));
            Assert.IsTrue(response.Content.DataFim.Equals(DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")));
            Assert.IsTrue(response.Content.JogosDisputados == 4);
            Assert.IsTrue(response.Content.MaiorPontuacaoJogo == 75);
            Assert.IsTrue(response.Content.MenorPontuacaoJogo == 25);
            Assert.IsTrue(response.Content.MediaPorJogo == 46.25);
            Assert.IsTrue(response.Content.QuantidadeVezesRecorde == 2);
        }

        [TestMethod]
        public void TesteVerResultadosErroSemPartidas()
        {
            //Arrange
            List<AcompanhamentoPartidaModel> partidas = new List<AcompanhamentoPartidaModel>();

            _acompanhamentoRepositoryMock.Setup(x => x.GetAllAcompanhamentoPartidas()).Returns(partidas);

            //Act
            var response = _acompanhamentoService.VerResultados();

            //Assert
            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.Errors.Count == 1);
            Assert.IsTrue(response.Errors.First().Equals("Não encontramos nenhuma partida"));
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void TesteVerResultadosException()
        {
            //Arrange
            List<AcompanhamentoPartidaModel> partidas = new List<AcompanhamentoPartidaModel>()
            {
                new AcompanhamentoPartidaModel()
                {
                    Id = 1,
                    QuantidadePontos = 25,
                    DataPartida = DateTime.Now.AddDays(-1)
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 2,
                    QuantidadePontos = 50,
                    DataPartida = DateTime.Now
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 3,
                    QuantidadePontos = 35,
                    DataPartida = DateTime.Now.AddDays(1)
                },
                new AcompanhamentoPartidaModel()
                {
                    Id = 4,
                    QuantidadePontos = 75,
                    DataPartida = DateTime.Now.AddDays(2)
                },
            };

            _acompanhamentoRepositoryMock.Setup(x => x.GetAllAcompanhamentoPartidas()).Throws(new Exception("Unhandled database exception"));

            //Act
            var response = _acompanhamentoService.VerResultados();

            //Assert
            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.Errors.Count == 1);
            Assert.IsTrue(response.Errors.First().Equals("Unhandled database exception"));
            Assert.IsNotNull(response.Content);
        }
    }
}
