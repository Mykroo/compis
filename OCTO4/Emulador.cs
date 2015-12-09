using System;

namespace OCTO4
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

