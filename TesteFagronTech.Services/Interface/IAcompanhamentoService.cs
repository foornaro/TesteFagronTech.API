using TesteFagronTech.Models.Model;
using TesteFagronTech.Models.ViewModel;
using TesteFagronTech.Repositories.Interface;

namespace TesteFagronTech.Services.Interface
{
    public interface IAcompanhamentoService
    {
        IAcompanhamentoRepository _acompanhamentoRepository { get; set; }

        ResponseViewModel<bool> Add(AcompanhamentoPartidaModel pontosPartida);
        ResponseViewModel<ResultadosViewModel> VerResultados();
    }
}
