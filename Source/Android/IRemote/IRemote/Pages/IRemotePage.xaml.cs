using Xamarin.Forms;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
namespace IRemote
{
	public partial class IRemotePage : ContentPage
	{
		public int[] a { get; } = new int[] { 4, 3, 2 };
		public IRemotePage()
		{
			InitializeComponent();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();


		}
		protected async void NewRemoteClicked(object sender, EventArgs e)
		{

		}

		protected async void ConnectClicked(object sender, EventArgs e)
		{
			if (App.BlueCon.IsBluetoothOn)
			{
				await Navigation.PushAsync(new ConnectionPage { BindingContext = null });
			}
			else
			{
				App.ToastMaker.ShowMessage("Please, turn on bluetooth", true);
			}

		}
	}
}
