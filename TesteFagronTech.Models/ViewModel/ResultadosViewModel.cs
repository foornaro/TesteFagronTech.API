using System;

namespace TesteFagronTech.Models.ViewModel
{
    public class ResultadosViewModel
    {

        public string DataInicio { get; set; }

        public string DataFim { get; set; }

        public int JogosDisputados { get; set; }

        public int TotalDePontosMarcados { get; set; }

        public double MediaPorJogo { get; set; }

        public int MaiorPontuacaoJogo { get; set; }

        public int MenorPontuacaoJogo { get; set; }

        public int QuantidadeVezesRecorde { get; set; }
    }
}
