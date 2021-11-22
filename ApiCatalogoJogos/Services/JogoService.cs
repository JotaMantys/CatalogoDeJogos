using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using ApiCatalogoJogos.Exception;
using ApiCatalogoJogos.Entities;

namespace ApiCatalogoJogos.Services
{
    public class JogoService : IJogoService
    {

        private readonly IJogoRepository _jogoRepository;
        
        public JogoService( IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

                
        public async Task<List<JogoViewModel>> Listar(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Listar(pagina, quantidade);


            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            }).ToList();



        }

        public async Task<JogoViewModel> Detalhes(Guid idJogo)
        {
           
            var jogo = await _jogoRepository.Listar(idJogo);

            /*
            if (jogo.Id == Guid.Empty)
            {
                return null;
            }*/
         
            return new JogoViewModel 
            { 
                Id = jogo.Id, 
                Nome = jogo.Nome, 
                Produtora = jogo.Produtora, 
                Preco = jogo.Preco 
            };


       
        }

        public async Task<JogoViewModel> NovoJogo(JogoInputModel jogo)
        {
            var jogoCadastro = await _jogoRepository.Listar(jogo.Nome , jogo.Produtora);

            if (jogoCadastro.Count > 0)
            {
                throw new JogoJaCadastradoException();
            }

            var novo = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };

            await _jogoRepository.Inserir(novo);

            return new JogoViewModel
            {
                Id = novo.Id,
                Nome = novo.Nome,
                Produtora = novo.Produtora,
                Preco = novo.Preco
            };
            
        }


        public async Task Atualizar(Guid idJogo, JogoInputModel jogo)
        {
            var jogoCadastro = await _jogoRepository.Listar(idJogo);

            if (jogoCadastro ==null)
            {
                throw new JogoNaoCadastradoException();
            }

            jogoCadastro.Nome = jogo.Nome;
            jogoCadastro.Produtora = jogo.Produtora;
            jogoCadastro.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(jogoCadastro);
        }

        public async Task Atualizar(Guid idJogo, double Preco)
        {
            var jogoCadastro = await _jogoRepository.Listar(idJogo);

            if (jogoCadastro == null)
            {
                throw new JogoNaoCadastradoException();
            }

            
            jogoCadastro.Preco = Preco;

            await _jogoRepository.Atualizar(jogoCadastro);
        }

        public async Task DeletarJogo(Guid idJogo)
        {
            var jogoCadastro = await _jogoRepository.Listar(idJogo);

            if (jogoCadastro == null)
            {
                throw new JogoNaoCadastradoException();
            }

            
            await _jogoRepository.Remover(idJogo);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }

        public async Task<int> Total()
        {
            return await _jogoRepository.Total();
        }
    }
}
