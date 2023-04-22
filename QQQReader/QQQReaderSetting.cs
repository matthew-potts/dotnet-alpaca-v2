using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using dotnet_alpaca_config;


namespace QQQReader
{
	public class QQQReaderSetting
	{
		private static QQQReaderConfig _qqqReaderSettings;
		public static QQQReaderConfig QQQReaderSettings
        {
			get
            {
				if (_qqqReaderSettings == null)
                {
					_qqqReaderSettings = new QQQReaderConfig();
					ConfigurationBase.Configuration.Bind("QQQFile", _qqqReaderSettings);
                }

				return _qqqReaderSettings;
            }
        }

	}


	public interface IQQQReaderBase
    {
		string QQQFile { get; }
    }

	public class QQQReaderBase : IQQQReaderBase
    {
		private string _qqqFile { get; }
		public string QQQFile { get { return _qqqFile; } }

		public QQQReaderBase()
        {
			this._qqqFile = QQQReaderSetting.QQQReaderSettings?.QQQ_FILE;
        }
    }

}

