using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cadastramento.Service.Logic.Cadastramento
{
    public class MotoristaDetran
    {
        public string cnh { get; set; }
        public string categoriacnh { get; set; }
        public string dataemissaocnh { get; set; }
        public string datavalidadecnh { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string rguf { get; set; }
        public string rgorgaoexpedidor { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public int situacaocadastroid { get; set; }

    }

    public class VeiculoDetran
    {
        public string placa { get; set; }
        public string renavam { get; set; }
        public string chassi { get; set; }
        public string marca { get; set; }
        public string cor { get; set; }
        public string combustivel { get; set; }
        public string tipo { get; set; }
        public string especie { get; set; }
        public string categoria { get; set; }
        public string anofabricacao { get; set; }
        public string anomodelo { get; set; }
        public string capacidadepassageiro { get; set; }
        public string nomeproprietariotransportadora { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string cep { get; set; }
        public string uf { get; set; }
        public int situacaocadastroid { get; set; }



    }

    public sealed class detranService
    {
        public MotoristaDetran GetDadosCNH(string cpf, string registro)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consCond/consCond.asp?";
                var URI = url + "cpf=" + cpf + "&registro=" + registro + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = client.GetAsync(URI).Result;
                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(JsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");

                    if (doc.InnerText.Contains("NAO ENCONTRADO") || doc.InnerText == "")
                    {
                        return null;
                    }

                    var motorista = new MotoristaDetran();
                    motorista.cnh = node[0]["registro"].InnerText.Trim();
                    motorista.categoriacnh = node[0]["categHab"].InnerText.Trim();
                    motorista.dataemissaocnh =  DateTime.ParseExact(node[0]["dtEmissao"].InnerText, "yyyyMMdd", new CultureInfo("pt-BR")).ToString("dd/MM/yyyy");
                    motorista.datavalidadecnh = DateTime.ParseExact(node[0]["dtVal"].InnerText, "yyyyMMdd", new CultureInfo("pt-BR")).ToString("dd/MM/yyyy");
                    motorista.nome = node[0]["nome"].InnerText.Trim();
                    motorista.cpf = node[0]["cpf"].InnerText.Trim();
                    motorista.rg = node[0]["rgNum"].InnerText.Trim();
                    motorista.rguf = node[0]["rgUf"].InnerText.Trim();
                    motorista.rgorgaoexpedidor = node[0]["rgOrgExp"].InnerText.Trim();
                    motorista.telefone = node[0]["telefone"].InnerText.Trim();
                    motorista.email = node[0]["email"].InnerText.Trim();
                    motorista.cep = node[0]["endCep"].InnerText.Trim();
                    motorista.logradouro = node[0]["endRua"].InnerText.Trim();
                    motorista.bairro = node[0]["endBairro"].InnerText.Trim();
                    motorista.cidade = node[0]["endMunicipio"].InnerText.Trim();
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

        public VeiculoDetran GetDadosVeiculo(string placa, string renavam)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consVei/consVei.asp?";
                var URI = url + "placa=" + placa + "&renavam=" + renavam + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = client.GetAsync(URI).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(JsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");

                    if (doc.InnerText.Contains("NAO ENCONTRADO") || doc.InnerText == "")
                    {
                        return null;
                    }

                    var veiculo = new VeiculoDetran();

                    veiculo.placa = node[0]["placa"].InnerText.Trim();
                    veiculo.renavam = node[0]["renavam"].InnerText.Trim();
                    veiculo.chassi = node[0]["chassi"].InnerText.Trim();
                    veiculo.marca = node[0]["marca"].InnerText.Trim();
                    veiculo.cor = node[0]["cor"].InnerText.Trim();
                    veiculo.combustivel = node[0]["combustivel"].InnerText.Trim();
                    veiculo.tipo = node[0]["tipo"].InnerText.Trim();
                    veiculo.especie = node[0]["especie"].InnerText.Trim();
                    veiculo.categoria = node[0]["categoria"].InnerText.Trim();
                    veiculo.anofabricacao = node[0]["anoFab"].InnerText.Trim();
                    veiculo.anomodelo = node[0]["anoMod"].InnerText.Trim();
                    veiculo.capacidadepassageiro = node[0]["capPas"].InnerText.Trim();
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
