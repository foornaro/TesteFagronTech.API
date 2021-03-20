using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TesteFagronTech.Models.Model;
using TesteFagronTech.Services.Interface;

namespace FagronTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcompanhamentoController : ControllerBase
    {
        private IAcompanhamentoService _acompanhamentoService { get; set; }

        public AcompanhamentoController(IAcompanhamentoService acompanhamentoService)
        {
            _acompanhamentoService = acompanhamentoService;
        }

        [HttpPost("AdicionarPontos")]
        public ActionResult AdicionarPontos([FromBody] AcompanhamentoPartidaModel pontos)
        {
            return new ContentResult() { Content = JsonConvert.SerializeObject(_acompanhamentoService.Add(pontos)) };
        }

        [HttpGet("VerResultados")]
        public ActionResult VerResultados()
        {
            return new ContentResult() { Content = JsonConvert.SerializeObject(_acompanhamentoService.VerResultados()) };
        }
    }
}
