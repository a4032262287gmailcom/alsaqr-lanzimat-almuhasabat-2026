using System.IO;
using System.Reflection;

namespace GTSErpSystem.Helper;

public class MetaData
{
	public static string GetMetaData()
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string name = "TXTextControl.DocumentServer.PDF.Zugferd.zugferd-metadata.xml";
		using Stream stream = executingAssembly.GetManifestResourceStream(name);
		using StreamReader streamReader = new StreamReader(stream);
		return streamReader.ReadToEnd();
	}
}


