using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

namespace Cadastramento.Util
{
    public class Funcoes
    {
        public static int DigitoModulo11(long intNumero)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };
            string strText = intNumero.ToString();
            if (strText.Length > 16)
                throw new Exception("Número não suportado pela função!");
            int intSoma = 0;
            int intIdx = 0;
            for (int intPos = strText.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(strText[intPos].ToString()) * intPesos[intIdx];
                intIdx++;
            }
            int intResto = (intSoma * 10) % 11;
            int intDigito = intResto;
            if (intDigito >= 10)
                intDigito = 0;
            return intDigito;
        }
        public static string getMD5Hash(string input)
        {

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static string SenhaHash()

        {
            string dataAtual = DateTime.Now.ToString("yyyyMMdd");
            string token = "zEpUrUPriChofL74xead" + dataAtual;
            UTF8Encoding encoder = new UTF8Encoding();
            SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(token));

            return byteArrayToString(hashedDataBytes);
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("x2"));
            }
            return output.ToString();
        }

        public static bool ValidarCnpj(string cnpj)
        {

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            cnpj = cnpj.Trim();

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)

                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }

        public static string mesextenso(string mes, bool abreviado, bool maiusculo)
        {
            string mesdescrito = string.Empty;
            switch (mes)
            {
                case "1":
                    {
                        mesdescrito = "Janeiro";
                        break;
                    }

                case "2":
                    {
                        mesdescrito = "Fevereiro";
                        break;
                    }
                case "3":
                    {
                        mesdescrito = "Março";
                        break;
                    }
                case "4":
                    {
                        mesdescrito = "Abril";
                        break;
                    }
                case "5":
                    {
                        mesdescrito = "Maio";
                        break;
                    }

                case "6":
                    {
                        mesdescrito = "Junho";
                        break;
                    }
                case "7":
                    {
                        mesdescrito = "Julho";
                        break;
                    }

                case "8":
                    {
                        mesdescrito = "Agosto";
                        break;
                    }

                case "9":
                    {
                        mesdescrito = "Setembro";
                        break;
                    }
                case "10":
                    {
                        mesdescrito = "Outubro";
                        break;
                    }
                case "11":
                    {
                        mesdescrito = "Novembro";
                        break;
                    }
                case "12":
                    {
                        mesdescrito = "Dezembro";
                        break;
                    }
                default:
                    break;
            }

            if (maiusculo)
                mesdescrito = mesdescrito.ToUpper();

            if (abreviado)
                mesdescrito = mesdescrito.Substring(0, 3);

            return mesdescrito;

        }

        public static Int32 mesnumero(string mes)
        {
            Int32 mesnumero = 0;
            mes = mes.ToLower();

            switch (mes)
            {
                case "janeiro":
                    {
                        mesnumero = 1;
                        break;
                    }

                case "fevereiro":
                    {
                        mesnumero = 2;
                        break;
                    }
                case "março":
                    {
                        mesnumero = 3;
                        break;
                    }
                case "abril":
                    {
                        mesnumero = 4;
                        break;
                    }
                case "maio":
                    {
                        mesnumero = 5;
                        break;
                    }

                case "junho":
                    {
                        mesnumero = 6;
                        break;
                    }
                case "julho":
                    {
                        mesnumero = 7;
                        break;
                    }

                case "agosto":
                    {
                        mesnumero = 8;
                        break;
                    }

                case "setembro":
                    {
                        mesnumero = 9;
                        break;
                    }
                case "outubro":
                    {
                        mesnumero = 10;
                        break;
                    }
                case "novembro":
                    {
                        mesnumero = 11;
                        break;
                    }
                case "dezembro":
                    {
                        mesnumero = 12;
                        break;
                    }
                default:
                    break;
            }

            return mesnumero;
        }

        public static bool ValidarCpf(string sNumero)
        {
            try
            {
                string tempCpfCnpj;
                string digito;
                int soma;
                int resto;
                int[] multiplicador1;
                int[] multiplicador2;

                sNumero = sNumero.apenasNumeros();

                #region Validar CPF
                multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                sNumero = sNumero.PadLeft(14, '0');

                if (sNumero.Substring(3, 11).Equals("00000000000") || sNumero.Substring(3, 11).Equals("11111111111") ||
                        sNumero.Substring(3, 11).Equals("22222222222") || sNumero.Substring(3, 11).Equals("33333333333") ||
                        sNumero.Substring(3, 11).Equals("44444444444") || sNumero.Substring(3, 11).Equals("55555555555") ||
                        sNumero.Substring(3, 11).Equals("66666666666") || sNumero.Substring(3, 11).Equals("77777777777") ||
                        sNumero.Substring(3, 11).Equals("88888888888") || sNumero.Substring(3, 11).Equals("99999999999"))
                    return false;


                tempCpfCnpj = sNumero.Substring(3, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador1[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();
                tempCpfCnpj = tempCpfCnpj + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();
                if (sNumero.EndsWith(digito))
                    return true;
                return false;
                #endregion
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static bool validarCpfCnpj(string sNumero)
        {
            try
            {
                string tempCpfCnpj;
                string digito;
                int soma;
                int resto;
                int[] multiplicador1;
                int[] multiplicador2;

                sNumero = sNumero.apenasNumeros();

                #region Validar CPF
                multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                sNumero = sNumero.PadLeft(14, '0');

                if (sNumero.Substring(3, 11).Equals("00000000000") || sNumero.Substring(3, 11).Equals("11111111111") ||
                        sNumero.Substring(3, 11).Equals("22222222222") || sNumero.Substring(3, 11).Equals("33333333333") ||
                        sNumero.Substring(3, 11).Equals("44444444444") || sNumero.Substring(3, 11).Equals("55555555555") ||
                        sNumero.Substring(3, 11).Equals("66666666666") || sNumero.Substring(3, 11).Equals("77777777777") ||
                        sNumero.Substring(3, 11).Equals("88888888888") || sNumero.Substring(3, 11).Equals("99999999999"))
                    return false;


                tempCpfCnpj = sNumero.Substring(3, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador1[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();
                tempCpfCnpj = tempCpfCnpj + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();
                if (sNumero.EndsWith(digito))
                    return true;
                #endregion

                #region Validar CNPJ
                multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                if (sNumero.Length != 14)
                    return false;

                if (sNumero.Substring(0, 8).Equals("00000000") || sNumero.Substring(0, 8).Equals("11111111") ||
                    sNumero.Substring(0, 8).Equals("22222222") || sNumero.Substring(0, 8).Equals("33333333") ||
                    sNumero.Substring(0, 8).Equals("44444444") || sNumero.Substring(0, 8).Equals("55555555") ||
                    sNumero.Substring(0, 8).Equals("66666666") || sNumero.Substring(0, 8).Equals("77777777") ||
                    sNumero.Substring(0, 8).Equals("88888888") || sNumero.Substring(0, 8).Equals("99999999"))
                    return false;

                tempCpfCnpj = sNumero.Substring(0, 12);
                soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador1[i];

                resto = (soma % 11);

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();
                tempCpfCnpj = tempCpfCnpj + digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return sNumero.EndsWith(digito);

                #endregion
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static int retornaDiferencaMeses(DateTime dmenor, DateTime dmaior)
        {
            if (dmenor > dmaior)
            {
                DateTime daux = dmaior;
                dmaior = dmenor;
                dmenor = daux;
            }

            int mesesmenor = (dmenor.Year * 12) + dmenor.Month;
            int mesesmaior = (dmaior.Year * 12) + dmaior.Month;

            return mesesmaior - mesesmenor;

        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string FormatarStrToMoney(string valor)
        {
            return "R$ " + valor.Substring(0, valor.Length - 2).StrToDecimal().DecimalToStr() + "," + valor.Substring(valor.Length - 2, 2);
        }

        //Função capta os erros da modelState e cria uma lista de chave e valor para mandar via Json
        //public static List<KeyValuePair<string, string>> ExtrairErrosModelState(ModelStateDictionary modelState)
        //{
        //    int count = 0;
        //    return modelState.Values
        //        .SelectMany(x => x.Errors)
        //        .Select(
        //        modelError =>
        //            new KeyValuePair<string, string>(
        //                count++.ToString(), modelError.ErrorMessage
        //                )).ToList();
        //}

        //Função que já renderiza uma lista em html com os Erros. 
        //public static string GerarAlertaDeErrosDaModelState(ModelStateDictionary modelState)
        //{
        //    var listaDeErros = ExtrairErrosModelState(modelState);
        //    var sb = new StringBuilder();
        //    sb.Append("<ul>");
        //    foreach (var keyValuePair in listaDeErros)
        //    {
        //        sb.Append("<li>" + keyValuePair.Value + "</li>");
        //    }
        //    sb.Append("</ul>");

        //    return sb.ToString();
        //}

        //Função que já gera uma lista com os Erros. 
        //public static string GerarListaDeErrosDaModelState(ModelStateDictionary modelState)
        //{
        //    var listaDeErros = ExtrairErrosModelState(modelState);
        //    var sb = new StringBuilder();
        //    foreach (var keyValuePair in listaDeErros)
        //    {
        //        sb.Append(Environment.NewLine + "- " + keyValuePair.Value);
        //    }
        //    return sb.ToString();
        //}

        //public static void AdicionarErrosModelState(Dictionary<string, string> listaErros, ModelStateDictionary ModelState)
        //{
        //    foreach (var listaErro in listaErros)
        //    {
        //        ModelState.AddModelError(listaErro.Key, listaErro.Value);
        //    }
        //}

        //public static void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        //{
        //    //options
        //    object matchCase = false;
        //    object matchWholeWord = true;
        //    object matchWildCards = false;
        //    object matchSoundsLike = false;
        //    object matchAllWordForms = false;
        //    object forward = true;
        //    object format = false;
        //    object matchKashida = false;
        //    object matchDiacritics = false;
        //    object matchAlefHamza = false;
        //    object matchControl = false;
        //    object replace = 2;
        //    object wrap = 1;

        //    //execute find and replace
        //    doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
        //        ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
        //        ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        //}

        /// <summary>
        /// Função criada para validar se a String informada é um e-mail válido.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>True(se válido) ou False</returns>
        public static bool ValidaEmail(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return true;
            var regEx = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", RegexOptions.IgnoreCase);
            return regEx.IsMatch(valor);
        }

        //public static string ExtrairConteudoPDF(string caminhoArquivo)
        //{
        //    try
        //    {
        //        // Create Bytescout.PDFExtractor.TextExtractor instance
        //        Bytescout.PDFExtractor.TextExtractor extractor = new Bytescout.PDFExtractor.TextExtractor();
        //        extractor.RegistrationName = "demo";
        //        extractor.RegistrationKey = "demo";

        //        // Load sample PDF document
        //        extractor.LoadDocumentFromFile(caminhoArquivo);

        //        // Save extracted text to file
        //        var serverpath = System.Configuration.ConfigurationManager.AppSettings["CaminhoDocumentosPadrao"];
        //        var ticks = DateTime.Now.Ticks.ToString();
        //        extractor.SaveTextToFile(serverpath + "output_" + ticks + ".txt");
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(serverpath + "output_" + ticks + ".txt"))
        //        {
        //            var line = sr.ReadToEnd();
        //            return line;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return "ERRO AO TENTAR EXTRAIR CONTEÚDO DO ARQUIVO.";
        //    }
        //}

        /// <summary>
        /// Método que retorna a data por extenso
        /// Ex: 25 de maio de 2015
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RetornaDataExtenso(DateTime data)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            int dia = data.Day;
            int ano = data.Year;
            string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(data.Month)).ToLower();
            string retorno = dia + " de " + mes + " de " + ano;
            return retorno;
        }

        ///// <summary>
        ///// Redimensionar imagem
        ///// </summary>
        ///// <param name="imgUpload"></param>
        //public static byte[] RedimensionarImagem(byte[] imgUpload, string larguraNova)
        //{
        //    // Recupera os parâmetros passados pela página
        //    string servidor = @"C:\Temp\";
        //    string nomeArquivo = "imageTemp";
        //    string extensao = ".jpg";
        //    string strSrcImagemOriginal = $@"{servidor}{nomeArquivo}{extensao}";

        //    File.WriteAllBytes(strSrcImagemOriginal, imgUpload);

        //    string strAlturaImagemRedimensionar = "";
        //    string strLarguraImagemRedimensionar = larguraNova;

        //    // Cria temporariamnte a imagem
        //    System.Drawing.Image imagemTemp = System.Drawing.Image.FromFile(strSrcImagemOriginal);

        //    // Variáveis contendo o tamanho
        //    int thumbHeight;
        //    int thumbWidth;

        //    // Redimensiona a largura de forma proporcional

        //    thumbWidth = int.Parse(strLarguraImagemRedimensionar);
        //    thumbHeight = (int)(thumbWidth * imagemTemp.Height) / imagemTemp.Width;


        //    imagemTemp.Dispose();

        //    // Envia para a memória o objeto a ser trabalhado bem como o novo objeto
        //    Stream objStream = new StreamReader(strSrcImagemOriginal).BaseStream;
        //    BinaryReader objBinaryReader = new BinaryReader(objStream);
        //    int i = (int)objStream.Length;
        //    byte[] arrBytes = objBinaryReader.ReadBytes(i);
        //    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(arrBytes);
        //    System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
        //    System.Drawing.Image thumbnail = new Bitmap(thumbWidth, thumbHeight);
        //    System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(thumbnail);

        //    // Melhoria da nova imagem
        //    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
        //    graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        //    graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
        //    graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;

        //    // Desenha a nova imagem
        //    graphic.DrawImage(image, 0, 0, thumbWidth, thumbHeight);

        //    // Aplica a codificação necessária
        //    System.Drawing.Imaging.ImageCodecInfo[] info = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        //    System.Drawing.Imaging.EncoderParameters encoderParameters;
        //    encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
        //    encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

        //    // Exibe a imagem em forma de JPG
        //    thumbnail.Save($"{servidor}imagem2Red{extensao}", info[1], encoderParameters);
        //    var retorno = File.ReadAllBytes($"{servidor}imagem2Red{extensao}");

        //    thumbnail.Dispose();

        //    imagemTemp = null;
        //    thumbnail = null;

        //    return retorno;
        //}

        public static DataTable ExecutaComandoSQL(string stringConexao, string sql)
        {
            SqlConnection connection = new SqlConnection(stringConexao);
            SqlDataAdapter adapter;
            SqlCommand command;
            DataTable table = new DataTable();

            try
            {
                command = new SqlCommand(sql, connection);
                connection.Open();

                adapter = new SqlDataAdapter(command);
                adapter.Fill(table);

                connection.Close();
                adapter.Dispose();

                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtém um range com as semanas de um determinado período passado como parametro
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static IEnumerable<WeekRange> GetWeekRange(DateTime dtStart, DateTime dtEnd)
        {
            DateTime fWeekStart, dt, fWeekEnd;
            int wkCnt = 1;

            if (dtStart.DayOfWeek != DayOfWeek.Sunday)
            {
                fWeekStart = dtStart.AddDays(7 - (int)dtStart.DayOfWeek);
                fWeekEnd = fWeekStart.AddDays(-1);
                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(dtStart, fWeekEnd, dtStart.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;

            }
            else
            {
                fWeekStart = dtStart;
            }
            
            for (dt = fWeekStart.AddDays(6); dt <= dtEnd; dt = dt.AddDays(7))
            {


                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(fWeekStart, dt, fWeekStart.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;
                fWeekStart = dt.AddDays(1);
            }

            if (dt > dtEnd)
            {
                IEnumerable<WeekRange> ranges = getMonthRange(new WeekRange(fWeekStart, dtEnd, dtEnd.Month, wkCnt++));
                foreach (WeekRange wr in ranges)
                {
                    yield return wr;
                }
                wkCnt = ranges.Last().WeekNo + 1;
            }
        }

        private static IEnumerable<WeekRange> getMonthRange(WeekRange weekRange)
        {
            List<WeekRange> ranges = new List<WeekRange>();

            if (weekRange.Start.Month != weekRange.End.Month)
            {
                DateTime lastDayOfMonth = new DateTime(weekRange.Start.Year, weekRange.Start.Month, 1).AddMonths(1).AddDays(-1);
                ranges.Add(new WeekRange(weekRange.Start, lastDayOfMonth, weekRange.Start.Month, weekRange.WeekNo));
                ranges.Add(new WeekRange(lastDayOfMonth.AddDays(1), weekRange.End, weekRange.End.Month, weekRange.WeekNo + 1));

            }
            else
            {
                ranges.Add(weekRange);
            }

            return ranges;
        }
    }

    public struct WeekRange
    {
        public DateTime Start;
        public DateTime End;
        public int MM;
        public int WeekNo;

        public WeekRange(DateTime _start, DateTime _end, int _mm, int _weekNo)
        {
            Start = _start;
            End = _end;
            MM = _mm;
            WeekNo = _weekNo;
        }
    }
}