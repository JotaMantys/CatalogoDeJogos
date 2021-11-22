using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiCatalogoJogos.Exception
{
    public class JogoJaCadastradoException : System.Exception
    {
        public JogoJaCadastradoException(): base("Este jogo Já esta cadastrado")
        {   }
    }
}
