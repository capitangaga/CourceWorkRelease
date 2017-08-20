using System;
using System.Collections.Generic;


using Xamarin.Forms;

namespace IRemote
{
	public partial class HomePage : ContentPage
	{
		string a { get; set; }
		public HomePage()
		{
			a = "123";



			InitializeComponent();

			aboutLabel.Text = "This app will prowide you full-functional remote for using with ArduinoIR. You" +
				" can easily manage your remotes an devide them by zones where you would like to use them. To find more " +
				"abaut bluetooth protocol and other additional informaton use HELP AND ABOUT BUTTON in left menu. \u23FB";

			aboutLabel.FontFamily = Device.OnPlatform(null, "Unicode_IEC_symbol.ttf#IEC-symbols-Unicode", null);

		}
	}
}
