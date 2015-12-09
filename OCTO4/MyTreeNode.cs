using System;

namespace OCTO4
{
	public class MyTreeNode: Gtk.TreeNode
	{
			//int linea;
			//string error;
			public MyTreeNode (int linea, string error)
			{
				this.linea = linea;
				this.error = error;
			}

			[Gtk.TreeNodeValue (Column=0)]
			public int linea;

			[Gtk.TreeNodeValue (Column=1)]
			public string error;
	}
}

