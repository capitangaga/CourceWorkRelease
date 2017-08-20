using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IRemote.Droid
{
	[Activity(Label = "IRemote.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			_mainContext = this;


			LoadApplication(new App());

		}
		/// <summary>
		/// private field to save Android context for my Activity
		/// </summary>
		static Context _mainContext;
		/// <summary>
		/// Returns context to native methods which needs it
		/// </summary>
		/// <value>The main context</value>
		public static Context MainContext { get { return _mainContext; } }

	}
}
