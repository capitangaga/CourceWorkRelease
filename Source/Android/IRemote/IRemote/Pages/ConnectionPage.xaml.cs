using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IRemote
{
	public partial class ConnectionPage : ContentPage
	{

		public ConnectionPage()
		{
			InitializeComponent();
			if (App.BlueCon.AnyBluetooth)
			{
				Devices.ItemsSource = App.BlueCon.BoundedDevicesNames;
			}
			else
			{
				StackLayout errorStack = new StackLayout();

				errorStack.Children.Add(new Label
				{
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Text = "Your device have no Bluetooth\n\n\n\nThis app wouldn't work, sorry((",
					FontSize = 30,
					//TextColor = Color.FromHex("#F44336"),
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center,
					Margin = new Thickness(20, 50)
				});
				Content = errorStack;
				ToolbarItems.Clear();
			}
			selectedNumber = -1;
			Devices.ItemSelected += OnItemSelected;

		}

		int selectedNumber;

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (App.BlueCon.AnyBluetooth)
			{
				if (App.BlueCon.AnyBluetooth && !App.BlueCon.IsBluetoothOn)
				{
					App.ToastMaker.ShowMessage("Please, turn on the bluetooth", true);

				}
				Devices.ItemsSource = App.BlueCon.BoundedDevicesNames;
				selectedNumber = -1;
				status.Text = App.BlueCon.ConnectedDeviceName;
			}
		}

		protected async void OnConnectClicked(object sender, System.EventArgs e)
		{
			if (selectedNumber != -1)
			{
				App.BlueCon.DeviceToWorkSetByNumber = selectedNumber;
				bool result = await App.BlueCon.ConnectToSelectedDevice();
				if (result)
				{
					App.ToastMaker.ShowMessage($"Connected to {Devices.SelectedItem as string}", false);
					status.Text = $"Connected to {Devices.SelectedItem as string}";
				}
				else
				{
					App.ToastMaker.ShowMessage($"Can not connect to {Devices.SelectedItem as string}", false);
					status.Text = "Not Connected";
				}
			}
			else
			{
				App.ToastMaker.ShowMessage("Select a device first", false);

			}

		}

		protected void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			int i = 0;
			foreach (object dev in Devices.ItemsSource)
			{
				if (dev.Equals(Devices.SelectedItem))
				{
					selectedNumber = i;
				}
				i++;
			}
		}

		protected void OnDisconnectClicked(object sender, EventArgs e)
		{
			App.BlueCon.Disconnect();
			App.ToastMaker.ShowMessage("All connections are closed", false);
			status.Text = "Not Connected";
		}
	}
}
