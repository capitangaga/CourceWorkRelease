using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IRemote
{
	public partial class HelpPage : ContentPage
	{
		public HelpPage()
		{
			InitializeComponent();
			about.Text = "Thank you for using IRemote app. \n\n If you want to build the receive-sending device by yourself," +
				" please, contact kirillgolowko@gmail.com for more details and the protocol description. \n\n" +
				"To connect to your devise use connection manager. You should have a Bluetooth turned on on your device." +
				" Then you could use the app. \n\nTo use a virtual remote just press a button.There are three tabs with " +
				"different keys.There is an Edit tab to edit your remote.Here you can choose name and category for your " +
				"remote, also some captures on buttons may be changed. To record IR signal press a button on virtual " +
				"remote in edit mode and then press a button on your real remote.App would record the signal automaticly. " +
				"To cancel recording just press the back button. \n\nThere is a + for adding new remote and space under" +
				" Show all remotes for categories. If app works unproper restart it.\n\n This is CC app, use it for your own needs for free. " +
				"\n\nBy Kirill Golovko, 2017.";
			Title = "Help and About";
		}
	}
}
