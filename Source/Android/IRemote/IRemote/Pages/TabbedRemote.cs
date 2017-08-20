using System;

using Xamarin.Forms;

namespace IRemote
{
	public class TabbedRemote : TabbedPage
	{
		public TabbedRemote()
		{

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			Title = (BindingContext as Remote).Name;
			Children.Clear();
			Children.Add(new RemotePage { BindingContext = this.BindingContext, IStart = 0, IFinish = 19, Title = "Main" });
			Children.Add(new RemotePage { BindingContext = this.BindingContext, IStart = 20, IFinish = 43, Title = "Additonal" });
			Children.Add(new RemotePage { BindingContext = this.BindingContext, IStart = 44, IFinish = 59, Title = "Numbers" });
		}
	}
}

