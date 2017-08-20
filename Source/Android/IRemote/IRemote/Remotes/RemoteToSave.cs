using System;
using SQLite;


namespace IRemote
{

	/// <summary>
	/// Remote to save in SQLite db
	/// </summary>

	public class RemoteToSave
	{
		[PrimaryKey, AutoIncrement]
		/// <summary>
		/// Gets or sets the identifier of Remote
		/// </summary>
		/// <value>The identifier.</value>
		public int ID { get; set; }
		/// <summary>
		/// Gets or sets the Category of Remote
		/// </summary>
		/// <value>The Category</value>
		public string Category { get; set; }
		/// <summary>
		/// Gets or sets the string with JSON Relation of Remote object
		/// </summary>
		/// <value>The keys.</value>
		public string JSONRemote { get; set; }

	}
}
