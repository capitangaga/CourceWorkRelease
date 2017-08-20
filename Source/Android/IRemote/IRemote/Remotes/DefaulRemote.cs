using System;
namespace IRemote
{
	public static class DefaultRemote
	{
		public static string[] DefaultButtonNames
		{
			get
			{
				return new string[60]
				{
					"?", "Source", "Mute","\u23FB",
					"CH+", "CH-", "VOL+", "VOL-",
					"GUIDE", "MENU", "\u25B2", "\u232B",
					"SMART", "\u25C0", "\u23CE", "\u25B6",
					"EXIT", "FRAME", "\u25BC", "HOME",

					"", "", "", "", // row with RGYB buttons
					"C1", "C2", "C3", "C4",
					"C5", "C6", "C7", "C8",
					"|\u25C0\u25C0", "\u25C0\u25C0", "\u25B6\u25B6", "\u25B6\u25B6|",
					"\u25CF", "\u25B6", "||", "\u25A0",
					"BASS+", "BASS-", "TRE+", "TRE-",

					"1", "2", "3", "C9",
					"4", "5", "6", "C10",
					"7", "8", "9", "C11",
					"C12", "0", "C13", "C14"
				};
			}
		}
	}
	public partial class Remote
	{
		public Remote()
		{

		}
		public Remote(bool IsNew)
		{
			if (IsNew)
			{
				Buttons = new System.Collections.Generic.List<RemoteButton>();
				for (int i = 0; i < 60; i++)
				{
					Buttons.Add(new RemoteButton(i, DefaultRemote.DefaultButtonNames[i]));
				}
				ID = 0;
				Name = "New Remote";
			}
		}

		public static int[] CustomKeys
		{
			get
			{
				return new int[] { 24, 25, 26, 27, 28, 29, 30, 31, 47, 51, 55, 56, 58, 59 };
			}
		}
	}
}