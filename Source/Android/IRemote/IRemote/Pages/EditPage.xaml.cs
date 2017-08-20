using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IRemote
{
	public partial class EditPage : ContentPage
	{
		Remote bindedRemote;
		List<Button> allButtons;
		public EditPage()
		{
			InitializeComponent();
			RemoteNameEntery.TextChanged += OnRemoteNameChanged;
			CategoryEntery.TextChanged += CategoryEntery_TextChanged;
			CategoryPicker.SelectedIndexChanged += CategoryPicker_SelectedIndexChanged;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			bindedRemote = BindingContext as Remote;
			Title = bindedRemote.Name;
			RemoteNameEntery.Text = Title;

			GenerateGrid(MainGrid, 0, 19);
			GenerateGrid(AdditionalGrid, 20, 43);
			GenerateGrid(NumbersGrid, 44, 59);
			SetAllButtonsList();
			GenerateCustomKeysEditor();
			CategoryPicker.Items.Clear();
			CategoryEntery.Text = bindedRemote.Category;
			List<string> categories = await App.Database.GetCategoriesAsync();
			foreach (string cat in categories)
			{
				CategoryPicker.Items.Add(cat);
			}
		}

		protected void OnRemoteNameChanged(object sennder, EventArgs e)
		{
			if (RemoteNameEntery.Text.Length > 32)
			{
				RemoteNameEntery.Text = RemoteNameEntery.Text.Substring(0, 32);
			}
			bindedRemote.Name = RemoteNameEntery.Text;
			Title = RemoteNameEntery.Text;
		}
		protected async override void OnDisappearing()
		{
			//await DisplayAlert("Don't forget to save changes",
			//				   "Untill you press save button, changes won\'t save to" +
			//				   " database and will reset after remote closing", "Got it");
			base.OnDisappearing();

		}
		protected async void OnSaveClicked(object sender, EventArgs e)
		{
			await App.Database.SaveRemoteAsync(bindedRemote);
			App.ToastMaker.ShowMessage($"Remote {bindedRemote.Name} saved", false);
		}

		void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CategoryPicker.SelectedIndex >= 0)
			{
				CategoryEntery.Text = CategoryPicker.Items[CategoryPicker.SelectedIndex];
				CategoryPicker.SelectedIndex = -1;

			}
		}

		void CategoryEntery_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (CategoryEntery.Text.Length > 24)
			{
				CategoryEntery.Text = CategoryEntery.Text.Substring(0, 24);
			}
			bindedRemote.Category = CategoryEntery.Text;
		}

		private void GenerateGrid(Grid grid, int istart, int ifinish)
		{
			grid.Children.Clear();
			for (int i = 0; i < ifinish - istart + 1; i++)
			{
				int remoteButtonNumber = istart + i;
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
				grid.Children.Add(nextButton, i % 4, i / 4);
			}

		}
		private void GenerateCustomKeysEditor()
		{
			CustomButtonsEdit.Children.Clear();
			int i = 0;
			foreach (int keyId in Remote.CustomKeys)
			{
				i++;
				StackLayout next = new StackLayout { Orientation = StackOrientation.Horizontal };
				next.Children.Add(new Label
				{
					Text = $"Custom Key {i} ",
					HorizontalOptions = LayoutOptions.Start,
					Margin = new Thickness(10, 10, 10, 10),
					FontSize = 18
				});
				next.Children.Add(new Entry
				{
					BindingContext = keyId,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Text = allButtons[keyId].Text,
				});
				(next.Children[1] as Entry).TextChanged += OnCustomKeyTextChanged;
				CustomButtonsEdit.Children.Add(next);
			}
		}
		private void SetAllButtonsList()
		{
			allButtons = new List<Button>();
			foreach (View child in MainGrid.Children)
			{
				allButtons.Add(child as Button);
			}
			foreach (View child in AdditionalGrid.Children)
			{
				allButtons.Add(child as Button);
			}
			foreach (View child in NumbersGrid.Children)
			{
				allButtons.Add(child as Button);
			}
		}
		protected void OnCustomKeyTextChanged(object sender, EventArgs e)
		{

			Entry customKeyEntry = sender as Entry;
			int customKeyId = (int)customKeyEntry.BindingContext;
			if (customKeyEntry.Text.Length > 16)
			{
				customKeyEntry.Text = customKeyEntry.Text.Substring(0, 16);
			}
			allButtons[customKeyId].Text = customKeyEntry.Text;
			bindedRemote.Buttons[customKeyId].Text = customKeyEntry.Text;

		}

		void NextButton_Clicked(object sender, EventArgs e)
		{
			if (App.BlueCon.AnyBluetooth)
			{
				if (App.BlueCon.IsBluetoothOn)
				{
					if (App.BlueCon.IsConnected)
					{
						Navigation.PushAsync(new RecivePage { BindingContext = (sender as Button).BindingContext as RemoteButton });
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
