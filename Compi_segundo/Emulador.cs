using System;

namespace Compi_segundo
{
	public partial class Emulador : Gtk.Window
	{
		public Emulador () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

