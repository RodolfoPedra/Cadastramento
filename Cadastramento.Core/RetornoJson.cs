namespace Cadastramento.Core
{
    public class RetornoJson
    {
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public object[] ParametrosAdicionais { get; set; }
        public object[] Erros { get; set; }
    }
}
