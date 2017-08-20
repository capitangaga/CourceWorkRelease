using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Newtonsoft.Json;
namespace IRemote
{
	public class RemotesDatabase
	{
		readonly SQLiteAsyncConnection database;

		public RemotesDatabase(string path)
		{
			database = new SQLiteAsyncConnection(path);
			RemoteToSave lol = new RemoteToSave();
			lol.Category = "LOL";
			database.CreateTableAsync<RemoteToSave>().Wait();
			//database.DropTableAsync<RemoteToSave>().Wait();
		}
		/// <summary>
		/// Gets all the remotes from database
		/// </summary>
		/// <returns>The remotes list</returns>
		public async Task<List<Remote>> GetRemotesAsync()
		{
			List<RemoteToSave> JSONRemotes = await database.Table<RemoteToSave>().ToListAsync();
			List<Remote> remotes = new List<Remote>();
			await Task.Run(() =>
			{
				{
					foreach (RemoteToSave rem in JSONRemotes)
					{
						Remote nextRemote = JsonConvert.DeserializeObject<Remote>(rem.JSONRemote);
						nextRemote.ID = rem.ID;
						nextRemote.Category = rem.Category;
						remotes.Add(nextRemote);
					}
				}
			});
			return remotes;
		}
		/// <summary>
		/// Gets the remotes with mancioned category.
		/// </summary>
		/// <returns>The remotes with mancioned category.</returns>
		/// <param name="category">Category.</param>
		public async Task<List<Remote>> GetRemotesWithCategoryAsync(string category)
		{
			List<RemoteToSave> JSONRemotes = await database.Table<RemoteToSave>().ToListAsync();
			List<Remote> remotes = new List<Remote>();
			await Task.Run(() =>
			{
				{
					foreach (RemoteToSave rem in JSONRemotes)
					{
						if (rem.Category == category)
						{
							Remote nextRemote = JsonConvert.DeserializeObject<Remote>(rem.JSONRemote);
							nextRemote.ID = rem.ID;
							nextRemote.Category = rem.Category;
							remotes.Add(nextRemote);
						}
					}
				}
			});
			return remotes;
		}
		/// <summary>
		/// Gets the categories.
		/// </summary>
		/// <returns>The categories list</returns>
		public async Task<List<string>> GetCategoriesAsync()
		{
			List<RemoteToSave> JSONRemotes = await database.Table<RemoteToSave>().ToListAsync();
			HashSet<string> categories = new HashSet<string>();
			List<string> categoriesList = new List<string>();
			await Task.Run(() =>
			{
				foreach (RemoteToSave rem in JSONRemotes)
					if (!String.IsNullOrWhiteSpace(rem.Category))
						categories.Add(rem.Category);
				foreach (string cat in categories)
					categoriesList.Add(cat);
			});
			return categoriesList;

		}
		/// <summary>
		/// Gets the remote by identifier.
		/// </summary>
		/// <returns>The remote by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<Remote> GetRemoteByIDAsync(int id)
		{
			RemoteToSave retrem = await database.Table<RemoteToSave>().Where(i => i.ID == id).FirstOrDefaultAsync();
			return JsonConvert.DeserializeObject<Remote>(retrem.JSONRemote);

		}
		/// <summary>
		/// Saves the remote async.
		/// </summary>
		/// <returns>int</returns>
		/// <param name="rem">Remote</param>
		public async Task<int> SaveRemoteAsync(Remote rem)
		{
			RemoteToSave save = new RemoteToSave
			{
				ID = rem.ID,
				Category = rem.Category,
				JSONRemote = JsonConvert.SerializeObject(rem)
			};
			if (save.ID != 0)
			{
				return await database.UpdateAsync(save);
			}
			else
			{
				return await database.InsertAsync(save);
			}

		}
		/// <summary>
		/// Removes the remote async.
		/// </summary>
		/// <returns>int</returns>
		/// <param name="rem">Remote</param>
		public async Task<int> RemoveRemoteAsync(Remote rem)
		{
			RemoteToSave del = new RemoteToSave
			{
				ID = rem.ID,
				Category = rem.Category,
				JSONRemote = JsonConvert.SerializeObject(rem)
			};
			return await database.DeleteAsync(del);
		}
	}
}