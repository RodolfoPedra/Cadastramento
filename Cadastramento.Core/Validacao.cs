namespace Cadastramento.Core
{
    public class Validacao
    {
        public Validacao() { }

        /// <summary>
        /// Validações de campo
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="mensagem"></param>
        public Validacao(string campo, string mensagem)
        {
            this.Campo = campo;
            this.Mensagem = mensagem;
        }

        /// <summary>
        /// Validações diversas
        /// </summary>
        /// <param name="mensagem"></param>
        public Validacao(string mensagem)
        {
            this.Mensagem = mensagem;
        }

        /// <summary>
        /// ID do campo para colocar na ModelState
        /// </summary>
        public string Campo { get; set; }

        /// <summary>
        /// Mensagem de validação
        /// </summary>
        public string Mensagem { get; set; }
    }
}
