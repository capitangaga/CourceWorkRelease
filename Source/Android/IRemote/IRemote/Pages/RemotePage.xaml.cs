using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IRemote
{
	public partial class RemotePage : ContentPage
	{
		protected Remote bindedRemote;

		public int IStart { get; set; }

		public int IFinish { get; set; }



		public RemotePage()
		{
			InitializeComponent();


		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			bindedRemote = BindingContext as Remote;
			ButtonsGrid.Children.Clear();
			for (int i = 0; i < IFinish - IStart + 1; i++)
			{
				int remoteButtonNumber = IStart + i;
				Button nextButton = new Button()
				{
					Text = $"{bindedRemote.Buttons[remoteButtonNumber].Text}",
					BorderRadius = 0,
					//Margin = new Thickness(-1, -1, -1, -1),
					BackgroundColor = Color.FromHex("EEEEEE"),
					BindingContext = bindedRemote.Buttons[remoteButtonNumber]
				};
				switch (remoteButtonNumber)
				{
					case 3:
						nextButton.BackgroundColor = Color.FromHex("F44336");
						nextButton.FontFamily = Device.OnPlatform(null, "Unicode_IEC_symbol.ttf#IEC-symbols-Unicode", null);
						break;
					case 20:
						nextButton.BackgroundColor = Color.FromHex("F44336");
						break;
					case 21:
						nextButton.BackgroundColor = Color.FromHex("4CAF50");
						break;
					case 22:
						nextButton.BackgroundColor = Color.FromHex("FFEB3B");
						break;
					case 23:
						nextButton.BackgroundColor = Color.FromHex("2196F3");
						break;
					default:
						break;
				}
				nextButton.Clicked += NextButton_Clicked;
				ButtonsGrid.Children.Add(nextButton, i % 4, i / 4);
			}

		}



		protected async void OnDeleteClicked(object sender, EventArgs e)
		{
			var confirm = await DisplayAlert("Delete the remote?", "Are you shure to delete" +
											 $"{(BindingContext as Remote).Name} remote?", "Delete", "Clancel");
			if (confirm)
			{
				await App.Database.RemoveRemoteAsync(BindingContext as Remote);
				await Navigation.PopAsync();
			}
		}
		protected async void OnEditClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EditPage { BindingContext = this.BindingContext });
		}

		void NextButton_Clicked(object sender, EventArgs e)
		{
			if (App.BlueCon.AnyBluetooth)
			{
				if (App.BlueCon.IsBluetoothOn)
				{
					if (App.BlueCon.IsConnected)
					{
						App.BlueCon.SendIR(((sender as Button).BindingContext as RemoteButton).Signal);
					}
					else
					{
						App.ToastMaker.ShowMessage("You are not connected", false);
					}
				}
				else
				{
					App.ToastMaker.ShowMessage("Turn on the Bluetooth", false);
				}


			}
			else
			{
				App.ToastMaker.ShowMessage("There is no Bluetooth on your device", false);
			}

		}
	}
}
