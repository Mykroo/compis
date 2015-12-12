using System;
using System.Collections;
namespace Compi_segundo
{
	public class GeneraCodigo
	{
		Hashtable hashtable;
		string codigoFinal;
		Stack asignacion = new Stack();
		Stack asignacionWhile = new Stack();
		public GeneraCodigo ()
		{
			hashtable = new Hashtable();
			codigoFinal = "";
		}
		public void GenererarDeclaETQ(Hashtable MiHash)
		{
			if (MiHash.Count <= 0) {
				return;
			}
			IDictionaryEnumerator denum = MiHash.GetEnumerator();
			DictionaryEntry dentry;
			Console.WriteLine();
			while (denum.MoveNext())
			{
				dentry = (DictionaryEntry)denum.Current;
				if(dentry.Value.Equals("int") || dentry.Value.Equals("bool")){
					codigoFinal += "VARE "+dentry.Key+"\n";
				}else if(dentry.Value.Equals("float")){
					codigoFinal += "VARF "+dentry.Key+"\n";
				}
			}
		}
		public string GetCodigo(Nodo nod, Hashtable tabla){
			this.hashtable = tabla;
			GenererarDeclaETQ (hashtable);
			etiquetas.Push (1);
			ciclarRecorrido(nod);
			codigoFinal += "EOFF";
			return codigoFinal;
		}
		public Nodo GeneraCodAsig(Nodo nod){
			if (nod == null) {
				return nod;
			} else {
				GeneraCodAsig (nod.hijos[0]);
				GeneraCodAsig (nod.hijos[1]);
				if (TipoOperador (nod.token.getLexema ()) == "-1") {
					codigoFinal+="CG"+Numero(nod.token.getLexema())+"\n";				
				} else {
					codigoFinal += TipoOperador (nod.token.getLexema ()) + "\n";
				}
				Console.WriteLine (nod.token.getLexema ());
			}
			return nod;
		}
		// Genera la etiqueta correspondiente si es numero variable y de que tipo es
		public string Numero(string val){
			char[] action = val.ToCharArray ();
			int cont = 0;
			for(int i=0;i<action.Length;i++){
				if (char.IsDigit (action [i])) {
					cont++;
				} else {
					break;
				}
			}
			// Las primeras dos E y F son para numeros,  el else es para variables o true y false
			if(cont == action.Length){
				return "E "+val;
			}else if(val.Contains(".")){
				return "F "+val;
			}else{
				if (hashtable.Contains (val)) {
					if ("int" == (string)hashtable [val]) {
						return "VE "+val;
					} else if ("float" == (string)hashtable [val]) {
						return "VF " + val;
					} else if ("bool" == (string)hashtable [val]) {
						return "VE "+val;
					} 
				} else {
					if(val.Equals("true")){
						return "E 1";
					}else if(val.Equals("false")){
						return "E 0";
					}
				}
			}				
			return "";
		}
		// Regresa el codigo de etiqueta correspondiente de cada operador
		public string TipoOperador(string texto){
			string[] tipos =       {"+","-","*","/","%","==","!=",">=", 
				"<=", "<", ">", "and","or","not" };
			string[] equivalente = {"SM","R","M","D","P","II","NI","MAI",
				"MEI", "ME","MA","AND","OR","NOT"};
			for(int i=0;i<tipos.Length;i++){
				if(tipos[i].Equals(texto)){		
					return equivalente[i];
				}
			}
			return "-1";
		}
		Stack etiquetas = new Stack();

