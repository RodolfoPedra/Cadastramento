using Cadastramento.ModelData.Logic.Cadastramento;

namespace Cadastramento.Service.Logic.Cadastramento
{
    public sealed class usuarioService : BaseService<usuario>, BaseService<usuario>.IService
    {
        /// <summary>
        /// Método para buscar o usuário pelo login informado
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public usuario ObterPorLogin(string login)
        {
            return repositorioGenerico.ObterPrimeiro(ent => ent.login.Equals(login));
        }
    }
}
