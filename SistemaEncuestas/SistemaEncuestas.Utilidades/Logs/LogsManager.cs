namespace SistemaEncuestas.Utilidades.Logs
{
    #region Namespaces

    using log4net;
    using log4net.Config;
    using System;

    #endregion

    public class LogsManager
    {
        #region Attributes

        private readonly ILog _log;

        #endregion

        #region Contructors

        public LogsManager(Type typeClass)
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(typeClass);
        }

        #endregion

        #region Public Methods

        public void Info(string message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        public void Warning(string message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        public void Error(string message, Exception ex = null)
        {
            if (_log.IsErrorEnabled)
            {
                if (ex != null)
                {
                    _log.Error(message, ex);

                    return;
                }

                _log.Error(message);
            }
        }

        #endregion
    }
}
