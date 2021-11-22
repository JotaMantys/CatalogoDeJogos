using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Exception
{
    public class JogoNaoCadastradoException : System.Exception
    {

        public JogoNaoCadastradoException() : base("Este jogo não está cadastrado")
        {     }
    }
}
