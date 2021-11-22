using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.Exception;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        ///<summary>
        /// Buscar todos os jogos com paginação para o total de paginas utilize o header total divido pela quantidade de paginas
        ///</summary>
        ///<remarks>
        /// Não é possivel encontrar jogos sem paginação
        ///</remarks>
        ///<param name="pagina">Indica qual pagina será carregada, minimo 1</param>
        ///<param name="quantidade">Indica a quantidade de registros por pagina. minimo 1 e maximo 50</param>
        ///<response code="200">Retorna a lista de jogos</response>
        ///<response code="204">Caso não haja Jogos</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Listar([FromQuery, Range(1, int.MaxValue)] int pagina =1 , [FromQuery, Range(1, 50)]  int quantidade = 5  )
        {
            var jogos = await _jogoService.Listar(pagina, quantidade);
            var total = await _jogoService.Total()+1;
            //throw new System.Exception();
            Response.Headers.Add("Total", total.ToString());

            if (jogos.Count()==0)
            {
                return NoContent();
            }

            return Ok(jogos);

        }


        [HttpGet("{idJogo:Guid}")]
        
        public async Task<ActionResult<JogoViewModel>> Detalhes([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.Detalhes(idJogo);

            
            if (jogo.Id == Guid.Empty)
            {
                return NoContent();
            }

            return Ok(jogo);

        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> NovoJogo([FromBody] JogoInputModel jogoInput)
        {
            try
            {
                var jogo = await _jogoService.NovoJogo(jogoInput);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            {

                return UnprocessableEntity("já existe um jogo cadastrado com esse nome para esta produtora");
            }
            
        }

        [HttpPut("{idJogo:Guid}")]
        public async Task<ActionResult> Atualizar([FromRoute] Guid idJogo ,[FromBody] JogoInputModel jogoInput)
        {
            try
            {
                 await _jogoService.Atualizar(idJogo, jogoInput);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {

                return NotFound("Jogo Inexistente");
            }

            
        }

        [HttpPatch("{idJogo:Guid}/preco/{preco:double}")]
        public async Task<ActionResult> Atualizar([FromRoute]Guid idJogo , [FromRoute]double preco)
        {

            try
            {
                await _jogoService.Atualizar(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {

                return NotFound("Jogo Inexistente");
            }

            
        }

        [HttpDelete("{idJogo:Guid}")]
        public async Task<ActionResult> DeletarJogo([FromRoute]Guid idJogo)
        {

            try
            {
                await _jogoService.DeletarJogo(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {

                return NotFound("Jogo Inexistente");
            }
        }


    }
}
