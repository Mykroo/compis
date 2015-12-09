
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action ArchivoAction;
	private global::Gtk.Action GuardarAction;
	private global::Gtk.Action GuardarComoAction;
	private global::Gtk.Action AbrirAction;
	private global::Gtk.Action SalirAction;
	private global::Gtk.Action EditarAction;
	private global::Gtk.Action CopiarAction;
	private global::Gtk.Action CortarAction;
	private global::Gtk.Action PegarAction;
	private global::Gtk.Action BorrarAction;
	private global::Gtk.Action FuenteAction;
	private global::Gtk.Action newAction;
	private global::Gtk.Action openAction;
	private global::Gtk.Action saveAction;
	private global::Gtk.Action saveAsAction;
	private global::Gtk.Action closeAction2;
	private global::Gtk.Action closeAction;
	private global::Gtk.Action deleteAction;
	private global::Gtk.Action removeAction;
	private global::Gtk.Action dialogErrorAction;
	private global::Gtk.Action quitAction;
	private global::Gtk.Action closeAction1;
	private global::Gtk.Action stopAction;
	private global::Gtk.Action findAction;
	private global::Gtk.Action findAndReplaceAction;
	private global::Gtk.Action indentAction;
	private global::Gtk.Action mediaPlayAction;
	private global::Gtk.Action RunAction;
	private global::Gtk.Action CompilarAction;
	private global::Gtk.Action CompilarYCorrerAction;
	private global::Gtk.Action mediaPlayAction1;
	private global::Gtk.VBox vbox1;
	private global::Gtk.VBox vbox2;
	private global::Gtk.MenuBar menubar1;
	private global::Gtk.HBox hbox18;
	private global::Gtk.Toolbar toolbar1;
	private global::Gtk.VPaned vpaned1;
	private global::Gtk.HPaned hpaned3;
	private global::Gtk.HBox hbox19;
	private global::Gtk.Notebook NoteBook;
	private global::Gtk.Notebook notebook3;
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	private global::Gtk.TextView TreeLexico;
	private global::Gtk.Label label9;
	private global::Gtk.ScrolledWindow GtkScrolledWindow2;
	private global::Gtk.TextView TreeSinta;
	private global::Gtk.Label label12;
	private global::Gtk.ScrolledWindow GtkScrolledWindow3;
	private global::Gtk.TextView TreeSema;
	private global::Gtk.Label label13;
	private global::Gtk.ScrolledWindow GtkScrolledWindow6;
	private global::Gtk.TextView Hashtable;
	private global::Gtk.Label label1;
	private global::Gtk.VBox vbox3;
	private global::Gtk.ScrolledWindow GtkScrolledWindow4;
	private global::Gtk.TextView Intermedio;
	private global::Gtk.Label label14;
	private global::Gtk.Notebook notebook6;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView TreeErrores;
	private global::Gtk.Label label15;
	private global::Gtk.ScrolledWindow GtkScrolledWindow5;
	private global::Gtk.TextView TreeResultados;
	private global::Gtk.Label label16;
	private global::Gtk.Statusbar statusbar3;
	private global::Gtk.HBox hbox5;
	private global::Gtk.Label label18;
	private global::Gtk.Label Lineas;
	private global::Gtk.Label label20;
	private global::Gtk.HBox hbox6;
	private global::Gtk.Label label21;
	private global::Gtk.Label actual;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.ArchivoAction = new global::Gtk.Action ("ArchivoAction", global::Mono.Unix.Catalog.GetString ("Archivo"), null, null);
		this.ArchivoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Archivo");
		w1.Add (this.ArchivoAction, null);
		this.GuardarAction = new global::Gtk.Action ("GuardarAction", global::Mono.Unix.Catalog.GetString ("Guardar"), null, null);
		this.GuardarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Guardar");
		w1.Add (this.GuardarAction, null);
		this.GuardarComoAction = new global::Gtk.Action ("GuardarComoAction", global::Mono.Unix.Catalog.GetString ("Guardar Como"), null, null);
		this.GuardarComoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Guardar Como");
		w1.Add (this.GuardarComoAction, null);
		this.AbrirAction = new global::Gtk.Action ("AbrirAction", global::Mono.Unix.Catalog.GetString ("Abrir"), null, null);
		this.AbrirAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Abrir");
		w1.Add (this.AbrirAction, null);
		this.SalirAction = new global::Gtk.Action ("SalirAction", global::Mono.Unix.Catalog.GetString ("Salir"), null, null);
		this.SalirAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Salir");
		w1.Add (this.SalirAction, null);
		this.EditarAction = new global::Gtk.Action ("EditarAction", global::Mono.Unix.Catalog.GetString ("Editar"), null, null);
		this.EditarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Editar");
		w1.Add (this.EditarAction, null);
		this.CopiarAction = new global::Gtk.Action ("CopiarAction", global::Mono.Unix.Catalog.GetString ("Copiar"), null, null);
		this.CopiarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copiar");
		w1.Add (this.CopiarAction, null);
		this.CortarAction = new global::Gtk.Action ("CortarAction", global::Mono.Unix.Catalog.GetString ("Cortar"), null, null);
		this.CortarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Cortar");
		w1.Add (this.CortarAction, null);
		this.PegarAction = new global::Gtk.Action ("PegarAction", global::Mono.Unix.Catalog.GetString ("Pegar"), null, null);
		this.PegarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Pegar");
		w1.Add (this.PegarAction, null);
		this.BorrarAction = new global::Gtk.Action ("BorrarAction", global::Mono.Unix.Catalog.GetString ("Borrar"), null, null);
		this.BorrarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Borrar");
		w1.Add (this.BorrarAction, null);
		this.FuenteAction = new global::Gtk.Action ("FuenteAction", global::Mono.Unix.Catalog.GetString ("Fuente"), null, null);
		this.FuenteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Fuente");
		w1.Add (this.FuenteAction, null);
		this.newAction = new global::Gtk.Action ("newAction", null, null, "gtk-new");
		w1.Add (this.newAction, null);
		this.openAction = new global::Gtk.Action ("openAction", null, null, "gtk-open");
		w1.Add (this.openAction, null);
		this.saveAction = new global::Gtk.Action ("saveAction", null, null, "gtk-save");
		w1.Add (this.saveAction, null);
		this.saveAsAction = new global::Gtk.Action ("saveAsAction", null, null, "gtk-save-as");
		w1.Add (this.saveAsAction, null);
		this.closeAction2 = new global::Gtk.Action ("closeAction2", null, null, "gtk-close");
		w1.Add (this.closeAction2, null);
		this.closeAction = new global::Gtk.Action ("closeAction", null, null, "gtk-close");
		w1.Add (this.closeAction, null);
		this.deleteAction = new global::Gtk.Action ("deleteAction", null, null, "gtk-delete");
		w1.Add (this.deleteAction, null);
		this.removeAction = new global::Gtk.Action ("removeAction", null, null, "gtk-remove");
		w1.Add (this.removeAction, null);
		this.dialogErrorAction = new global::Gtk.Action ("dialogErrorAction", null, null, "gtk-dialog-error");
		w1.Add (this.dialogErrorAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", null, null, "gtk-quit");
		w1.Add (this.quitAction, null);
		this.closeAction1 = new global::Gtk.Action ("closeAction1", null, null, "gtk-close");
		w1.Add (this.closeAction1, null);
		this.stopAction = new global::Gtk.Action ("stopAction", null, null, "gtk-stop");
		w1.Add (this.stopAction, null);
		this.findAction = new global::Gtk.Action ("findAction", null, null, "gtk-find");
		w1.Add (this.findAction, null);
		this.findAndReplaceAction = new global::Gtk.Action ("findAndReplaceAction", null, null, "gtk-find-and-replace");
		w1.Add (this.findAndReplaceAction, null);
		this.indentAction = new global::Gtk.Action ("indentAction", null, null, "gtk-indent");
		w1.Add (this.indentAction, null);
		this.mediaPlayAction = new global::Gtk.Action ("mediaPlayAction", null, null, "gtk-media-play");
		w1.Add (this.mediaPlayAction, null);
		this.RunAction = new global::Gtk.Action ("RunAction", global::Mono.Unix.Catalog.GetString ("Run"), null, null);
		this.RunAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Run");
		w1.Add (this.RunAction, null);
		this.CompilarAction = new global::Gtk.Action ("CompilarAction", global::Mono.Unix.Catalog.GetString ("Compilar"), null, null);
		this.CompilarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Compilar");
		w1.Add (this.CompilarAction, null);
		this.CompilarYCorrerAction = new global::Gtk.Action ("CompilarYCorrerAction", global::Mono.Unix.Catalog.GetString ("Compilar y correr"), null, null);
		this.CompilarYCorrerAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Compilar y correr");
		w1.Add (this.CompilarYCorrerAction, null);
		this.mediaPlayAction1 = new global::Gtk.Action ("mediaPlayAction1", null, null, "gtk-media-play");
		w1.Add (this.mediaPlayAction1, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Compilador M&M  ++**--");
		this.Icon = global::Stetic.IconLoader.LoadIcon (this, "stock_smiley-22", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.Gravity = ((global::Gdk.Gravity)(5));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='ArchivoAction' action='ArchivoAction'><menuitem name='GuardarAction' action='GuardarAction'/><menuitem name='GuardarComoAction' action='GuardarComoAction'/><menuitem name='AbrirAction' action='AbrirAction'/><menuitem name='SalirAction' action='SalirAction'/></menu><menu name='EditarAction' action='EditarAction'><menuitem name='CopiarAction' action='CopiarAction'/><menuitem name='CortarAction' action='CortarAction'/><menuitem name='PegarAction' action='PegarAction'/><menuitem name='BorrarAction' action='BorrarAction'/><menuitem name='FuenteAction' action='FuenteAction'/></menu><menu name='RunAction' action='RunAction'><menuitem name='CompilarAction' action='CompilarAction'/><menuitem name='mediaPlayAction1' action='mediaPlayAction1'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox2.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox18 = new global::Gtk.HBox ();
		this.hbox18.Name = "hbox18";
		this.hbox18.Spacing = 6;
		// Container child hbox18.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar1'><toolitem name='newAction' action='newAction'/><toolitem name='openAction' action='openAction'/><toolitem name='saveAction' action='saveAction'/><toolitem name='saveAsAction' action='saveAsAction'/><toolitem name='closeAction2' action='closeAction2'/><toolitem name='stopAction' action='stopAction'/><toolitem name='mediaPlayAction' action='mediaPlayAction'/></toolbar></ui>");
		this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar1")));
		this.toolbar1.Name = "toolbar1";
		this.toolbar1.ShowArrow = false;
		this.hbox18.Add (this.toolbar1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox18 [this.toolbar1]));
		w3.Position = 0;
		this.vbox2.Add (this.hbox18);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox18]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		this.vbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vbox2]));
		w5.Position = 0;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.vpaned1 = new global::Gtk.VPaned ();
		this.vpaned1.CanFocus = true;
		this.vpaned1.Name = "vpaned1";
		this.vpaned1.Position = 475;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.hpaned3 = new global::Gtk.HPaned ();
		this.hpaned3.CanFocus = true;
		this.hpaned3.Name = "hpaned3";
		this.hpaned3.Position = 1056;
		// Container child hpaned3.Gtk.Paned+PanedChild
		this.hbox19 = new global::Gtk.HBox ();
		this.hbox19.Name = "hbox19";
		this.hbox19.Spacing = 6;
		// Container child hbox19.Gtk.Box+BoxChild
		this.NoteBook = new global::Gtk.Notebook ();
		this.NoteBook.CanFocus = true;
		this.NoteBook.Name = "NoteBook";
		this.NoteBook.CurrentPage = 0;
		this.hbox19.Add (this.NoteBook);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox19 [this.NoteBook]));
		w6.Position = 0;
		this.hpaned3.Add (this.hbox19);
		global::Gtk.Paned.PanedChild w7 = ((global::Gtk.Paned.PanedChild)(this.hpaned3 [this.hbox19]));
		w7.Resize = false;
		// Container child hpaned3.Gtk.Paned+PanedChild
		this.notebook3 = new global::Gtk.Notebook ();
		this.notebook3.CanFocus = true;
		this.notebook3.Name = "notebook3";
		this.notebook3.CurrentPage = 4;
		// Container child notebook3.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.TreeLexico = new global::Gtk.TextView ();
		this.TreeLexico.CanFocus = true;
		this.TreeLexico.Name = "TreeLexico";
		this.TreeLexico.Editable = false;
		this.TreeLexico.CursorVisible = false;
		this.GtkScrolledWindow1.Add (this.TreeLexico);
		this.notebook3.Add (this.GtkScrolledWindow1);
		// Notebook tab
		this.label9 = new global::Gtk.Label ();
		this.label9.Name = "label9";
		this.label9.LabelProp = global::Mono.Unix.Catalog.GetString ("Lexico");
		this.notebook3.SetTabLabel (this.GtkScrolledWindow1, this.label9);
		this.label9.ShowAll ();
		// Container child notebook3.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.TreeSinta = new global::Gtk.TextView ();
		this.TreeSinta.CanFocus = true;
		this.TreeSinta.Name = "TreeSinta";
		this.TreeSinta.Editable = false;
		this.TreeSinta.CursorVisible = false;
		this.GtkScrolledWindow2.Add (this.TreeSinta);
		this.notebook3.Add (this.GtkScrolledWindow2);
		global::Gtk.Notebook.NotebookChild w11 = ((global::Gtk.Notebook.NotebookChild)(this.notebook3 [this.GtkScrolledWindow2]));
		w11.Position = 1;
		// Notebook tab
		this.label12 = new global::Gtk.Label ();
		this.label12.Name = "label12";
		this.label12.LabelProp = global::Mono.Unix.Catalog.GetString ("Sintactico");
		this.notebook3.SetTabLabel (this.GtkScrolledWindow2, this.label12);
		this.label12.ShowAll ();
		// Container child notebook3.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
		this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
		this.TreeSema = new global::Gtk.TextView ();
		this.TreeSema.CanFocus = true;
		this.TreeSema.Name = "TreeSema";
		this.TreeSema.Editable = false;
		this.TreeSema.CursorVisible = false;
		this.GtkScrolledWindow3.Add (this.TreeSema);
		this.notebook3.Add (this.GtkScrolledWindow3);
		global::Gtk.Notebook.NotebookChild w13 = ((global::Gtk.Notebook.NotebookChild)(this.notebook3 [this.GtkScrolledWindow3]));
		w13.Position = 2;
		// Notebook tab
		this.label13 = new global::Gtk.Label ();
		this.label13.Name = "label13";
		this.label13.LabelProp = global::Mono.Unix.Catalog.GetString ("Semantico");
		this.notebook3.SetTabLabel (this.GtkScrolledWindow3, this.label13);
		this.label13.ShowAll ();
		// Container child notebook3.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow6 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow6.Name = "GtkScrolledWindow6";
		this.GtkScrolledWindow6.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow6.Gtk.Container+ContainerChild
		this.Hashtable = new global::Gtk.TextView ();
		this.Hashtable.CanFocus = true;
		this.Hashtable.Name = "Hashtable";
		this.GtkScrolledWindow6.Add (this.Hashtable);
		this.notebook3.Add (this.GtkScrolledWindow6);
		global::Gtk.Notebook.NotebookChild w15 = ((global::Gtk.Notebook.NotebookChild)(this.notebook3 [this.GtkScrolledWindow6]));
		w15.Position = 3;
		// Notebook tab
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Hashtable");
		this.notebook3.SetTabLabel (this.GtkScrolledWindow6, this.label1);
		this.label1.ShowAll ();
		// Container child notebook3.Gtk.Notebook+NotebookChild
		this.vbox3 = new global::Gtk.VBox ();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.GtkScrolledWindow4 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow4.Name = "GtkScrolledWindow4";
		this.GtkScrolledWindow4.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow4.Gtk.Container+ContainerChild
		this.Intermedio = new global::Gtk.TextView ();
		this.Intermedio.CanFocus = true;
		this.Intermedio.Name = "Intermedio";
		this.Intermedio.Editable = false;
		this.GtkScrolledWindow4.Add (this.Intermedio);
		this.vbox3.Add (this.GtkScrolledWindow4);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow4]));
		w17.Position = 1;
		this.notebook3.Add (this.vbox3);
		global::Gtk.Notebook.NotebookChild w18 = ((global::Gtk.Notebook.NotebookChild)(this.notebook3 [this.vbox3]));
		w18.Position = 4;
		// Notebook tab
		this.label14 = new global::Gtk.Label ();
		this.label14.Name = "label14";
		this.label14.LabelProp = global::Mono.Unix.Catalog.GetString ("Cod Intermedio");
		this.notebook3.SetTabLabel (this.vbox3, this.label14);
		this.label14.ShowAll ();
		this.hpaned3.Add (this.notebook3);
		this.vpaned1.Add (this.hpaned3);
		global::Gtk.Paned.PanedChild w20 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.hpaned3]));
		w20.Resize = false;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.notebook6 = new global::Gtk.Notebook ();
		this.notebook6.CanFocus = true;
		this.notebook6.Name = "notebook6";
		this.notebook6.CurrentPage = 0;
		// Container child notebook6.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.TreeErrores = new global::Gtk.TreeView ();
		this.TreeErrores.CanFocus = true;
		this.TreeErrores.Name = "TreeErrores";
		this.GtkScrolledWindow.Add (this.TreeErrores);
		this.notebook6.Add (this.GtkScrolledWindow);
		// Notebook tab
		this.label15 = new global::Gtk.Label ();
		this.label15.Name = "label15";
		this.label15.LabelProp = global::Mono.Unix.Catalog.GetString ("Errores");
		this.notebook6.SetTabLabel (this.GtkScrolledWindow, this.label15);
		this.label15.ShowAll ();
		// Container child notebook6.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow5 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow5.Name = "GtkScrolledWindow5";
		this.GtkScrolledWindow5.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow5.Gtk.Container+ContainerChild
		this.TreeResultados = new global::Gtk.TextView ();
		this.TreeResultados.CanFocus = true;
		this.TreeResultados.Name = "TreeResultados";
		this.TreeResultados.Editable = false;
		this.TreeResultados.CursorVisible = false;
		this.GtkScrolledWindow5.Add (this.TreeResultados);
		this.notebook6.Add (this.GtkScrolledWindow5);
		global::Gtk.Notebook.NotebookChild w24 = ((global::Gtk.Notebook.NotebookChild)(this.notebook6 [this.GtkScrolledWindow5]));
		w24.Position = 1;
		// Notebook tab
		this.label16 = new global::Gtk.Label ();
		this.label16.Name = "label16";
		this.label16.LabelProp = global::Mono.Unix.Catalog.GetString ("Resultados");
		this.notebook6.SetTabLabel (this.GtkScrolledWindow5, this.label16);
		this.label16.ShowAll ();
		this.vpaned1.Add (this.notebook6);
		this.vbox1.Add (this.vpaned1);
		global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vpaned1]));
		w26.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar3 = new global::Gtk.Statusbar ();
		this.statusbar3.Name = "statusbar3";
		this.statusbar3.Spacing = 6;
		// Container child statusbar3.Gtk.Box+BoxChild
		this.hbox5 = new global::Gtk.HBox ();
		this.hbox5.Name = "hbox5";
		this.hbox5.Spacing = 6;
		// Container child hbox5.Gtk.Box+BoxChild
		this.label18 = new global::Gtk.Label ();
		this.label18.Name = "label18";
		this.label18.LabelProp = global::Mono.Unix.Catalog.GetString ("Lineas Totales: ");
		this.hbox5.Add (this.label18);
		global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.label18]));
		w27.Position = 0;
		w27.Expand = false;
		w27.Fill = false;
		// Container child hbox5.Gtk.Box+BoxChild
		this.Lineas = new global::Gtk.Label ();
		this.Lineas.Name = "Lineas";
		this.hbox5.Add (this.Lineas);
		global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.Lineas]));
		w28.Position = 1;
		w28.Expand = false;
		w28.Fill = false;
		// Container child hbox5.Gtk.Box+BoxChild
		this.label20 = new global::Gtk.Label ();
		this.label20.Name = "label20";
		this.label20.LabelProp = global::Mono.Unix.Catalog.GetString ("----------");
		this.hbox5.Add (this.label20);
		global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.label20]));
		w29.Position = 2;
		w29.Expand = false;
		w29.Fill = false;
		this.statusbar3.Add (this.hbox5);
		global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.statusbar3 [this.hbox5]));
		w30.Position = 1;
		w30.Expand = false;
		w30.Fill = false;
		// Container child statusbar3.Gtk.Box+BoxChild
		this.hbox6 = new global::Gtk.HBox ();
		this.hbox6.Name = "hbox6";
		this.hbox6.Spacing = 6;
		// Container child hbox6.Gtk.Box+BoxChild
		this.label21 = new global::Gtk.Label ();
		this.label21.Name = "label21";
		this.label21.LabelProp = global::Mono.Unix.Catalog.GetString ("Linea Actual: ");
		this.hbox6.Add (this.label21);
		global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.label21]));
		w31.Position = 0;
		w31.Expand = false;
		w31.Fill = false;
		// Container child hbox6.Gtk.Box+BoxChild
		this.actual = new global::Gtk.Label ();
		this.actual.Name = "actual";
		this.hbox6.Add (this.actual);
		global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.actual]));
		w32.Position = 1;
		w32.Expand = false;
		w32.Fill = false;
		this.statusbar3.Add (this.hbox6);
		global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.statusbar3 [this.hbox6]));
		w33.Position = 2;
		w33.Expand = false;
		w33.Fill = false;
		this.vbox1.Add (this.statusbar3);
		global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar3]));
		w34.Position = 2;
		w34.Expand = false;
		w34.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1544;
		this.DefaultHeight = 771;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.GuardarAction.Activated += new global::System.EventHandler (this.OnGuardarActionActivated);
		this.GuardarComoAction.Activated += new global::System.EventHandler (this.OnGuardarComoActionActivated);
		this.AbrirAction.Activated += new global::System.EventHandler (this.OnAbrirActionActivated);
		this.SalirAction.Activated += new global::System.EventHandler (this.OnSalirActionActivated);
		this.CopiarAction.Activated += new global::System.EventHandler (this.OnCopiarActionActivated);
		this.CortarAction.Activated += new global::System.EventHandler (this.OnCortarActionActivated);
		this.PegarAction.Activated += new global::System.EventHandler (this.OnPegarActionActivated);
		this.BorrarAction.Activated += new global::System.EventHandler (this.OnBorrarActionActivated);
		this.FuenteAction.Activated += new global::System.EventHandler (this.OnFuenteActionActivated);
		this.newAction.Activated += new global::System.EventHandler (this.OnNewActionActivated);
		this.openAction.Activated += new global::System.EventHandler (this.OnOpenActionActivated);
		this.saveAction.Activated += new global::System.EventHandler (this.OnSaveActionActivated);
		this.saveAsAction.Activated += new global::System.EventHandler (this.OnSaveAsActionActivated);
		this.closeAction2.Activated += new global::System.EventHandler (this.OnNoActionActivated);
		this.stopAction.Activated += new global::System.EventHandler (this.OnDialogErrorAction1Activated);
		this.findAndReplaceAction.Activated += new global::System.EventHandler (this.OnFindAndReplaceActionActivated);
		this.indentAction.Activated += new global::System.EventHandler (this.OnIndentActionActivated);
		this.mediaPlayAction.Activated += new global::System.EventHandler (this.OnExecuteActionActivated);
	}
}
