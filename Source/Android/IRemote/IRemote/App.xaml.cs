using Xamarin.Forms;

namespace IRemote
{

	public partial class App : Application
	{
		static IBlueConnection _con;

		static RemotesDatabase _database;

		static IMakeToast _toastMaker;
		public App()
		{
			InitializeComponent();
			//Задаем ресурсы с цветами приложения, чтобы потом не путать где какой цвет
			Resources = new ResourceDictionary();
			Resources.Add("primaryDarkColor", Color.FromHex("01579B"));
			Resources.Add("primaryColor", Color.FromHex("03A9F4"));
			Resources.Add("backgroundColor", Color.FromHex("ECEFF1"));

			//
			//Создаем страницу навигации из главной страницы и указываем ей необходимые цвета

			var startfrom = new MainPage();




			// передаем управление на главную страницу
			MainPage = startfrom;

		}

		public static IBlueConnection BlueCon
		{
			get
			{
				if (_con == null)
				{
					_con = DependencyService.Get<IBlueConnection>();
				}
				return _con;
			}
		}
		public static IMakeToast ToastMaker
		{
			get
			{
				if (_toastMaker == null)
				{
					_toastMaker = DependencyService.Get<IMakeToast>();
				}
				return _toastMaker;
			}
		}

		public static RemotesDatabase Database
		{
			get
			{
				if (_database == null)
				{
					_database = new RemotesDatabase(DependencyService.Get<IDbFileProvider>().GetLocalFilePath("RemotesDatabase.db3"));
				}
				return _database;
			}

		}


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
