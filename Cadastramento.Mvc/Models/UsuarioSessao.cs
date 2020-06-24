namespace Cadastramento.Mvc.Models
{
    public class UsuarioSessao
    {
        public int usuarioid { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string situacao { get; set; }
    }
}