using System;

namespace Cadastramento.Core
{
    public class Excecao : Exception
    {
        public Excecao() : base()
        { }

        public Excecao(string message) : base(message)
        { }
    }
}
