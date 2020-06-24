using Cadastramento.ModelData.Logic.Cadastramento;
using Cadastramento.Util;
using System;
using System.Collections.Generic;
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


        public async Task<motorista> GetDadosCNH(string cpf, string registro)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consCond/consCond.asp?";
                var URI = url + "cpf=" + cpf + "&registro=" + registro + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = await client.GetAsync(URI);
                if (response.IsSuccessStatusCode)
                {
                    var ProdutoJsonString = await response.Content.ReadAsStringAsync();

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(ProdutoJsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");
                    var motorista = new motorista
                    {
                        cnh = node[0]["registro"].InnerText.Trim(),
                        categoriacnh = node[0]["categHab"].InnerText.Trim(),
                        dataemissaocnh = Convert.ToDateTime(node[0]["dtEmissao"].InnerText.Trim()),
                        datavalidadecnh = Convert.ToDateTime(node[0]["dtVal"].InnerText.Trim()),
                        nome = node[0]["nome"].InnerText.Trim(),
                        cpf = node[0]["cpf"].InnerText.Trim(),
                        rg = node[0]["rgNum"].InnerText.Trim(),               
                        telefone = node[0]["telefone"].InnerText.Trim(),
                        email = node[0]["email"].InnerText.Trim(),
                        cep = node[0]["endCep"].InnerText.Trim(),
                        logradouro = node[0]["endRua"].InnerText.Trim(),
                        bairro = node[0]["endBairro"].InnerText.Trim(),
                        uf = node[0]["endUf"].InnerText.Trim()
                    };

                    return motorista;

                }
                else
                {
                    return null;
                }
            }

        }

        public async Task<veiculo> GetDadosVeiculo(string placa, string renavam)
        {

            using (var client = new HttpClient())
            {
                var url = "http://www2.detran.ms.gov.br/detranet/iagro/consVei/consVei.asp?";
                var URI = url + "placa=" + placa + "&renavam=" + renavam + "&token=" + Funcoes.SenhaHash();
                HttpResponseMessage response = await client.GetAsync(URI);

                if (response.IsSuccessStatusCode)
                {
                    var ProdutoJsonString = await response.Content.ReadAsStringAsync();

                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(ProdutoJsonString);

                    XmlNodeList node = doc.GetElementsByTagName("dados");
                    var veiculo = new veiculo
                    {
                        placa = node[0]["placa"].InnerText.Trim(),
                        renavam = node[0]["renavam"].InnerText.Trim(),
                        chassi = node[0]["chassi"].InnerText.Trim(),
                        marca = node[0]["marca"].InnerText.Trim(),
                        corpredominante = node[0]["cor"].InnerText.Trim(),    
                        anofabricacao = node[0]["anoFab"].InnerText.Trim(),
                        anomodelo = node[0]["anoMod"].InnerText.Trim(),                    
                        nomeproprietariotransportadora = node[0]["nome"].InnerText.Trim(),
                        logradouro = node[0]["endRua"].InnerText.Trim(),
                        complemento = node[0]["endCompl"].InnerText.Trim(),
                        bairro = node[0]["endBairro"].InnerText.Trim(),
                        cidade = node[0]["codMun"].InnerText.Trim(),
                        cep = node[0]["endCep"].InnerText.Trim(),
                        uf = node[0]["endUf"].InnerText.Trim()
                    };

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
