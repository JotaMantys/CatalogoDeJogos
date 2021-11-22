using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        
        private static Dictionary<Guid, Jogo> Jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4941"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4941"),
                Nome="1",
                Preco=1,
                Produtora="P1"
              }
            },
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4942"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4942"),
                Nome="2",
                Preco=2,
                Produtora="P2"
              }
            },
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4943"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4943"),
                Nome="3",
                Preco=3,
                Produtora="P3"
              }
            },
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4944"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4944"),
                Nome="4",
                Preco=4,
                Produtora="P4"
              }
            },
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4945"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4945"),
                Nome="5",
                Preco=5,
                Produtora="P5"
              }
            },
            { Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4946"),
              new Jogo()
              {
                Id = Guid.Parse("eea5e71e-b2fa-4e78-913f-d6d813ee4946"),
                Nome="6",
                Preco=6,
                Produtora="P6"
              }
            }
        };

       
       
        public Task<List<Jogo>> Listar(int pagina, int quantidade)
        {
            return Task.FromResult(
                Jogos.Values.Skip((pagina - 1) * quantidade)
                .Take(quantidade).ToList()
                );
        }

        public Task<Jogo> Listar(Guid id)
        {
            if (! Jogos.ContainsKey(id))
            {
                return Task.FromResult(new Jogo { Id=Guid.Empty,Nome="",Produtora="",Preco=0 });
            }

            return Task.FromResult(Jogos[id]);
        }

        public Task<List<Jogo>> Listar(string nome, string produtora)
        {
            return Task.FromResult(
                Jogos.Values.Where(
                    jogo => jogo.Nome.Equals(nome) 
                         && jogo.Produtora.Equals( produtora))
                .ToList()
                );
        }
        public Task Inserir(Jogo jogo)
        {
            Jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            Jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }
        public Task Remover(Guid id)
        {
            Jogos.Remove(id);
            return Task.CompletedTask;
        }

        public Task<int> Total()
        {
            return Task.FromResult( Jogos.Count());
        }
        public void Dispose()
        {
            //sem implementação
        }


    }
}
