using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace Cadastramento.Util
{
    public class WebReportRenderer : IDisposable
    {
        #region Private fields
        //private string fullReportPath;
        private string downloadFileName;
        private Microsoft.Reporting.WebForms.LocalReport reportInstance;
        private MemoryStream reportMemoryStream;
        private string reportMimeType;
        private String deviceInfo = "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.2in</MarginLeft>" +
            "  <MarginRight>0.2in</MarginRight>" +
            "  <MarginBottom>0.2in</MarginBottom>" +
            "  <Language>pt-BR</Language>" +
            "</DeviceInfo>";

        private String encoding, extension;
        private string[] streamids;
        //private CustomReportCredentials credenciais;

        public enum Orientacao
        {
            Retrato = 0,
            Paisagem = 1
        }
        public enum Papel
        {
            Carta = 0,
            A4 = 1
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Classe para instanciar o relatorio e configurar o servidor.
        /// Exemplo: http://NomeDoServidor/reportserver
        /// </summary>
        /// <param name="reportPath"></param>
        /// <param name="downloadFileName"></param>
        /// 

        public WebReportRenderer(string reportPath, string downloadFileName)
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("Esta classe somente para aplicacoes WEB.");

            this.downloadFileName = downloadFileName;

            reportInstance = new Microsoft.Reporting.WebForms.LocalReport();
            reportInstance.ReportPath = HttpContext.Current.Server.MapPath(reportPath);
        }
        /// <summary>
        /// Por padrão, o relatório é gerado em modo retrato.
        /// </summary>
        /// <param name="reportPath"></param>
        /// <param name="downloadFileName"></param>
        /// <param name="orientacao"></param>
        public WebReportRenderer(string reportPath, string downloadFileName, Orientacao orientacao, string papel = "A4", string margensVerticais = null, string margensLaterais = null)
        {
            //if (!downloadFileName.EndsWith(".pdf"))
            //{
            //    downloadFileName = downloadFileName + "." + HttpContext.Current.Session["FormatoRelatorioTipoArquivoImpressao"].ToString();
            //}

            if (HttpContext.Current == null)
                throw new InvalidOperationException("Esta classe somente para aplicacoes WEB.");

            this.downloadFileName = downloadFileName;
            var altura = "";
            var largura = "";
            switch (papel)
            {
                case "A4":
                    altura = "29.7cm";
                    largura = "21cm";
                    break;
                case "Carta":
                    altura = "27.94cm";
                    largura = "21.59cm";
                    break;
            }
            deviceInfo = "<DeviceInfo>" +
                         "  <OutputFormat>PDF</OutputFormat>";
            if (orientacao == Orientacao.Paisagem)
                deviceInfo += " <PageWidth>" + altura + "</PageWidth>" +
                    " <PageHeight>" + largura + "</PageHeight>";
            else
                deviceInfo += " <PageWidth>" + largura + "</PageWidth>" +
                  " <PageHeight>" + altura + "</PageHeight>";
            if (!String.IsNullOrEmpty(margensVerticais))
                deviceInfo +=
                    "  <MarginBottom>" + "0.2cm" + "</MarginBottom>" +
                    "  <MarginTop>" + margensVerticais + "</MarginTop>";
            else
                deviceInfo +=
                    "  <MarginBottom>0.2in</MarginBottom>" +
                    "  <MarginTop>0.5in</MarginTop>";
            if (!String.IsNullOrEmpty(margensLaterais))
                deviceInfo +=
                    "  <MarginLeft> " + margensLaterais + "</MarginLeft>" +
                    "  <MarginRight>" + margensLaterais + "</MarginRight>";
            else
                deviceInfo +=
                    "  <MarginLeft>0.2in</MarginLeft>" +
                    "  <MarginRight>0.2in</MarginRight>";
            deviceInfo +=
            "  <Language>pt-BR</Language>" +
            "</DeviceInfo>";

            reportInstance = new Microsoft.Reporting.WebForms.LocalReport();
            reportInstance.ReportPath = HttpContext.Current.Server.MapPath(reportPath);
        }

        #endregion

        #region Properties
        /// <summary>        
        /// Retorna a instancia do relatorio
        /// </summary>        
        /// <value>A instancia do relatorio.</value>
        public Microsoft.Reporting.WebForms.LocalReport ReportInstance
        {
            get
            {
                return reportInstance;
            }
        }

        #endregion

        #region Public Methods
        /// <summary>              
        /// Renderiza o trlatorio atual para o browser do usuario como um download de PDF
        /// </summary>        
        /// <param name="pdfDeviceInfoSettings">The PDF device info settings (see http://msdn2.microsoft.com/en-us/library/ms154682.aspx).</param>        
        /// <returns></returns>
        public Warning[] RenderToBrowserPDF()
        {
            CreateStreamCallback callback = new Microsoft.Reporting.WebForms.CreateStreamCallback(CreateWebBrowserStream);
            Warning[] warnings;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + downloadFileName + "\"");

            byte[] bytes = reportInstance.Render("PDF", deviceInfo, out reportMimeType, out encoding, out extension, out streamids, out warnings);

            return warnings;
        }

        /// <summary>        
        /// Renderiza o relatorio atual para o browser do usuario como um download de PDF
        /// </summary>        
        /// <returns></returns>        


        /// <summary>        
        /// Renderiza a intancia do relatorio atual para um email com um arquivo anexado contendo o relatorio.
        /// </summary>        
        /// <param name="pdfDeviceInfoSettings">O device com as configurações de informação do PDF (veja http://msdn2.microsoft.com/en-us/library/ms154682.aspx).</param>        
        /// <param name="toAddress">Endereço Para.</param>        
        /// <param name="fromAddress">Endereço De.</param>        
        /// <param name="subject">O Assunto.</param>        
        /// <param name="body">O Corpoo.</param>        
        /// <returns></returns>        
        public Warning[] RenderToEmailPDF(string pdfDeviceInfoSettings, string toAddress, string fromAddress, string subject, string body)
        {
            CreateStreamCallback callback = CreateMemoryStream;
            Warning[] warnings = null;
            //reportInstance.Render("PDF", pdfDeviceInfoSettings, callback, out warnings); 
            reportMemoryStream.Seek(0, SeekOrigin.Begin);
            var client = new SmtpClient();
            using (var message = new MailMessage(fromAddress, toAddress, subject, body))
            using (var attachment = new Attachment(reportMemoryStream, reportMimeType))
            {
                attachment.Name = downloadFileName;
                message.Attachments.Add(attachment);
                client.Send(message);
            }
            return warnings;
        }
        public byte[] RenderToBytesPDF()
        {
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            return reportInstance.Render("PDF", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
        }

        public byte[] RenderToBytesExcel()
        {
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            return reportInstance.Render("EXCEL", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
        }

        /// <summary>     
        /// Renderiza a intancia do relatorio atual para um email com um arquivo anexado contendo o relatorio.
        /// </summary>                
        /// <param name="toAddress">Endereço De.</param>        
        /// <param name="fromAddress">Endereço De.</param>        
        /// <param name="subject">O Assunto.</param>        
        /// <param name="body">O Corpo.</param>                
        /// <returns></returns>        
        public Warning[] RenderToEmailPDF(string toAddress, string fromAddress, string subject, string body)
        {
            return RenderToEmailPDF(null, toAddress, fromAddress, subject, body);
        }
        #endregion

        #region Private Methods

        /// <summary>        
        /// Formata a saida da resposta corrente e retorna um stream de saida apropriada para renderizar no brownser como um arquivo de download.
        /// </summary>        
        /// <param name="name">O Nome.</param>        
        /// <param name="extension">A Extenssão.</param>        
        /// <param name="encoding">O Encoding.</param>        
        /// <param name="mimeType">O tipo do MIME.</param>        
        /// <param name="willSeek">se marcado para <c>true</c> [irá procurar].</param>
        /// <returns></returns>
        private Stream CreateWebBrowserStream(string name, string extension, System.Text.Encoding encoding, string mimeType, bool willSeek)
        {
            return HttpContext.Current.Response.OutputStream;
        }

        /// <summary>        
        /// Cria um stream de memoria que pode ser usada pelo relatorio quando estiver renderizando para um anexo de email.
        /// </summary>       
        /// <param name="name">O Nome.</param>        
        /// <param name="extension">A Extensão.</param>        
        /// <param name="encoding">O encoding.</param>        
        /// <param name="mimeType">Tipo do MIME.</param>        
        /// <param name="willSeek">se marcado para <c>true</c> [irá procurar].</param>        
        /// <returns></returns>        
        private Stream CreateMemoryStream(string name, string extension, System.Text.Encoding encoding, string mimeType, bool willSeek)
        {
            reportMemoryStream = new MemoryStream();
            reportMimeType = mimeType;
            return reportMemoryStream;
        }
        #endregion

        #region IDisposable Members
        /// <summary>           
        /// Realiza tarefas definidas para aplicações associada com liberdar, soltar ou resetar recursos não gerenciados.
        /// </summary>
        public void Dispose()
        {
            if (this.reportInstance != null)
                this.reportInstance.Dispose();
            if (this.reportMemoryStream != null)
                this.reportMemoryStream.Dispose();
        }
        #endregion
    }
}
