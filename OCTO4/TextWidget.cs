using System;
using Gtk;
namespace OCTO4
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class TextWidget : Gtk.Bin
	{
		string ruta,nombre;
		bool modificado;
		int contLetras, linea;
		TextIter inicioFind,findFind;
		public TextWidget ()
		{
			this.Build ();
			etiquetas ("purple","green","black","yellow","red",false);
			modificado = false;
			contLetras = 0;
			linea = 0;
		}
		public void setRuta(string ruta){
			this.ruta = ruta;
		}
		public string getRuta()
		{
			return ruta;
		}
		public void setNombre(string nombre){
			this.nombre = nombre;
		}
		public string getNombre()
		{
			return nombre;
		}
		public string getText()
		{
			return codigo.Buffer.Text;
		}
		public void setText(string texto)
		{
			codigo.Buffer.Text = texto;
			Console.WriteLine ("set text");
		}
		public void pintarTodo()
		{
			pintar (codigo.Buffer.StartIter,codigo.Buffer.EndIter);
		}
		public bool isModificado()
		{
			return this.modificado;
		}
		public void setModificado(bool modificado)
		{
			this.modificado = modificado;
		}

		public void etiquetas(string reservada,string numero,string normal,
			string comentario, string buscar,bool modificar){
			TextBuffer buffer;
			string texto;

			if(modificar){
				texto = codigo.Buffer.Text;
				codigo.Buffer = new TextBuffer (new TextTagTable());
				codigo.Buffer.Text = texto;
			}
			buffer = codigo.Buffer;
			TextTag tag = new TextTag ("reservada");
			tag.Foreground = reservada;
			buffer.TagTable.Add (tag);

			tag = new TextTag ("numero");
			tag.Foreground = numero;
			buffer.TagTable.Add (tag);

			tag = new TextTag ("normal");
			tag.Foreground = normal;
			buffer.TagTable.Add (tag);

			tag = new TextTag ("comentario");
			tag.Foreground = comentario;
			buffer.TagTable.Add (tag);

			tag = new TextTag ("buscar");
			tag.Foreground =  buscar;
			buffer.TagTable.Add (tag);
		}
		// Manda pintar el texto en la linea donde esta el cursor
		protected void OnTextview2KeyReleaseEvent (object o, Gtk.KeyReleaseEventArgs args)
		{
			TextIter iter1, iter2;

			iter2 = codigo.Buffer.GetIterAtOffset (codigo.Buffer.CursorPosition);
			int linea = iter2.Line;	

			iter1 = codigo.Buffer.GetIterAtLine (linea);
			iter2 = iter1;
			iter2.ForwardToLineEnd ();
			setModificado (true);
			pintar (iter1, iter2);
		}
		public void pintar(TextIter iter1,TextIter iter2){
			int estado = 0,queEs=0;
			String cadena="";
			TextIter line;
			TextBuffer buffer = codigo.Buffer;

			//obteniendo linea actual
			line = codigo.Buffer.GetIterAtOffset (codigo.Buffer.CursorPosition);
			int linea = line.Line;
			int general = codigo.Buffer.CursorPosition;

			//extraigo eltexto entre dos iteradores
			char[] vector = iter1.GetText (iter2).ToCharArray();

			iter1.Buffer.DeleteInteractive (ref iter1, ref iter2, true);
			buffer.RemoveAllTags (iter1, iter2);
			iter1.StartsLine();
			iter2.EndsLine ();
			for(int i=0;i<vector.Length;i++){		
				if (Char.IsDigit (vector [i]) || Char.IsLetter (vector [i])) {
					estado = 1;
					cadena += vector [i];
					if(i+1 == vector.Length){
						estado = 2;
					}
				}else{
					//se recivio caracter diferente de numero o letra
					estado = 2;
				}
				// La cadena se pintara dependiendo del caso.
				if(estado==2){
					if (cadena != "") {
						queEs = esReservada (cadena);
						if(queEs == 1 ){
							buffer.InsertWithTagsByName (ref iter1,cadena,"black");
						}if(queEs==2 ){
							buffer.InsertWithTagsByName (ref iter1,cadena,"reservada");
						}if(queEs==0){
							buffer.InsertWithTagsByName (ref iter1,cadena,"numero");
						}						
						if (i + 1 == vector.Length) {
							if (Char.IsDigit (vector [i]) || Char.IsLetter (vector [i])) {

							} else {
								i--;
							}		
						} else {
							i--;
						}				
						cadena = "";
						estado = 1;
					} else {
						buffer.InsertWithTagsByName (ref iter1, vector [i] + "", "null");
					}
					cadena = "";
					estado = 1;
				}
			}
			int cont = 0;
			iter1 = codigo.Buffer.GetIterAtOffset (general);
			while(!iter1.StartsLine()){
				iter1.BackwardChar ();
				cont++;
			}

			actual.Text =(linea+1)+" y "+cont;
			totales.Text = codigo.Buffer.LineCount+"";

			//posicionando cursor endonde estaba
			codigo.Buffer.PlaceCursor (codigo.Buffer.GetIterAtLineIndex(linea,cont));//esto se queda
		}
		public int esReservada(string cadena){
			char[] letras = cadena.ToCharArray ();
			int numero = 0;
			string[] reser = {"if","read","write", "for","while","until",
				"true","false","and","or","not","program", "int","float","bool", "do", "else"
			};
			for (int i = 0; i < letras.Length; i++) {
				if (Char.IsLetter (letras [i])) {
					numero = 1;
					for (int j = 0; j < reser.Length; j++) {
						if (cadena.Equals (reser [j])) {
							numero = 2;
							break;
						}
					}
				}
			}
			return numero;
		}
		protected void OnOcultarButtonReleaseEvent (object o, ButtonReleaseEventArgs args)
		{

		}
		protected void OnOcultarActivated (object sender, EventArgs e)
		{

		}

		public void Identar(){
			TextIter iter1, iter2;
			iter1 = codigo.Buffer.StartIter;
			iter2 = codigo.Buffer.EndIter;
			bool inicio = true;

			int estado = 0, contador = 0;
			TextBuffer buffer = codigo.Buffer;
			//estraigo texto entre dos iteradores
			char[] vector = iter1.GetText (iter2).ToCharArray();

			iter1.Buffer.DeleteInteractive (ref iter1, ref iter2, true);
			buffer.RemoveAllTags (iter1, iter2);

			for(int i=0;i<vector.Length;i++){
				switch (estado) {
				case 0:
					if(inicio){
						for (int j = 0; j < contador; j++) {								
							buffer.Text += '\t' + "";									
						}
						inicio = false;
					}
					if (vector [i] != ' ' && vector [i] != '\t') {
						i--;
						estado = 1;
					}
					break;
				case 1:
					inicio = true;
					if(vector[i]=='{'){
						contador++;
					}
					if(vector[i]=='}'){
						contador--;
					}						
					if(vector[i]=='\n'){
						estado = 0;
					}
					buffer.Text += vector [i]+"";
					break;
				}
			}

			TextIter iter11, iter22;
			iter11 = codigo.Buffer.StartIter;
			iter22 = codigo.Buffer.EndIter;

			pintar (iter11, iter22);
		}
		protected void OnSiguiClicked (object sender, EventArgs e)
		{
			TextIter fin;
			inicioFind = codigo.Buffer.GetIterAtOffset (contLetras);
			fin = inicioFind;
			fin.ForwardToLineEnd ();
			Console.WriteLine (inicioFind.GetText(fin));
			contLetras++;
		}

		protected void OnReemplClicked (object sender, EventArgs e)
		{
		}

		protected void OnAnteriClicked (object sender, EventArgs e)
		{
		}

		protected void OnTodasClicked (object sender, EventArgs e)
		{
		}
		public void Cortar()
		{
			Clipboard clip = codigo.GetClipboard (Gdk.Selection.Clipboard); 
			codigo.Buffer.CutClipboard (clip, true);
		}
		public void Copiar()
		{
			Clipboard clip = codigo.GetClipboard (Gdk.Selection.Clipboard); 
			codigo.Buffer.CopyClipboard (clip);
		}		
		public void Pegar()
		{
			Clipboard clip = codigo.GetClipboard (Gdk.Selection.Clipboard); 
			codigo.Buffer.PasteClipboard (clip);
		}
		public void Eliminar()
		{
			codigo.Buffer.DeleteSelection (true,true);
		}	
	}
}

