using System;
using System.IO;
using Gtk;
using System.Collections.Generic;
using System.Collections;


public partial class MainWindow: Gtk.Window
{
	bool enUso,estaBuscando;
	Gtk.ListStore ErroresListStore;
	Hashtable Variables;
	Stack<OCTO4.TipoDato> pila;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		CreateNodeViewErrores ();
		enUso = false;
		estaBuscando = true;
		Button1.ModifyBg(StateType.Normal,new Gdk.Color(225,225,225));
		Button2.ModifyBg(StateType.Normal,new Gdk.Color(240,187,33));
	}
	// Extrae el nombre de un archivo en una ruta
	public string getNombre(string ruta)
	{
		char[] letra;
		int cont;
		string nombre = "";

		letra = ruta.ToCharArray ();
		cont = letra.Length;
		int apuntador = -1;
		for (int i = letra.Length-1; i > 0; i--) 
		{
			if (letra [i] != '/') {
			} else {
				apuntador = i+1;
				i = 0;
			}
		}
		if(apuntador != -1)
		{
			for (int i = apuntador; i <ruta.Length ; i++) 
			{
				nombre += letra [i];
			}
		}

		return nombre;
	}
	public void Nuevo()
	{
		Console.WriteLine ("Nuevo");
		Gtk.FileChooserDialog filechooser =
			new Gtk.FileChooserDialog ("Crear",
				this,
				FileChooserAction.Save,
				"Cancelar", ResponseType.Cancel,
				"Crear", ResponseType.Accept);

		if (filechooser.Run () == (int)ResponseType.Accept) {		
			Console.WriteLine (filechooser.Filename + "= creado: "+filechooser.Name);

			StreamWriter SW = new StreamWriter (filechooser.Filename );
			SW.Close ();

			Label tabLabel = new Label (getNombre (filechooser.Filename));
			OCTO4.TextWidget nuevo = new OCTO4.TextWidget();

			nuevo.setRuta (filechooser.Filename);
			nuevo.setNombre (tabLabel.Text);
			NoteBook.AppendPage (nuevo,tabLabel);
			NoteBook.ShowAll ();
		}
		filechooser.Destroy ();	
	}
	public void Guardar()
	{
		int a;
		if (!TapExist()) {
			Console.WriteLine ("NO!! asi no puedes guardar");
			return;
		}
		a = NoteBook.CurrentPage ;
		StreamWriter writer = new StreamWriter (getCurrentTabPath());
		// Rewrite the entire value of s to the file
		writer.Write (getCurrentTabText());
		// Close the writer
		writer.Close ();
		LimpiaArchivos ();
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.setModificado (false);
	}
	public void GuargarTodo()
	{
		int a;
		a = NoteBook.CurrentPage;
		while (a != -1) 
		{
			OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
			if (widget.isModificado ()) {
				Console.WriteLine ("Se guardo se Cerro la pestania, pero " +
					"debe preguntar si quiere guardar cambios");
				Guardar ();
				NoteBook.RemovePage (a);
			} else {
				NoteBook.RemovePage (a);
			}
			LimpiaArchivos ();
			a = NoteBook.CurrentPage;
		}
	}
	public void LimpiaArchivos(){
		TreeLexico.Buffer.Text = "";
		TreeSinta.Buffer.Text = "";
		TreeSema.Buffer.Text = "";
		Hashtable.Buffer.Text = "";
		TreeResultados.Buffer.Text = "";
		ErroresListStore.Clear ();
	}
	public void GuardarComo()
	{
		if (!TapExist()) {
			Console.WriteLine ("NO!! asi no puedes guardar como");
			return;
		}
		Gtk.FileChooserDialog filechooser =
			new Gtk.FileChooserDialog ("Guardar como",
				this,
				FileChooserAction.Save,
				"Cancelar", ResponseType.Cancel,
				"Crear", ResponseType.Accept);

		if (filechooser.Run () == (int)ResponseType.Accept) {
			StreamWriter SW = new StreamWriter (filechooser.Filename);				
			SW.Write (getCurrentTabText());
			SW.Close ();
		}
		filechooser.Destroy ();
	}
	public string getCurrentTabText()
	{
		int a;
		a = NoteBook.CurrentPage ;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		return widget.getText ();
	}
	public string getCurrentTabName()
	{
		int a;
		a = NoteBook.CurrentPage ;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		return widget.getNombre();
	}
	public string getCurrentTabPath()
	{
		int a;
		a = NoteBook.CurrentPage ;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		return widget.getRuta();
	}
	public bool TapExist()
	{
		int a;
		a = NoteBook.CurrentPage ;
		if(a == -1){
			return false;
		}
		return true;
	}
	public void Abrir()
	{
		FileChooserDialog filechooser =
			new FileChooserDialog ("Abrir...",
				this,
				FileChooserAction.Open,
				"Cancel", ResponseType.Cancel,
				"Open", ResponseType.Accept);

		if (filechooser.Run () == (int)ResponseType.Accept) {
			FileStream file = File.OpenRead (filechooser.Filename);
			file.Close ();

			StreamReader archivoOct = File.OpenText (file.Name);
			string contenido = archivoOct.ReadToEnd ();
			archivoOct.Close ();

			// Creando pestania y pintar las palabras
			Label tabLabel = new Label (getNombre (filechooser.Filename));
			OCTO4.TextWidget nuevo = new OCTO4.TextWidget();

			nuevo.setRuta (filechooser.Filename);
			nuevo.setNombre (tabLabel.Text);
			nuevo.setText (contenido);
			nuevo.pintarTodo ();

			NoteBook.AppendPage (nuevo,tabLabel);
			NoteBook.ShowAll ();
		}
		filechooser.Destroy ();
	}
	public void CreateNodeViewErrores()
	{
		Gtk.TreeViewColumn LineColumn = new Gtk.TreeViewColumn ();
		LineColumn.Title = "Linea";

		// Adding a cellrender
		CellRendererText LineaNameCell = new Gtk.CellRendererText ();
		LineColumn.PackStart (LineaNameCell, true);

		// Create a column for the song title
		Gtk.TreeViewColumn ErrorColumn = new Gtk.TreeViewColumn ();
		ErrorColumn.Title = "Error";

		CellRendererText ErrorNameCell = new Gtk.CellRendererText ();
		ErrorColumn.PackStart (ErrorNameCell, true);

		// Add the columns to the TreeView
		TreeErrores.AppendColumn (LineColumn);
		TreeErrores.AppendColumn (ErrorColumn);

		// Tell cell renderers which item in the model to desplay
		LineColumn.AddAttribute (LineaNameCell, "text", 0);
		ErrorColumn.AddAttribute (ErrorNameCell, "text", 1);

		// Create a model that will hold two strings - Artist Name and Song Title
		ErroresListStore = new Gtk.ListStore (typeof (int), typeof (string));

		// Assign the model to the TreeView
		TreeErrores.Model = ErroresListStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		Nuevo ();
	}

	protected void OnOpenActionActivated (object sender, EventArgs e)
	{
		Abrir ();
	}
	protected void OnSaveActionActivated (object sender, EventArgs e)
	{
		Guardar ();
	}
	protected void OnSaveAsActionActivated (object sender, EventArgs e)
	{
		GuardarComo ();
	}
	protected void OnNoActionActivated (object sender, EventArgs e)
	{
		int a;
		a = NoteBook.CurrentPage;
		if (a != -1) {
			OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
			if (widget.isModificado ()) {
				Console.WriteLine ("Se guardo se Cerro la pestania, pero " +
				"debe preguntar si quiere guardar cambios");
				Guardar ();
				ErroresListStore.Clear ();
				NoteBook.RemovePage (a);
			} else {
				ErroresListStore.Clear ();
				NoteBook.RemovePage (a);
			}
			LimpiaArchivos ();
		}
	}
	protected void OnDialogErrorAction1Activated (object sender, EventArgs e)
	{
		ErroresListStore.Clear();
		GuargarTodo ();
	}

	protected void OnFindAndReplaceActionActivated (object sender, EventArgs e)
	{

	}
	protected void OnIndentActionActivated (object sender, EventArgs e)
	{
		Console.WriteLine ("identar");
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.Identar();
	}
	protected void OnExecuteActionActivated (object sender, EventArgs e)
	{
		ErroresListStore.Clear();
		LimpiaArchivos ();
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;

		OCTO4.Nodo nodo;
		OCTO4.TextWidget widget;
		OCTO4.Lexico tokens;
		OCTO4.Sintactico sintactico;
		OCTO4.Semantico semantico;
		OCTO4.GeneraCodigo generaCodigo;

		widget =(OCTO4.TextWidget)NoteBook.Children[a];

		tokens = new OCTO4.Lexico (widget.getText(),ErroresListStore);
		sintactico = new OCTO4.Sintactico (tokens.Tokenize(),tokens.GetListStore());
		semantico = new OCTO4.Semantico (sintactico.GetListStore());
		nodo = sintactico.creaArbol ();
		generaCodigo = new OCTO4.GeneraCodigo();

		if(nodo != null)
		{
			semantico.AnalizaDeclaraciones (nodo.hijos[0]);
			semantico.analisaSintaxis (nodo.hijos[1]);
			Intermedio.Buffer.Text = generaCodigo.GetCodigo (nodo.hijos[1],semantico.getHashTable());

			// Mostrando los resultados en archivos de texto

			TreeErrores.Model = semantico.GetListStore ();
			TreeErrores.ShowAll ();
			TreeLexico.Buffer.Text = sintactico.getArchivoTokes ();
			TreeSinta.Buffer.Text = sintactico.ArchivoArbol ();
			TreeSema.Buffer.Text = semantico.getArchivoArbolSeman (nodo.hijos[1]);
			Hashtable.Buffer.Text = semantico.GetHachValues ();

		}
		TreeResultados.Buffer.Text = "";
		Variables = new Hashtable ();
		pila = new Stack<OCTO4.TipoDato> ();
		Simular ();

	}
	protected void OnGuardarActionActivated (object sender, EventArgs e)
	{
		Guardar ();
	}
	protected void OnGuardarComoActionActivated (object sender, EventArgs e)
	{
		GuardarComo ();
	}
	protected void OnAbrirActionActivated (object sender, EventArgs e)
	{
		Abrir ();
	}
	protected void OnSalirActionActivated (object sender, EventArgs e)
	{
		Application.Quit ();
	}		


	protected void OnCopiarActionActivated (object sender, EventArgs e)
	{
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.Copiar ();
	}
	protected void OnCortarActionActivated (object sender, EventArgs e)
	{
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.Cortar ();
	}
	protected void OnPegarActionActivated (object sender, EventArgs e)
	{
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.Pegar ();
		widget.pintarTodo ();
	}
	protected void OnBorrarActionActivated (object sender, EventArgs e)
	{
		int a;
		if (!TapExist ()){return;}
		a = NoteBook.CurrentPage;
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.Eliminar ();
		widget.pintarTodo ();
	}
	protected void OnFuenteActionActivated (object sender, EventArgs e)
	{
		int a;
		a = NoteBook.CurrentPage;
		if (a == -1) 
		{
			return;
		}
		OCTO4.TextWidget widget =(OCTO4.TextWidget)NoteBook.Children[a];
		widget.etiquetas ("red","red","red","red","red",true);
		widget.pintarTodo ();
	}

	protected void OnButton1Activated (object sender, EventArgs e)
	{}protected void OnButton2Activated (object sender, EventArgs e)
	{}protected void OnButton3Activated (object sender, EventArgs e)
	{}protected void OnButton1DeleteEvent (object o, DeleteEventArgs args)
	{}protected void OnButton1ButtonReleaseEvent (object o, ButtonReleaseEventArgs args)
	{}
	protected void OnButton1Clicked (object sender, EventArgs e)
	{
		Intermedio.Editable = true;
		Button1.ModifyBg(StateType.Normal,new Gdk.Color(240,187,33));
		Button2.ModifyBg(StateType.Normal,new Gdk.Color(225,225,225));
	}
	protected void OnButton2Clicked (object sender, EventArgs e)
	{
		Intermedio.Editable = false;
		Button1.ModifyBg(StateType.Normal,new Gdk.Color(225,225,225));
		Button2.ModifyBg(StateType.Normal,new Gdk.Color(240,187,33));
	}
	protected void OnButton3Clicked (object sender, EventArgs e)
	{

		// Falta validar, si hay errores, no debe ejecutarce
		TreeResultados.Buffer.Text = "";
		Variables = new Hashtable ();
		pila = new Stack<OCTO4.TipoDato> ();
		Simular ();
	}
	public void Simular ()
	{
		int linea = 0;
		string instruccion = "";
		OCTO4.TipoDato objeto;
		Variables = new Hashtable ();
		pila = new Stack<OCTO4.TipoDato> ();
		instruccion = NextLine (linea);
		while (instruccion != "EOFF" && existeInstruccion (instruccion)) {
			//BEGIN of safe zone

			string[] cant = instruccion.Split (' ');
			switch (cant [0]) {
			case "VARE":
				Variables.Add (cant [1], new OCTO4.TipoDato (cant [1], "null", "int"));
				break;
			case "VARF":
				Variables.Add (cant [1], new OCTO4.TipoDato (cant [1], "null", "float"));
				break;
			case "CGVE":
				pila.Push ((OCTO4.TipoDato)Variables [cant [1]]);
				break;
			case "CGVF":
				pila.Push ((OCTO4.TipoDato)Variables [cant [1]]);
				break;
			case "CGE":
				pila.Push (new OCTO4.TipoDato (cant [1], cant [1], "int"));
				break;
			case "CGF":
				pila.Push (new OCTO4.TipoDato (cant [1], cant [1], "float"));
				break;
			case "WR":
				TreeResultados.Buffer.Text += DameValor () + "\n";
				break;
			case "NOT":
				if (pila.Peek () != null) {
					if (pila.Peek ().GetValor () == "null") {
						TreeResultados.Buffer.Text += "Error var "
							+ pila.Peek ().GetName () + " nullo\n";
						return;
					}
					int not = Int32.Parse (pila.Pop ().GetValor ());

					if (not == 1) {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "1", "int"));
					}
				}
				break;
			case "ST":
				if (pila.Count >= 1) {

					OCTO4.TipoDato cpy = (OCTO4.TipoDato)Variables [cant [1]];
					cpy.setvalor (DameValor ());
					Variables.Remove (cant [1]);
					Variables.Add (cpy.GetName (), cpy);
				}
				break;
			case "AND":
				if (pila.Peek () != null) {
					if (pila.Peek ().GetValor () == "null") {
						TreeResultados.Buffer.Text += "Error var "
							+ pila.Peek ().GetName () + " nullo\n";
						return;
					}
					int valuno = Int32.Parse (pila.Pop ().GetValor ());

					if (pila.Peek ().GetValor () == "null") {
						TreeResultados.Buffer.Text += "Error var "
							+ pila.Peek ().GetName () + " nullo\n";
						return;
					}
					int valDos = Int32.Parse (pila.Pop ().GetValor ());

					if (valDos == 1 && valuno == 1) {
						pila.Push (new OCTO4.TipoDato ("0", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "OR":
				if (pila.Peek () != null) {
					if (pila.Peek ().GetValor () == "null") {
						TreeResultados.Buffer.Text += "Error var "
							+ pila.Peek ().GetName () + " nullo\n";
						return;
					}
					int valuno = Int32.Parse (pila.Pop ().GetValor ());

					if (pila.Peek ().GetValor () == "null") {
						TreeResultados.Buffer.Text += "Error var "
							+ pila.Peek ().GetName () + " nullo\n";
						return;
					}
					int valDos = Int32.Parse (pila.Pop ().GetValor ());

					if (valDos == 0 && valuno == 0) {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "1", "int"));
					}
				}
				break;
			case "M":
				if (pila.Count >= 2) {
					if (pila.Peek ().GetTipo () == "float") {
						float a, b;
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						a = Single.Parse (DameValor ());

						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						b = Single.Parse (DameValor ());

						pila.Push (new OCTO4.TipoDato ((a * b) + "", (a * b) + "", "float"));
					} else {
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						int fA = Int32.Parse (DameValor ());

						if (pila.Peek ().GetTipo () == "float") {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							float fB = Single.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((fA * fB) + "", (fA * fB) + "", "float"));
						} else {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							int intB = Int32.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((fA * intB) + "", (fA * intB) + "", "int"));
						}
					}
				} else {
					// Error
					return;
				}
				break;	
			case "R":
				if (pila.Count >= 2) {
					if (pila.Peek ().GetTipo () == "float") {
						float a, b;
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						a = Single.Parse (DameValor ());

						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						b = Single.Parse (DameValor ());

						pila.Push (new OCTO4.TipoDato ((b - a) + "", (b - a) + "", "float"));
					} else {
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						int fA = Int32.Parse (DameValor ());

						if (pila.Peek ().GetTipo () == "float") {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							float fB = Single.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((fB - fA) + "", (fB - fA) + "", "float"));
						} else {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							int intB = Int32.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((intB - fA) + "", (intB - fA) + "", "int"));
						}
					}
				} else {
					// Error
					return;
				}
				break;	
			case "SM":
				if (pila.Count >= 2) {
					if (pila.Peek ().GetTipo () == "float") {
						float a, b;

						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						a = Single.Parse (DameValor ());

						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						b = Single.Parse (DameValor ());

						pila.Push (new OCTO4.TipoDato ((b + a) + "", (b + a) + "", "float"));
					} else {
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var " +
								pila.Peek ().GetName () + " nullo\n";
							return;
						}
						int fA = Int32.Parse (DameValor ());

						if (pila.Peek ().GetTipo () == "float") {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							float fB = Single.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((fB + fA) + "", (fB + fA) + "", "float"));
						} else {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							int intB = Int32.Parse (DameValor ());

							pila.Push (new OCTO4.TipoDato ((intB + fA) + "", (intB + fA) + "", "int"));
						}
					}
				} else {
					// Error
					return;
				}
				break;
			case "D":
				if (pila.Count >= 2) {
					if (pila.Peek ().GetTipo () == "float") {
						float a, b;
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						a = Single.Parse (DameValor ());

						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						b = Single.Parse (DameValor ());

						if (a == 0) {
							TreeResultados.Buffer.Text += "Error: divicion entre cero\n";
							return;
						}
						pila.Push (new OCTO4.TipoDato ((b / a) + "", (b / a) + "", "float"));
					} else {
						if (pila.Peek ().GetValor () == "null") {
							TreeResultados.Buffer.Text += "Error var "
								+ pila.Peek ().GetName () + " nullo\n";
							return;
						}
						int fA = Int32.Parse (DameValor ());

						if (pila.Peek ().GetTipo () == "float") {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							float fB = Single.Parse (DameValor ());

							if (fA == 0) {
								TreeResultados.Buffer.Text += "Error: divicion entre cero\n";
								return;
							}
							pila.Push (new OCTO4.TipoDato ((fB / fA) + "", (fB / fA) + "", "float"));
						} else {
							if (pila.Peek ().GetValor () == "null") {
								TreeResultados.Buffer.Text += "Error var "
									+ pila.Peek ().GetName () + " nullo\n";
								return;
							}
							int intB = Int32.Parse (DameValor ());

							if (fA == 0) {
								TreeResultados.Buffer.Text += "Error: divicion entre cero\n";
								return;
							}
							pila.Push (new OCTO4.TipoDato ((intB / fA) + "", (intB / fA) + "", "int"));
						}
					}
				} else {
					// Error
					return;
				}
				break;
			case "RIDVE":
				String entero = "entero";
				MessageDialog dialogo; new MessageDialog (null, 0, MessageType.Question, ButtonsType.Ok, "Entero: " + cant [1]);			
				Entry entry = new Entry ();
				int aux_comp;
				while (!Int32.TryParse(entero,out aux_comp)){
					dialogo= new MessageDialog (null, 0, MessageType.Question, ButtonsType.Ok, "Entero: " + cant [1]);			
					dialogo.ActionArea.PackStart (entry);
					dialogo.ShowAll ();
					dialogo.Run ();
					entero = entry.Text;
					Variables [cant [1]] = new OCTO4.TipoDato (cant [1], entero, "int");
					dialogo.Destroy (); 
				}
				Console.WriteLine ("UpS");
				break;
			case "RIDVF":
				double aux_flt;
				MessageDialog diaF; //new MessageDialog (null, 0, MessageType.Question, ButtonsType.Ok, "Flotante " + cant [1] + ": ");			
				entry = new Entry ();
				entero = "noflt";
				do {
					diaF = new MessageDialog (null, 0, MessageType.Question, ButtonsType.Ok, "Flotante " + cant [1] + ": ");			
					diaF.ActionArea.PackStart (entry);
					diaF.ShowAll ();
					diaF.Run ();
					entero = entry.Text;
					Variables [cant [1]] = new OCTO4.TipoDato (cant [1], entero, "float");
					diaF.Destroy (); 
				} while (!double.TryParse(entero,out aux_flt));
				break;
			case "SLT":
				// Aqui debe haber dos etiquetas, si no, es error.
				int salto = SaltaA ("ETQ " + cant [1]);
				if (salto != -2) {
					linea = salto;
				}
				break;
			case "SLTSF":
				int destinoFalse = SaltaA ("ETQ " + cant [1]);
				if (DameValor () == "0") {
					if (destinoFalse != -2) {
						linea = destinoFalse;
					}
				}
				break;
			case "SLTSV":
				int destinoTrue = SaltaA ("ETQ " + cant [1]);
				if (DameValor () == "1") {
					if (destinoTrue != -2) {
						linea = destinoTrue;
					}
				}
				break;
			case "II":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 == n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "NI":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 != n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "MAI":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 >= n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "MEI":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 <= n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "ME":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 < n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;
			case "MA":
				if (pila.Count > 1) {
					int n1 = Int32.Parse (DameValor ());
					int n2 = Int32.Parse (DameValor ());
					if (n2 > n1) {
						pila.Push (new OCTO4.TipoDato ("1", "1", "int"));
					} else {
						pila.Push (new OCTO4.TipoDato ("0", "0", "int"));
					}
				}
				break;	
			// Fn de switch
			}
			linea++;
			instruccion = NextLine (linea);	
		}

	}
	public string NextLine(int linea){
		TextIter iter1, iter2;

		iter1 = Intermedio.Buffer.GetIterAtLine (linea);
		iter2 = iter1;
		iter2.ForwardToLineEnd ();
		return iter1.GetText (iter2);
	}
	public bool existeInstruccion(string instruccion){
		string[] inst = {"VARE","VARF","CGVE","CGVF","WR","ST","CGE","CGF","SM","R","M","D",
			"P","II","NI","MAI","MEI", "ME","MA","AND","OR","NOT","SLT","SLTSF","SLTSV","ETQ","RIDVF","RIDVE"};
		string[] cant = instruccion.Split (' ');
		if(cant[0] != null){
			for (int i = 0; i < inst.Length; i++) {
				if(cant[0].Equals(inst[i])){
					return true;
				}
			}
		}
		return false;
	}
	public int SaltaA(string etiqueta){
		int num = 0;
		//Console.WriteLine ("esta cosa debe saltar a: "+etiqueta);
		do {
			//Console.WriteLine ("comparando: "+NextLine(num) +" con "+etiqueta);
			if(NextLine(num) == etiqueta){
				return num-1;
			}else if(NextLine(num) == "EOFF"){
				return -2;
			}
			num++;
		} while (NextLine (num) != "EOFF" && existeInstruccion (NextLine (num)));
		return -2;
	}
	public string DameTipoDato(){
		if (pila.Peek () != null) {
			if (Variables.Contains (pila.Peek ())) {
				OCTO4.TipoDato dato = (OCTO4.TipoDato)Variables [pila.Pop ()];
				return dato.GetValor ();
			} else {
				return pila.Pop ().GetValor ();
			}
		}
		return "";
	}
	public string DameValor(){
		if (pila.Peek () != null) {
			if (Variables.Contains (pila.Peek ())) {
				OCTO4.TipoDato dato = (OCTO4.TipoDato)Variables [pila.Pop ()];
				return dato.GetValor ();
			} else {
				return pila.Pop ().GetValor ();
			}
		}
		return "";
	}

	protected void Compilar (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}
}