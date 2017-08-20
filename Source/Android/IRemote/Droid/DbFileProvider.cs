using System;
using System.IO;
using Xamarin.Forms;
using IRemote;
using IRemote.Droid;

[assembly: Dependency(typeof(DbFileProvider))]
namespace IRemote.Droid
{

	public class DbFileProvider : IDbFileProvider
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
		}
	}
}
