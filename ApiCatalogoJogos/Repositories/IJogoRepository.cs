using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;

namespace ApiCatalogoJogos.Repositories
{
    public interface IJogoRepository : IDisposable
    {

        Task<List<Jogo>> Listar(int pagina , int quantidade);

        Task<Jogo> Listar(Guid id);

        Task<List<Jogo>> Listar(string nome,string produtora);

        Task Inserir(Jogo jogo);

        Task Atualizar(Jogo jogo);

        Task Remover(Guid id);

        Task<int> Total();
    }
}
