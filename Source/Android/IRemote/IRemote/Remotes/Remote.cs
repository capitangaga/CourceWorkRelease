using System;
using System.Collections.Generic;
namespace IRemote
{
	public partial class Remote
	{

		public string Name { get; set; }
		public string Category { get; set; }
		public int ID { get; set; }
		public List<RemoteButton> Buttons;

	}
}
