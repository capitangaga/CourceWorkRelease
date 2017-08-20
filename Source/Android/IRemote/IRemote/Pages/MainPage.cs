using System;
using System.Diagnostics;

using Xamarin.Forms;

namespace IRemote
{
	public class MainPage : MasterDetailPage
	{
		MasterPage master;

		public MainPage()
		{
			Detail = new NavigationPage(new HomePage { BindingContext = null });
			master = new MasterPage();
			Master = master;
			master.ConnectButtonClicked += OnConnectButtonClicked;
			master.ShowAllButtonClicked += OnShowAllButtonClicked;
			IsPresentedChanged += MainPage_IsPresentedChanged;
			master.CategorySelected += OnCategorySelected;
			master.HelpButtonClicked += OnHelpButtonClicked;
		}

		void MainPage_IsPresentedChanged(object sender, EventArgs e)
		{
			master.UpdateCategories();
		}

		public void OnConnectButtonClicked(object sender, EventArgs e)
		{
			Detail = new NavigationPage(new ConnectionPage());
			IsPresented = false;
		}
		public void OnShowAllButtonClicked(object sender, EventArgs e)
		{
			Detail = new NavigationPage(new SelectionPage { BindingContext = null });
			IsPresented = false;
		}

		void OnCategorySelected(object sender, CategorySelectedEventArgs e)
		{
			Detail = new NavigationPage(new SelectionPage { BindingContext = e.Category });
			IsPresented = false;
		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
		void OnHelpButtonClicked(object sender, EventArgs e)
		{
			Detail = new NavigationPage(new HelpPage());
			IsPresented = false;
		}
	}
}

