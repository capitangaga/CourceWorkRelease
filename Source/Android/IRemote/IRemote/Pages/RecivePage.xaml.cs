using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;

namespace IRemote
{
	public partial class RecivePage : ContentPage
	{

		public RecivePage()
		{
			InitializeComponent();

		}
		protected async override void OnAppearing()
		{

			IRSignal sig = await App.BlueCon.ReciveIR();
			if (sig.Ok)
			{
				(BindingContext as RemoteButton).Signal = sig;
				App.ToastMaker.ShowMessage($"Recorded {sig}", false);
			}
			else
			{
				App.ToastMaker.ShowMessage("An error accured, try again", false);
			}
			if (IsVisible && IsEnabled)
			{
				try
				{
					await Navigation.PopAsync();
				}
				catch
				{
					App.ToastMaker.ShowMessage("Please, restart the app", false);
				}
			}

		}
		protected override bool OnBackButtonPressed()
		{
			App.BlueCon.CalancellReciving();

			//bool res = base.OnBackButtonPressed();
			return true;
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			App.BlueCon.CalancellReciving();
		}

	}
}
