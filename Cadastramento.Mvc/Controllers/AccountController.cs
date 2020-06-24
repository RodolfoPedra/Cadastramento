using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Mvc.Models;
using Cadastramento.Service;
using Cadastramento.Service.Logic.Cadastramento;
using Cadastramento.Util;

namespace Cadastramento.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f, string ReturnUrl)
        {
            try
            {
                usuario u = new usuarioService().ObterPorLogin(f["username"]);
                if (u != null)
                {
                    if (u.situacao != "A")
                    {
                        EnviarMensagem("Usuário está inativo ou é inválido!", TipoMensagem.Vermelho);
                        return View();
                    }

                    if (Funcoes.getMD5Hash(f["password"]).ToUpper() == u.senha.ToUpper())
                    {
                        IncluiPessoaNaSessao(f["username"]);

                        return RedirectToAction("SelecionarPerfil", "Account", new { SessaoUsuario.Sessao.usuarioid, ReturnUrl });
                    }

                    EnviarMensagem("Usuário ou Senha Incorreto(s)!", TipoMensagem.Vermelho);
                }
                else
                {
                    EnviarMensagem("Usuário não encontrado!", TipoMensagem.Vermelho);
                }
            }
            catch (Exception)
            {
                EnviarMensagem("Ocorreu um erro. Tente mais tarde.", TipoMensagem.Vermelho);
            }

            return View();
        }

        private void IncluiPessoaNaSessao(string login)
        {
            FormsAuthentication.SetAuthCookie(login, false);

            SessaoUsuario.DefinirUsuarioTemp(login);

            var cidade = "Campo Grande";

            var ip = System.Web.HttpContext.Current.Request.UserHostAddress.Equals("::1")
                ? "127.0.0.1"
                : System.Web.HttpContext.Current.Request.UserHostAddress;

            var browser = Request.Browser;

            var servicoLogAcesso = new logacessoService();
            logacesso logAcesso = new logacesso
            {
                usuarioid = SessaoUsuario.Sessao.usuarioid,
                login = SessaoUsuario.Sessao.login,
                sistema = "Cadastramento",
                ip = ip,
                municipio = cidade,
                datahoralog = DateTime.Now,
                browser = browser.Browser + " " + browser.Version
            };
            servicoLogAcesso.Incluir(logAcesso);
            servicoLogAcesso.Salvar(SessaoUsuario.Sessao.login);
        }

        [HttpGet]
        public ActionResult SelecionarPerfil(int usuarioid, string ReturnUrl)
        {
            if (SessaoUsuario.Sessao != null)
                usuarioid = SessaoUsuario.Sessao.usuarioid;

            var usuario = new usuarioService().Obter(usuarioid);

            //Tratamento para escolher o único perfil automaticamente
            if (usuario.usuarioperfil.Count == 1)
            {
                int perfilid = usuario.usuarioperfil.First().perfilid;
                SessaoUsuario.DefinirPerfilUsuarioTemp(perfilid);
                FormsAuthentication.SetAuthCookie(usuario.login + "_" + perfilid, false);


                if (!String.IsNullOrEmpty(ReturnUrl))
                    return Redirect(ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            return View(new SelecionaPerfilDTO { perfis = usuario.usuarioperfil.Select(ent => ent.perfil).ToList(), ReturnUrl = ReturnUrl });
        }

        [HttpPost, ActionName("SelecionarPerfil")]
        public ActionResult SelecionarPerfilPost(int perfilid, string ReturnUrl)
        {
            FormsAuthentication.SetAuthCookie(SessaoUsuario.Sessao.login + "_" + perfilid, false);
            SessaoUsuario.DefinirPerfilUsuarioTemp(perfilid);

            if (!String.IsNullOrEmpty(ReturnUrl))
                return Redirect(ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ModalAlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AlterarSenha(string senhaAtual, string novaSenha, string novaSenhaConfirmacao)
        {
            try
            {
                var usuarioid = SessaoUsuario.Sessao.usuarioid;
                var srv = new BaseService<usuario>();

                var u = srv.Obter(usuarioid);

                senhaAtual = Funcoes.getMD5Hash(senhaAtual).ToUpper();
                if (u.senha != senhaAtual)
                    return Json(new { Mensagem = "SENHA_ATUAL" });

                if (!novaSenha.Equals(novaSenhaConfirmacao))
                    return Json(new { Mensagem = "NOVA_SENHA" });

                u.senha = Funcoes.getMD5Hash(novaSenha).ToUpper();

                srv.Alterar(u);
                srv.Salvar(SessaoUsuario.Sessao.login);

                return Json(new { Mensagem = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Mensagem = "ERRO", Erro = ex.Message });
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            SessaoUsuario.SessaoLimpar();
            return Redirect("~/Account/LogIn");
        }
    }
}