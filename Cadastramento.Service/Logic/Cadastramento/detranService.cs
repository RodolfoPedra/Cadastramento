using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cadastramento.Service.Logic.Cadastramento
{
    public sealed class detranService
    {


        public motorista GetDadosCNH(string cpf, string registro)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consCond/consCond.asp?";
                var URI = url + "cpf=" + cpf + "&registro=" + registro + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = client.GetAsync(URI).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ProdutoJsonString = response.Content.ReadAsStringAsync().Result;

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(ProdutoJsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");

                    if (doc.InnerText.Contains("NAO ENCONTRADO"))
                    {
                        return null;
                    }

                    string dataemissao = node[0]["dtEmissao"].InnerText;
                    string datavalidadecnh = node[0]["dtVal"].InnerText;
                    var motorista = new motorista();
                    motorista.cnh = node[0]["registro"].InnerText.Trim();
                    motorista.categoriacnh = node[0]["categHab"].InnerText.Trim();
                    //motorista.dataemissaocnh = DateTime.ParseExact(dataemissao, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR"));  
                    //motorista.datavalidadecnh = DateTime.ParseExact(datavalidadecnh, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR"));
                    motorista.nome = node[0]["nome"].InnerText.Trim();
                    motorista.cpf = node[0]["cpf"].InnerText.Trim();
                    motorista.rg = node[0]["rgNum"].InnerText.Trim();
                    motorista.telefone = node[0]["telefone"].InnerText.Trim();
                    motorista.email = node[0]["email"].InnerText.Trim();
                    motorista.cep = node[0]["endCep"].InnerText.Trim();
                    motorista.logradouro = node[0]["endRua"].InnerText.Trim();
                    motorista.bairro = node[0]["endBairro"].InnerText.Trim();
                    motorista.uf = node[0]["endUf"].InnerText.Trim();
                    motorista.situacaocadastroid = 2;
                    return motorista;

                }
                else
                {
                    return null;
                }
            }

        }

        public veiculo GetDadosVeiculo(string placa, string renavam)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consVei/consVei.asp?";
                var URI = url + "placa=" + placa + "&renavam=" + renavam + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = client.GetAsync(URI).Result;

                if (response.IsSuccessStatusCode)
                {
                    var ProdutoJsonString = response.Content.ReadAsStringAsync().Result;

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(ProdutoJsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");
                    var veiculo = new veiculo();


                    veiculo.placa = node[0]["placa"].InnerText.Trim();
                    veiculo.renavam = node[0]["renavam"].InnerText.Trim();
                    veiculo.chassi = node[0]["chassi"].InnerText.Trim();
                    veiculo.marca = node[0]["marca"].InnerText.Trim();
                    veiculo.corpredominante = node[0]["cor"].InnerText.Trim();
                    veiculo.anofabricacao = node[0]["anoFab"].InnerText.Trim();
                    veiculo.anomodelo = node[0]["anoMod"].InnerText.Trim();
                    veiculo.nomeproprietariotransportadora = node[0]["nome"].InnerText.Trim();
                    veiculo.logradouro = node[0]["endRua"].InnerText.Trim();
                    veiculo.complemento = node[0]["endCompl"].InnerText.Trim();
                    veiculo.bairro = node[0]["endBairro"].InnerText.Trim();
                    veiculo.cidade = node[0]["codMun"].InnerText.Trim();
                    veiculo.cep = node[0]["endCep"].InnerText.Trim();
                    veiculo.uf = node[0]["endUf"].InnerText.Trim();
                    veiculo.situacaocadastroid = 2;

                    return veiculo;

                }
                else
                {
                    return null;
                }
            }

        }


    }
}
