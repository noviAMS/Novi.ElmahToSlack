using System;

namespace Novi.ElmahToSlack
{
	public class SlackException : Exception
	{
		public SlackException(string message) : base(message)
		{
		}
	}
}