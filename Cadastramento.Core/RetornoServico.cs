using System.Collections.Generic;
using System.Linq;

namespace Cadastramento.Core
{
    public class RetornoServico
    {
        public RetornoServico()
        {
            this.Erros = new List<Validacao>();
        }

        public List<Validacao> Erros { get; set; }

        public bool Sucesso
        {
            get { return !Erros.Any(); }
        }

        public void IncluirErro(Validacao erro)
        {
            if (this.Erros.All(ent => ent.Campo != erro.Campo))
                Erros.Add(erro);
        }

        public void IncluirErros(IEnumerable<Validacao> erros)
        {
            foreach (var erro in erros)
            {
                this.IncluirErro(erro);
            }
        }

        public void LimparErros()
        {
            this.Erros = new List<Validacao>();
        }
    }
}
