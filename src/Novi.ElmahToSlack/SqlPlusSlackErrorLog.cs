using System;
using System.Collections;
using System.Threading.Tasks;
using Elmah;

namespace Novi.ElmahToSlack
{
	public class SqlPlusSlackErrorLog : SqlErrorLog
	{
		public string WebHookUri { get; set; }
		public string UserName { get; set; }
		public string Channel { get; set; }

		public SqlPlusSlackErrorLog(IDictionary config) : base(config)
		{
			WebHookUri = config["webHookUri"].ToString();
			if (string.IsNullOrWhiteSpace(WebHookUri))
				throw new Exception("The elmah errorLog config element must have a value for webHookUri.");
			UserName = config["userName"].ToString();
			if (string.IsNullOrWhiteSpace(UserName))
				throw new Exception("The elmah errorLog config element must have a value for userName.");
			Channel = config["channel"].ToString();
			if (string.IsNullOrWhiteSpace(Channel))
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
						var client = new SlackClient(UserName, Channel, new Uri(WebHookUri));
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