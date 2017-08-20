using System;
using Xamarin.Forms;
using Android.App;
using Android.Widget;
using IRemote.Droid;
[assembly: Dependency(typeof(MakeToast))]
namespace IRemote.Droid
{

	public class MakeToast : IMakeToast
	{

		public void ShowMessage(string Message, bool IsLong)
		{
			ToastLength len = IsLong ? ToastLength.Long : ToastLength.Short;
			Toast toShow = Toast.MakeText(MainActivity.MainContext, Message, len);
			toShow.Show();
		}
	}
}
