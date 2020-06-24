using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Configuration;

namespace Cadastramento.Util
{
    public class sendEmail
    {
        private string smtpServer = string.Empty;
        private int smtpPorta = 0;
        private string emailLogin = string.Empty;
        private string emailSenha = string.Empty;

        public bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConfigureAutentication()
        {
            smtpServer = ConfigurationManager.AppSettings["smtpServer"];
            smtpPorta = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPorta"]);
            emailLogin = ConfigurationManager.AppSettings["emailLogin"];
            emailSenha = ConfigurationManager.AppSettings["emailSenha"];
        }

        public void ConfigureAutentication(string smtpServer,string smtpPorta, string emailLogin, string emailSenha)
        {
            this.smtpServer = smtpServer;
            this.smtpPorta = smtpPorta.StrToInt32();
            this.emailLogin = emailLogin;
            this.emailSenha = emailSenha;
        }


        //public bool EnviarEmail(string de, List<string> para, string assunto, string mensagem, Attachment anexo)
        //{
        //    try
        //    {
        //        //Cria objeto com dados do e-mail.
        //        MailMessage Email = new MailMessage();

        //        //Define o Campo From e ReplyTo do e-mail.
        //        Email.From = new System.Net.Mail.MailAddress(de);

        //        //Define os destinatários do e-mail.
        //        foreach (var item in para)
        //        {
        //            Email.To.Add(item);
        //        }

        //        Email.IsBodyHtml = true;
        //        Email.Body = mensagem;
        //        Email.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
        //        Email.Subject = assunto;
        //        Email.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        //        if (anexo != null)
        //        {
        //            Email.Attachments.Add(anexo);
        //        }

        //        SmtpClient SC = new SmtpClient(smtpServer, smtpPorta);
        //        SC.Credentials = new NetworkCredential(emailLogin, emailSenha);
        //        SC.EnableSsl = true;
        //        //SC.EnableSsl = true;                 
        //        //SC.UseDefaultCredentials = false;

        //        SC.Send(Email);

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// Método padrão para enviar e-mail
        /// </summary>
        /// <param name="de"></param>
        /// <param name="para"></param>
        /// <param name="assunto"></param>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        public bool EnviarEmail(string de, string para, string assunto, string mensagem)
        {
            try
            {
                //Cria objeto com dados do e-mail.
                var email = new MailMessage { From = new MailAddress(de) };

                //Define o Campo From e ReplyTo do e-mail.

                //Destinatário do e-mail.
                email.To.Add(para);

                email.IsBodyHtml = true;
                email.Body = mensagem;
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.Subject = assunto;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                var sc = new SmtpClient(smtpServer, smtpPorta)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailLogin, emailSenha),
                    EnableSsl = true
                };

                sc.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EnviarEmail(string de, List<string> para, string assunto, string mensagem)
        {
            try
            {
                //Cria objeto com dados do e-mail.
                var email = new MailMessage { From = new MailAddress(de) };

                //Define o Campo From e ReplyTo do e-mail.

                //Define os destinatários do e-mail.
                foreach (var item in para)
                {
                    email.To.Add(item);
                }

                email.IsBodyHtml = true;
                email.Body = mensagem;
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.Subject = assunto;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                var sc = new SmtpClient(smtpServer, smtpPorta)
                {
                    Credentials = new NetworkCredential(emailLogin, emailSenha)
                };

                sc.EnableSsl = true;
                sc.Send(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EnviarEmail(string de, List<string> para, string assunto, string mensagem, string smtpEndereco, int smtpPort, string emailLogon, string emailPass)
        {
            try
            {
                //Cria objeto com dados do e-mail.
                var email = new MailMessage { From = new MailAddress(de) };

                //Define o Campo From e ReplyTo do e-mail.

                //Define os destinatários do e-mail.
                foreach (var item in para)
                {
                    email.To.Add(item);
                }

                email.IsBodyHtml = true;
                email.Body = mensagem;
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.Subject = assunto;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                var sc = new SmtpClient(smtpEndereco, smtpPort)
                {
                    Credentials = new NetworkCredential(emailLogon, emailPass)
                };
                sc.EnableSsl = true;


                sc.Send(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EnviarEmail(string de, List<string> para, string assunto, string mensagem, string smtpEndereco, int smtpPort, string emailLogon, string emailPass, Attachment anexo)
        {
            try
            {
                //Cria objeto com dados do e-mail.
                var email = new MailMessage { From = new MailAddress(de) };

                //Define o Campo From e ReplyTo do e-mail.

                //Define os destinatários do e-mail.
                foreach (var item in para)
                {
                    email.To.Add(item);
                }

                if (anexo != null)
                {
                    email.Attachments.Add(anexo);
                }

                email.IsBodyHtml = true;
                email.Body = mensagem;
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.Subject = assunto;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                var sc = new SmtpClient(smtpEndereco, smtpPort)
                {
                    Credentials = new NetworkCredential(emailLogon, emailPass)
                };
                sc.EnableSsl = true;

                sc.Send(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EnviarEmail(string de, List<string> para, string assunto, string mensagem, string smtpEndereco, int smtpPort, string emailLogon, string emailPass, Dictionary<string, byte[]> imagens)
        {
            try
            {
                //Cria objeto com dados do e-mail.
                var email = new MailMessage { From = new MailAddress(de) };

                //Define o Campo From e ReplyTo do e-mail.

                //Define os destinatários do e-mail.
                foreach (var item in para)
                {
                    email.To.Add(item);
                }

                email.IsBodyHtml = true;
                email.Body = mensagem;
                email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
                email.Subject = assunto;
                email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

                foreach (var imagem in imagens)
                {
                    MemoryStream image1 = new MemoryStream(imagem.Value);
                    AlternateView av = AlternateView.CreateAlternateViewFromString(mensagem, null, MediaTypeNames.Text.Html);

                    LinkedResource headerImage = new LinkedResource(image1, MediaTypeNames.Image.Jpeg);
                    headerImage.ContentId = imagem.Key;
                    headerImage.ContentType = new ContentType("image/jpg");
                    av.LinkedResources.Add(headerImage);
                    email.AlternateViews.Add(av);
                }

                //ContentType mimeType = new ContentType("text/html");
                //AlternateView alternate = AlternateView.CreateAlternateViewFromString(mensagem, mimeType);
                //email.AlternateViews.Add(alternate);

                var sc = new SmtpClient(smtpEndereco, smtpPort)
                {
                    Credentials = new NetworkCredential(emailLogon, emailPass)
                };
                sc.EnableSsl = true;

                sc.Send(email);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
