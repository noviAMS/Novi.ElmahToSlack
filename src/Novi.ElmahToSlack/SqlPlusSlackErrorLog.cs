using System;
using System.Collections;
using System.Threading.Tasks;
using Elmah;

namespace Novi.ElmahToSlack
{
	public class SqlPlusSlackErrorLog : SqlErrorLog
	{
		private readonly string _webHookUri;
		private readonly string _userName;
		private readonly string _channel;

		public SqlPlusSlackErrorLog(IDictionary config) : base(config)
		{
			_webHookUri = config["webHookUri"].ToString();
			if (string.IsNullOrWhiteSpace(_webHookUri))
				throw new Exception("The elmah errorLog config element must have a value for webHookUri.");
			_userName = config["userName"].ToString();
			if (string.IsNullOrWhiteSpace(_userName))
				throw new Exception("The elmah errorLog config element must have a value for userName.");
			_channel = config["channel"].ToString();
			if (string.IsNullOrWhiteSpace(_channel))
				throw new Exception("The elmah errorLog config element must have a value for channel.");
		}

		public SqlPlusSlackErrorLog(string connectionString) : base(connectionString)
		{
		}

		public override string Name => "Microsoft SQL Server Error Log + Slack";

		public override string Log(Error error)
		{
			var result = base.Log(error);

			if (!(error.Exception is SlackException))
			{
				Task.Run(() => // this doesn't have to hang up the executing code
				{
					try
					{
						var client = new SlackClient(_userName, _channel, new Uri(_webHookUri));
						client.SendSlackMessage($"{error.ServerVariables["SERVER_NAME"]}: [{error.Type}] {error.Message}");
					}
					catch (Exception exc)
					{
						throw new SlackException("Can't Slack the elmah error. See InnerException.", exc);
					}
				});
			}

			return result;
		}
	}
}