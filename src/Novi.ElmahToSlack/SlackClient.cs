using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Novi.ElmahToSlack
{
	public class SlackClient
	{
		private readonly string _userName;
		private readonly string _channel;
		private readonly Uri _webHookUri;

		public SlackClient(string userName, string channel, Uri webHookUri)
		{
			_userName = userName;
			_channel = channel;
			_webHookUri = webHookUri;
		}

		public void SendSlackMessage(string text)
		{
			var payload = new Payload
			{
				Channel = _channel,
				UserName = _userName,
				Text = text
			};
			using (var webClient = new WebClient())
			{
				webClient.Headers.Add("Content-Type", "application/json");
				var request = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));
				var response = webClient.UploadData(_webHookUri, "POST", request);
			}
		}
	}
}