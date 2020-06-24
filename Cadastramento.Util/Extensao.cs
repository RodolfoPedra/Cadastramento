using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cadastramento.Util
{
    public static class Extensao
    {
        
        public static Dictionary<String, String> ToDictionaryFormat<T>(this IEnumerable<T> enumerable, Func<T, object> valor, Func<T, object> texto, Boolean selecione = false)
        {
            var items = enumerable.ToList();
            Dictionary<string, string> retorno = items.Select(x => new
            {
                Valor = valor.Invoke(x),
                Texto = texto.Invoke(x)
            }).ToDictionary(x => x.Valor.ToString(), x => x.Texto.ToString());

            var selecionar = new Dictionary<string, string>();
            if (selecione)
                selecionar.Add("", "SELECIONE..");

            retorno = selecionar.Concat(retorno).ToDictionary(x => x.Key, x => x.Value);

            return retorno;
        }

        public static string RemoveAcentos(this string texto)
        {
            if (texto == null)
            {
                return null;
            }
            texto = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in texto.ToCharArray())
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string InserirCaracterPorIntervalo(this string texto, string caracter, int intervalo)
        {
            StringBuilder result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + intervalo < texto.Length)
            {
                result.Append(texto.Substring(currentPosition, intervalo)).Append(caracter);
                currentPosition += intervalo;
            }
            if (currentPosition < texto.Length)
            {
                result.Append(texto.Substring(currentPosition));
            }
            return result.ToString();
        }

        public static string Tratartelefone(this string telefone)
        {
            if (telefone.StartsWith("67"))
            {
                if (!telefone.StartsWith("673"))
                {
                    if (telefone.Length == 10)
                    {
                        telefone = telefone.Replace("67", "679");
                    }
                }
            }
            if (telefone.StartsWith("9"))
            {
                if (telefone.Length == 8)
                {
                    telefone = "9" + telefone;
                }
            }

            return telefone;
        }

        public static string apenasNumeros(this string toNormalize)
        {
            if (toNormalize == null)
            {
                return "";
            }

            List<char> numbers = new List<char>("0123456789");
            StringBuilder toReturn = new StringBuilder(toNormalize.Length);
            CharEnumerator enumerator = toNormalize.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (numbers.Contains(enumerator.Current))
                    toReturn.Append(enumerator.Current);
            }

            return toReturn.ToString();
        }

        public static String isNull(this decimal? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }
        }
        public static String isNull(this int? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }
        }
        public static String isNull(this DateTime? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }
        }
        public static String isNull(this string valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return null;
            }
            else
            {
                return valor.Trim().ToString();
            }
        }
        public static String isNull(this Object valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }
        }
        public static String isNullN2(this decimal valor)
        {
            return valor.ToString("###,##0.00");
        }

        /// <summary>
        /// Data para String dd/MM/yyyy
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static String DateToStr(this DateTime valor)
        {
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
        }
        public static String DateToStr(this DateTime? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return Convert.ToDateTime(valor).ToString("dd/MM/yyyy");
            }

        }
        public static int TotalMeses(this DateTime start, DateTime end)
        {
            return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
        }
        public static String DateTimeToStr(this DateTime valor)
        {
            return Convert.ToDateTime(valor).ToString("dd/MM/yyyy HH:mm");
        }
        public static String DateTimeToStr(this DateTime? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return Convert.ToDateTime(valor).ToString("dd/MM/yyyy HH:mm");
            }
        }
        public static String DateTimeSecToStr(this DateTime? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return Convert.ToDateTime(valor).ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
        public static String DateToStrConsultaBD(this DateTime valor)
        {
            string retorno = "";
            retorno += valor.Year + "-";
            retorno += valor.Month + "-";
            retorno += valor.Day;
            return retorno;
        }
        public static DateTime? StrToDateTimeNull(this String valor)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(valor))
                {
                    return null;
                }
                else
                {
                    if (valor.IndexOf("/") == -1)
                        return Convert.ToDateTime(valor.Substring(0, 2) + "/" + valor.Substring(2, 2) + "/" + valor.Substring(4));
                    else
                        return Convert.ToDateTime(valor);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DateTime StrToDateTime(this String valor)
        {
            if (valor.IndexOf("/") == -1 && valor.IndexOf("-") == -1)
            {
                return Convert.ToDateTime(valor.Substring(0, 2) + "/" + valor.Substring(2, 2) + "/" + valor.Substring(4));
            }
            else if (valor.IndexOf("-") != -1)
            {
                return new DateTime(valor.Substring(0, 4).StrToInt32(), valor.Substring(5, 2).StrToInt32(), valor.Substring(8, 2).StrToInt32());
            }
            else
                return Convert.ToDateTime(valor);
        }
        public static int StrToInt32(this String valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(valor);
            }
        }
        public static int? StrToInt32Null(this String valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(valor);
            }
        }

        public static int ObjToInt32(this Object valor)
        {
            if (valor == null)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(valor);
            }
        }

        public static string DecodeFromUtf8(this string valor)
        {
            string utf8_String = valor;
            byte[] bytes = Encoding.Default.GetBytes(utf8_String);
            utf8_String = Encoding.Default.GetString(bytes);

            return utf8_String;
        }

        public static Decimal? StrToDecimalNull(this String valor)
        {
            if (String.IsNullOrWhiteSpace(valor))
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(valor);
            }
        }
        public static Decimal StrToDecimal(this String valor)
        {
            return Convert.ToDecimal(valor);
        }
        
        public static String DecimalNullToStr(this Decimal? valor)
        {
            if (!valor.HasValue)
            {
                return String.Empty;
            }
            else
            {
                return valor.Value.ToString("###,##0.00");
            }
        }
        
        /// <summary>
        /// Função que converte de Decimal para String
        /// </summary>
        /// <param name="valor">Valor Decimal</param>
        /// <returns>String com 2 casas decimais</returns>
        public static String DecimalToStr(this Decimal valor)
        {
            return valor.ToString("###,##0.##");
        }

        /// <summary>
        /// Função que converte de Decimal para String
        /// </summary>
        /// <param name="valor">Valor Decimal</param>
        /// <returns>String com 2 casas decimais</returns>
        public static String DecimalToStrMoney(this Decimal valor)
        {
            return valor.ToString("C", CultureInfo.CurrentCulture);
        }

        public static DateTime? IntToDateTimeNull(this String value)
        {
            try
            {
                string sData = Convert.ToInt32(value).ToString();
                sData = sData.ToString().Substring(6, 2) + "/" + sData.ToString().Substring(4, 2) + "/" + sData.ToString().Substring(0, 4);
                return Convert.ToDateTime(sData);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Remove as máscaras aplicadas pela vierw.
        /// Preparada para remover os caracteres "()_./-", atendendo assim a remoção
        /// de máscara de CNPJ, CPF, Telefone e possíveis campos vazios na máscara,
        /// que irão gerar "_".
        /// </summary>
        /// <param name="texto">Valor string com a máscara ou nulo</param>
        /// <returns>String sem máscara ou null caso a string seja null</returns>
        public static string RemoveMascara(this string texto)
        {
            return texto == null ? null : (Regex.Replace(texto, "[?\\)?\\(_./-]", "")).Replace(" ","");
        }

        /// <summary>
        /// O substring padrão retorna "" e lança exception para valores nulos.
        /// Reescrevi um proprio para que ele retorne null quando o valor for null, para não ficar
        /// fazendo ifs desnecessários no código e tambem porque se tratar o valor com "??" no banco
        /// deixa de ser nulo e é gravado um valor de string vazia
        /// </summary>
        /// <param name="texto">Texto pego pela extensão</param>
        /// <param name="startIndex">Inicio do trecho a ser considerada</param>
        /// <param name="length">Tamanho do trecho a ser considerada</param>
        /// <returns>Trecho da string solicitado pelos parâmetros INT ou null caso a string seja nula</returns>
        public static string CustomSubstring(this string texto, int startIndex)
        {
            return texto == null ? null : texto.Substring(startIndex);
        }

        public static string CustomSubstring(this string texto, int startIndex, int length)
        {
            if (texto == null)
            {
                return string.Empty;
            }
            else if (texto.Length > length)
            {
                return texto.Substring(startIndex, length);
            }
            else
            {
                return texto;
            }
        }

        public static String intNullToStr(this int? valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor.ToString();
            }

        }

        public static string validaCpfCnpj(this string valor)
        {
            try
            {
                if (Funcoes.validarCpfCnpj(valor))
                {
                    return valor;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string ValidaCpf(this string valor)
        {
            try
            {
                if (Funcoes.ValidarCpf(valor))
                {
                    return valor;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static decimal? StrGPToDecimal(this string valor)
        {
            try
            {
                return Convert.ToDecimal(valor) / 100;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Bom, o cálculo é simples, basta subtrair o ano atual – o ano de nascimento e caso o dia e mês de aniversário seja inferior ao dia e mês atual, subtraí 1.
        /// </summary>
        /// <param name="dataNascimento"></param>
        /// <returns>Idade em Anos</returns>
        public static int RetornaIdade(this DateTime? dataNascimento)
        {
            if (dataNascimento != null)
            {
                int anos = DateTime.Now.Year - dataNascimento.Value.Year;
                if (DateTime.Now.Month < dataNascimento.Value.Month || (DateTime.Now.Month == dataNascimento.Value.Month && DateTime.Now.Day < dataNascimento.Value.Day))
                    anos--;
                return anos;
            }
            return 0;
        }

        public static String FormataTelefone(this String valor)
        {
            if (String.IsNullOrEmpty(valor))
                return valor;
            
            switch (valor.Length)
            {
                case 10:
                    return String.Format("({0}) {1}-{2}", valor.Substring(0, 2), valor.Substring(2, 4), valor.Substring(6, 4));
                case 11:
                    return String.Format("({0}) {1}-{2}", valor.Substring(0, 2), valor.Substring(2, 5), valor.Substring(7, 4));
                default:
                    return valor;
            }
        }

        public static String FormataCep(this String valor)
        {
            if (String.IsNullOrEmpty(valor))
                return valor;

            return String.Format("{0}.{1}-{2}", valor.Substring(0, 2), valor.Substring(2, 3), valor.Substring(5, 3));
        }

        public static string FormataCpfCnpj(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return string.Empty;
            }
            else if (valor.Length == 11)
            {
                return FormataCpf(valor);
            }
            else if (valor.Length == 14)
            {
                return FormataCnpj(valor);
            }
            else
            {
                return valor;
            }
        }

        public static String FormataCpf(this String valor)
        {
            if (valor != null)
            {
                switch (valor.Length)
                {
                    case 11:
                        return String.Format("{0}.{1}.{2}-{3}", valor.Substring(0, 3), valor.Substring(3, 3)
                            , valor.Substring(6, 3), valor.Substring(9, 2));
                    default:
                        return valor;
                }
            }
            else
            {
                return "";
            }
        }

        public static String FormataCnpj(this String valor)
        {
            if (valor != null)
            {
                switch (valor.Length)
                {
                    case 14:
                        return String.Format("{0}.{1}.{2}/{3}-{4}", valor.Substring(0, 2), valor.Substring(2, 3)
                            , valor.Substring(5, 3), valor.Substring(8, 3), valor.Substring(11, 3));
                    default:
                        return valor;
                }
            }
            else
            {
                return "";
            }
        }

        public static string FormataIE(this string ie)
        {
            try
            {
                return string.Format("{0}.{1}.{2}-{3}", ie.Substring(0, 2), ie.Substring(2, 3), ie.Substring(5, 3), ie.Substring(8, 1));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DataTable ConvertToDataTable<T>(this List<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
        
            DataTable table = new DataTable();
            
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            
            return table;
        }

        public static string CapitalizarString(this string nome)
        {
            string[] excecoes = new string[] { "e", "de", "da", "das", "do", "dos" };
            var palavras = new Queue<string>();
            foreach (var palavra in nome.Split(' '))
            {
                if (!string.IsNullOrEmpty(palavra))
                {
                    var emMinusculo = palavra.ToLower();
                    var letras = emMinusculo.ToCharArray();
                    if (!excecoes.Contains(emMinusculo)) letras[0] = char.ToUpper(letras[0]);
                    palavras.Enqueue(new string(letras));
                }
            }
            return string.Join(" ", palavras);
        }

        public static String ToUpperNull(this string valor)
        {
            return valor?.ToUpper();
        }

        public static String ToLowerNull(this string valor)
        {
            return valor?.ToLower();
        }

        public static bool HasMethod(this object objectToCheck, string methodName)
        {
            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType.GetGenericArguments()[0]), null);
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        #region ToSelectList

        /// <summary>
        /// Extension Method que retorna uma SelectList para ser usada, por exemplo, no HtmlHelper DropDownList
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <param name="enumerable">Extension method</param>
        /// <param name="valor">Propriedade com o valor do respectivo option</param>
        /// <param name="texto">Propriedade com o nome do respectivo option</param>
        /// <returns>SelectList com o primeiro valor "Selecione…"</returns>
        public static SelectList ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, object> valor, Func<T, object> texto)
        {
            return ToSelectList(enumerable, valor, texto, "Selecione..");
        }

        /// <summary>
        /// Extension Method que retorna uma SelectList para ser usada, por exemplo, no HtmlHelper DropDownList
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <param name="enumerable">Extension method</param>
        /// <param name="valor">Propriedade com o valor do respectivo option</param>
        /// <param name="texto">Propriedade com o nome do respectivo option</param>
        /// <param name="nomePrimeiroCampo">Nome da primeira linha. Padrão "Selecione…"</param>
        /// <returns>SelectList com o primeiro valor selecionado</returns>
        public static SelectList ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, object> valor, Func<T, object> texto, string nomePrimeiroCampo)
        {
            return ToSelectList(enumerable, valor, texto, nomePrimeiroCampo, null);
        }

        /// <summary>
        /// Extension Method que retorna uma SelectList para ser usada, por exemplo, no HtmlHelper DropDownList
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <param name="enumerable">Extension method</param>
        /// <param name="valor">Propriedade com o valor do respectivo option</param>
        /// <param name="texto">Propriedade com o nome do respectivo option</param>
        /// <param name="nomePrimeiroCampo">Nome da primeira linha. Padrão "Selecione…"</param>
        /// <param name="valorSelecionado">Valor que vai ser selecionado por padrãos</param>
        /// <returns>SelectList</returns>
        public static SelectList ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, object> valor, Func<T, object> texto, string nomePrimeiroCampo, object valorSelecionado)
        {
            var list = enumerable.Select(x => new
            {
                Valor = valor.Invoke(x),
                Texto = texto.Invoke(x)
            }).ToList();
            if (nomePrimeiroCampo != null)
            {
                list.Insert(0, new { Valor = (object)null, Texto = (object)nomePrimeiroCampo });
            }

            return new SelectList(list, "Valor", "Texto", valorSelecionado);
        }

        #endregion
    }
}
