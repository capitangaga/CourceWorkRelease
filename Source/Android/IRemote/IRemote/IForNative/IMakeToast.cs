using System;

namespace IRemote
{
	/// <summary>
	/// Interface to make user notifyes
	/// </summary>
	public interface IMakeToast
	{

		/// <summary>
		/// Shows the toast message
		/// </summary>
		/// <param name="Message">Message</param>
		/// <param name="IsLong">If set to <c>true</c> long showing, else short</param>
		void ShowMessage(string Message, bool IsLong);

	}
}
