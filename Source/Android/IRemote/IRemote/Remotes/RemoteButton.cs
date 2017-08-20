using System;
namespace IRemote
{
	/// <summary>
	/// Remote button class
	/// </summary>
	public class RemoteButton
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:IRemote.RemoteButton"/> class.
		/// </summary>
		public RemoteButton() : this(0, "Unnamed", new IRSignal()) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:IRemote.RemoteButton"/> class.
		/// </summary>
		/// <param name="id">Identifier</param>
		public RemoteButton(int id) : this(id, "Unnamed", new IRSignal()) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:IRemote.RemoteButton"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="code">Code.</param>
		/// <param name="text">Text.</param>
		public RemoteButton(int id, string text) : this(id, text, new IRSignal()) { }

		public RemoteButton(int id, string text, IRSignal signal)
		{

			Text = text;
			ID = id;
			Signal = signal;
		}

		/// <summary>
		/// Gets or sets the text of button
		/// </summary>
		/// <value>The text</value>
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the identifier of button
		/// </summary>
		/// <value>The identifier.</value>
		public int ID { get; set; }
		public IRSignal Signal { get; set; }


	}
}
