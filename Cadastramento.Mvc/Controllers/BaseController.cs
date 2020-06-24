using System.Collections.Generic;
using System.Web.Mvc;
using Cadastramento.Core;
using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Service;
using Cadastramento.Util;
using System.Linq;

namespace Cadastramento.Mvc.Controllers
{
    public class BaseController : Controller
    {
        public void EnviarMensagem(string texto, TipoMensagem tipo)
        {
            Session["Mensagem"] = new Mensagem { texto = new MvcHtmlString(texto).ToHtmlString(), tipo = tipo };
        }

        public SelectList GerarListaSituacao(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            var listaSituacao = new Dictionary<string, string>();
            listaSituacao.Add("A", "ATIVO");
            listaSituacao.Add("I", "INATIVO");

            return listaSituacao.ToSelectList(ent => ent.Key, ent => ent.Value, nomePrimeiroCampo, valorSelecionado);
        }

        public SelectList ListaMonitoramento(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            var listaMonitoramente = new Dictionary<string, string>();
            listaMonitoramente.Add("S", "SIM");
            listaMonitoramente.Add("N", "NÃO");

            return listaMonitoramente.ToSelectList(ent => ent.Key, ent => ent.Value, nomePrimeiroCampo, valorSelecionado);
        }

        public SelectList ListaSituacao(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {

            return new BaseService<situacaocadastro>().ObterTodos()
                .OrderBy(ent => ent.situacaocadastroid)
                .ToSelectList(ent => ent.situacaocadastroid, ent => ent.descricao, nomePrimeiroCampo, valorSelecionado);            
        }

        public SelectList TipoVeiculo(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            return new BaseService<tipoveiculo>().ObterTodos()
                .OrderBy(ent => ent.tipoveiculoid)
                .ToSelectList(ent => ent.tipoveiculoid, ent => ent.descricao, nomePrimeiroCampo, valorSelecionado);
        }

        public SelectList TipoCarga(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            return new BaseService<tipocarga>().ObterTodos()
                .OrderBy(ent => ent.tipocargaid)
                .ToSelectList(ent => ent.tipocargaid, ent => ent.descricao, nomePrimeiroCampo, valorSelecionado);
        }

        public SelectList GerarListaTodosMunicipios(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            return new BaseService<municipio>().ObterTodos()
                .OrderBy(ent => ent.uf).ThenBy(ent => ent.nome)
                .ToSelectList(ent => ent.municipioid, ent => ent.nome + " - " + ent.uf, nomePrimeiroCampo, valorSelecionado);
        }

        public SelectList GerarListaUfs(object valorSelecionado = null, string nomePrimeiroCampo = "SELECIONE...")
        {
            return new BaseService<municipio>().ObterTodos()
                .Select(ent => ent.uf).Distinct()
                .OrderBy(ent => ent)
                .ToSelectList(ent => ent, ent => ent, nomePrimeiroCampo, valorSelecionado);
        }
    }
}