using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Service.Logic.Cadastramento;
using Cadastramento.Util;
using System.Linq;
using System.Web;

namespace Cadastramento.Mvc.Models
{
    public class SessaoUsuario
    {
        public static perfil Perfil
        {
            get
            {
                if (HttpContext.Current.Session["Perfil"] == null)
                {
                    HttpContext.Current.Session["Perfil"] = new perfil();
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var name = HttpContext.Current.User.Identity.Name.Split('_');
                        DefinirPerfilUsuarioTemp(name.Last().StrToInt32());
                    }
                }
                return (perfil)HttpContext.Current.Session["Perfil"];
            }
        }

        public static UsuarioSessao Sessao
        {
            get
            {
                if (HttpContext.Current.Session["Usuario"] == null)
                {
                    HttpContext.Current.Session["Usuario"] = new UsuarioSessao();
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        var name = HttpContext.Current.User.Identity.Name.Split('_');
                        DefinirUsuarioTemp(name.First());
                    }
                }
                return (UsuarioSessao)HttpContext.Current.Session["Usuario"];
            }
        }

        public static void DefinirUsuarioTemp(string NomeLogin)
        {
            usuario usuario = new usuarioService().ObterPorLogin(NomeLogin);
            Sessao.usuarioid = usuario.usuarioid;
            Sessao.login = usuario.login;
            Sessao.senha = usuario.senha;
            Sessao.nome = usuario.nome;
            Sessao.email = usuario.email;
            Sessao.situacao = usuario.situacao;
        }

        public static void DefinirPerfilUsuarioTemp(int perfilid)
        {
            var perfil = new perfilService().Obter(perfilid);
            HttpContext.Current.Session["Perfil"] = perfil;
        }

        public static void SessaoLimpar()
        {
            HttpContext.Current.Session["Perfil"] = null;
            HttpContext.Current.Session["Usuario"] = null;
        }
    }
}