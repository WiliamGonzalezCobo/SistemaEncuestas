namespace SistemaEncuestas.Utilidades.Correo
{
    #region Namespaces

    using Logs;
    using Recursos;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;

    #endregion

    public class Correo
    {
        #region Public Methods

        public static bool EnviarCorreo(CorreoEntity configuracionCorreo, string de, List<string> listaPara,
            List<string> listaCopia, string asunto, string cuerpo)
        {
            LogsManager logManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);

            string smtp = configuracionCorreo.SMTP;
            int puerto = configuracionCorreo.Puerto;
            string usuario = configuracionCorreo.Usuario;
            string contrasena = configuracionCorreo.Password;
            bool habilitadoSSL = configuracionCorreo.EsSSL;
            bool habilitadoHTML = configuracionCorreo.EsHTML;

            try
            {
                SmtpClient clienteSmtp = new SmtpClient(smtp);
                MailMessage mensaje = new MailMessage();

                clienteSmtp.Port = puerto;
                clienteSmtp.Credentials = new System.Net.NetworkCredential(usuario, contrasena);
                clienteSmtp.EnableSsl = habilitadoSSL;

                mensaje.From = new MailAddress(de);

                foreach (string para in listaPara)
                {
                    mensaje.To.Add(new MailAddress(para));
                }

                if (listaCopia != null)
                {
                    foreach (string copia in listaCopia)
                    {
                        mensaje.CC.Add(new MailAddress(copia));
                    }
                }

                mensaje.Subject = asunto;
                mensaje.SubjectEncoding = Encoding.UTF8;
                mensaje.Body = cuerpo;
                mensaje.BodyEncoding = Encoding.UTF8;
                mensaje.IsBodyHtml = habilitadoHTML;

                clienteSmtp.Send(mensaje);

                logManager.Info(Mensajes.MensajeCorreoEnviado);

                return true;
            }
            catch
            {
                logManager.Error(Mensajes.MensajeCorreoError);
                return false;
            }
        }

        #endregion
    }
}