using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;


namespace ApiCatalogoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Listar( int pagina , int quantidade);
        Task<JogoViewModel> Detalhes(Guid idJogo);

        Task<JogoViewModel> NovoJogo(JogoInputModel jogo);

        Task Atualizar(Guid idJogo, JogoInputModel jogo);

        Task Atualizar(Guid idJogo , double Preco);

        Task DeletarJogo(Guid idJogo);

        Task<int> Total();
    }
}