		/* genera estiquetas correspondientes para cada instruccion
		   Antes de cada etiqueta SLTSF, SLTSV hay una exp booleana 
		   la cual se manda a analizar para generarle etiquetas
		*/
		public void AnalizaArbol(Nodo arbol) 
		{
			if(arbol == null){
				return;
			}
			if(arbol.token.getTipoToken() == (int)enumTok.If){
				int valW = (int)etiquetas.Pop ();
				GeneraCodAsig (arbol.hijos[0]);
				codigoFinal += "SLTSF L"+valW+"\n";
				asignacionWhile.Push (valW+1);

				etiquetas.Push (valW+2);
				ciclarRecorrido (arbol.hijos[1]);
				valW = (int)asignacionWhile.Pop ();
				codigoFinal += "SLT L"+(valW)+"\n";
				codigoFinal += "ETQ L"+(valW-1)+"\n";
				ciclarRecorrido (arbol.hijos[2]);
				codigoFinal += "ETQ L"+(valW)+"\n";

			}else if(arbol.token.getTipoToken() == (int)enumTok.While){
				int valW = (int)etiquetas.Pop ();
				codigoFinal += "ETQ L"+valW+"\n";
				GeneraCodAsig (arbol.hijos[0]);
				codigoFinal += "SLTSF L"+(valW+1)+"\n";
				asignacionWhile.Push (valW+1);
				etiquetas.Push (valW+2);
				ciclarRecorrido (arbol.hijos[1]);
				valW = (int)asignacionWhile.Pop ();
				codigoFinal += "SLT L"+(valW-1)+"\n";
				codigoFinal += "ETQ L"+(valW)+"\n";
			}
			else if(arbol.token.getTipoToken() == (int)enumTok.Do){
				int val1 = (int)etiquetas.Pop ();
				codigoFinal += "ETQ L"+val1+"\n";
				etiquetas.Push (val1+1);
				asignacionWhile.Push (val1);
				ciclarRecorrido (arbol.hijos[0]);
				GeneraCodAsig (arbol.hijos[1]);
				codigoFinal += "SLTSV L"+asignacionWhile.Pop()+"\n";
			}
			else if(arbol.token.getTipoToken() == (int)enumTok.Read){
				codigoFinal += "RID"+Numero(arbol.token.getLexema())+"\n";
			}
			else if(arbol.token.getTipoToken() == (int)enumTok.Write){
				GeneraCodAsig (arbol.hijos[0]);
				codigoFinal += "WR"+"\n";			
			}
			else if(arbol.token.getTipoToken() == (int)enumTok.For){
				if(arbol.hijos[0] != null){
					//GeneraCodAsig (arbol.hijos[0]);
				}
				int valW = (int)etiquetas.Pop ();
				codigoFinal += "ETQ L"+valW+"\n";
				GeneraCodAsig (arbol.hijos[1]);
				codigoFinal += "SLTSF L"+(valW+1)+"\n";
				asignacionWhile.Push (valW+1);
				etiquetas.Push (valW+2);
				ciclarRecorrido (arbol.hijos[3]);
				valW = (int)asignacionWhile.Pop ();
				ciclarRecorrido (arbol.hijos[2]);
				codigoFinal += "SLT L"+(valW-1)+"\n";
				codigoFinal += "ETQ L"+(valW)+"\n";
			}
			else if(arbol.token.getTipoToken() == (int)enumTok.igual){
				GeneraCodAsig (arbol.hijos [0]);			
				codigoFinal += "ST "+arbol.token.getLexema()+"\n";
			}
		}
		// Se cicla para que todo el arbol pueda ser leido
		public void ciclarRecorrido(Nodo nod)
		{
			try
			{
				asignacion.Clear();
				if (nod.token.getTipoToken() == (int)enumTok.If ||
					nod.token.getTipoToken() == (int)enumTok.While ||
					nod.token.getTipoToken() == (int)enumTok.For ||
					nod.token.getTipoToken() == (int)enumTok.Do ||
					nod.token.getTipoToken() == (int)enumTok.Read ||
					nod.token.getTipoToken() == (int)enumTok.Write ||
					nod.token.getTipoToken() == (int)enumTok.llaveAbre ||
					nod.token.getTipoToken() == (int)enumTok.igual)
				{
					AnalizaArbol(nod);										
					if(nod.hermano != null){
						nod = nod.hermano;
						ciclarRecorrido (nod);
					}
				}
			}
			catch (Exception e2)
			{}
		}
	}
}
