using System.Collections.Generic;
using Cadastramento.ModelData.Logic.Cadastramento;

namespace Cadastramento.Mvc.Models
{
    public class SelecionaPerfilDTO
    {
        public List<perfil> perfis { get; set; }
        public string ReturnUrl { get; set; }
    }
}