using System;

namespace Novi.ElmahToSlack
{
	public class SlackException : Exception
	{
		public SlackException(string message, Exception innerException) : base(message)
		{
			InnerException = innerException;
		}

		public new Exception InnerException { get; private set; }
	}
}