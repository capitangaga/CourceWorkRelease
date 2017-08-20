using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IRemote
{
	public delegate void CategorySelectedEventHandler(object sender, CategorySelectedEventArgs e);

	public partial class MasterPage : ContentPage
	{
		public MasterPage()
		{
			InitializeComponent();

			Title = "IRemote";

			LogoLabel.BackgroundColor = (Color)App.Current.Resources["primaryColor"];
			LogoView.BackgroundColor = (Color)App.Current.Resources["primaryColor"];

			ConnectButton.Clicked += OnNavigation;
			ShowAllButton.Clicked += OnNavigation;
			CategoryList.ItemTapped += OnNavigation;
			HelpButton.Clicked += OnNavigation;

		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			CategoryList.ItemsSource = await App.Database.GetCategoriesAsync();
		}

		public event CategorySelectedEventHandler CategorySelected;
		public event EventHandler HelpButtonClicked;
		public event EventHandler ConnectButtonClicked;
		public event EventHandler ShowAllButtonClicked;

		public void OnNavigation(object sender, EventArgs e)
		{
			if (sender == ConnectButton && ConnectButtonClicked != null)
				ConnectButtonClicked(this, EventArgs.Empty);

			if (sender == HelpButton && HelpButtonClicked != null)
				HelpButtonClicked(this, EventArgs.Empty);

			if (sender == ShowAllButton && ShowAllButtonClicked != null)
				ShowAllButtonClicked(this, EventArgs.Empty);
			if (sender == CategoryList && CategorySelected != null)
			{
				CategorySelected(this, new CategorySelectedEventArgs((e as ItemTappedEventArgs).Item as string));
			}

		}
		public async void UpdateCategories()
		{
			CategoryList.ItemsSource = await App.Database.GetCategoriesAsync();
		}
	}

	public class CategorySelectedEventArgs : EventArgs
	{
		public CategorySelectedEventArgs(string category) : base()
		{
			Category = category;
		}
		public string Category { get; }
	}

}
