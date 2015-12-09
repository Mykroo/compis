using System;
using System.IO;

namespace OCTO4
{
	public class Process
	{
		string ruta;
		public Process (string ruta)
		{
			this.ruta = ruta;
		}
		public Process()
		{

		}
		public string getRuta()
		{
			return this.ruta;
		}
		public void AnalizaArchivo()
		{
			/*if(File.Exists())
			{

			}*/
			StreamReader archivoOct = File.OpenText (getRuta());
			string contenido = archivoOct.ReadToEnd ();
			archivoOct.Close ();		
		}
	}
}

