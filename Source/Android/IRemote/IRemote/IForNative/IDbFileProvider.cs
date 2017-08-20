using System;
namespace IRemote
{
	public interface IDbFileProvider
	{
		string GetLocalFilePath(string filename);
	}
}
