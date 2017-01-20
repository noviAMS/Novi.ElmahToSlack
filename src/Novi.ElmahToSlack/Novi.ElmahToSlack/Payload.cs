using Newtonsoft.Json;

namespace Novi.ElmahToSlack
{
	public class Payload
	{
		[JsonProperty("channel")]
		public string Channel { get; set; }
		[JsonProperty("username")]
		public string UserName { get; set; }
		[JsonProperty("text")]
		public string Text { get; set; }
		[JsonProperty("icon_emoji")]
		public string Icon => ":scream:";
	}
}