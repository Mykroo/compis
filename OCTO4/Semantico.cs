using System;
using System.Collections;
using Gtk;
namespace OCTO4
{
	public class Semantico
	{
		Nodo nodo;
		int tipo;
		Hashtable hashtable = new Hashtable();
		ListStore store;
		// Si hay una variable sin declarar o algun problema, dejamos de analizar prosesos
		bool terminar = false; 
		string archivoArbolSeman;
		public Semantico(ListStore store)
		{
			this.store = store;
			archivoArbolSeman = "";
		}
		public Hashtable getHashTable(){
			return hashtable;
		}
		public ListStore GetListStore()
		{
			return this.store;
		}
		public Nodo getNodos(){
			return nodo;
		}
		public string GetHachValues()
		{
			string hashText = "";
			if (hashtable.Count <= 0) {
				return "";
			}
			IDictionaryEnumerator denum = hashtable.GetEnumerator();
			DictionaryEntry dentry;
			hashText += "\n     Var    Type";
			while (denum.MoveNext())
			{
				dentry = (DictionaryEntry)denum.Current;
				hashText += "\n     " + dentry.Key + "           " + dentry.Value;
			}
			return hashText;
		}
		public string getArchivoArbolSeman(Nodo nod)
		{
			mostrar (nod);
			return archivoArbolSeman;
		}
		public void AnalizaDeclaraciones(Nodo arbol)
		{
			while (arbol != null)
			{                
				if (arbol.token.getTipoToken() == (int)enumTok.Int)
				{
					tipo = (int)enumTok.Int;
				}
				else if (arbol.token.getTipoToken() == (int)enumTok.Float)
				{
					tipo = (int)enumTok.Float;
				}
				else if (arbol.token.getTipoToken() == (int)enumTok.Bool)
				{
					tipo = (int)enumTok.Bool;
				}
				else if (arbol.token.getTipoToken() == (int)enumTok.Id
					|| arbol.token.getTipoToken() == (int)enumTok.igual)
				{
					//Console.WriteLine("Tenemos decaracion " + arbol.token.getLexema());
					try
					{

						if (tipo == (int)enumTok.Int)
						{
							hashtable.Add(arbol.token.getLexema(), "int");
						}
						else if (tipo == (int)enumTok.Float)
						{
							hashtable.Add(arbol.token.getLexema(), "float");
						}
						else if (tipo == (int)enumTok.Bool)
						{
							hashtable.Add(arbol.token.getLexema(), "bool");
						}
					}
					catch (Exception repetido)
					{
						Console.WriteLine("ERROR: Variable (" +
							arbol.token.getLexema() + ") ya declarada");

						store.AppendValues (arbol.token.getLinea(), "La variable (" +
							arbol.token.getLexema() + ") ya esta declarada");
					}
				}
				else
				{
					//Console.WriteLine("ERROR: El token "+ arbol.token.getLexema()+" no existe");
				}
				for (int i = 0; i <= 4; i++)
				{
					AnalizaDeclaraciones(arbol.hijos[i]);

				}
				arbol = arbol.hermano;
			}
		}
		public void analisaSintaxis(Nodo arbol)
		{			
			analisaSintaxis2 (arbol);
		}
		public void analisaSintaxis2(Nodo arbol)
		{
			int tipo = -1;
			while (arbol != null)
			{
				terminar = false;

				if (arbol.token.getTipoToken() == (int)enumTok.Id
					|| arbol.token.getTipoToken() == (int)enumTok.Read
					|| arbol.token.getTipoToken() == (int)enumTok.igual)
				{
					if (!hashtable.Contains(arbol.token.getLexema()))
					{
						Console.WriteLine("ERROR: la variable (" + arbol.token.getLexema() + ")" +
							" no ha sido declarado, linea "+arbol.token.getLinea());

						store.AppendValues (arbol.token.getLinea(), "Variable (" +
							arbol.token.getLexema() + ") no declarada");
					}
				}
				switch(arbol.token.getTipoToken()){
				case (int)enumTok.If:
					tipo = postOrden(arbol.hijos[0]);
					if (tipo != (int)enumTok.Bool)
					{
						store.AppendValues (arbol.token.getLinea(), "if, hace falta expresión booleana");
					}
					break;
				case (int)enumTok.While:
					tipo = postOrden(arbol.hijos[0]);                      
					if (tipo != (int)enumTok.Bool || tipo == -1)
					{
						store.AppendValues (arbol.token.getLinea(), "while, hace falta expresión booleana");
					}
					break;
				case (int)enumTok.Read:
					if(getTipoVariable(arbol.token.getLexema()) == (int)enumTok.Bool) {
						store.AppendValues (arbol.token.getLinea (), "read booleano desactivado");
					}
					Console.WriteLine (arbol.token.getLexema());
					break;
				case (int)enumTok.Do:   
					tipo = postOrden(arbol.hijos[1]);
					if (tipo != (int)enumTok.Bool || tipo == -1)
					{
						Console.WriteLine("ERROR: dohace falta  expresión booleana");
						store.AppendValues (arbol.token.getLinea(), "do hace falta expresión booleana");
					}
					break;
				case (int)enumTok.Write:
					if(arbol.hijos[0] == null){
						store.AppendValues (arbol.token.getLinea(), "write, faltan parámetros ");
					}
					break;
				case (int)enumTok.For:
					tipo = postOrden(arbol.hijos[1]);
					if (tipo != (int)enumTok.Bool || tipo == -1)
					{
						Console.WriteLine("ERROR: For errror");
						store.AppendValues (arbol.token.getLinea(), "El for error");
					}
					break;	
				case (int)enumTok.igual:
					int queTipo = getTipoVariable (arbol.token.getLexema ());
					tipo = postOrden (arbol.hijos [0]);				

					if(queTipo == (int)enumTok.Int
						&& tipo == (int)enumTok.Float)
					{
						Console.WriteLine("ERROR: Int a flotante no valido (" 
							+ arbol.token.getLexema() + ")");

						store.AppendValues (arbol.token.getLinea(), "Estas asignando un flotante a un entero en (" 
							+ arbol.token.getLexema() + ")");
					}
					else if (queTipo == (int)enumTok.Int
						&& tipo == (int)enumTok.Bool
						|| 
						queTipo == (int)enumTok.Float
						&& tipo == (int)enumTok.Bool)
					{
						Console.WriteLine("ERROR: booleano a entero no valido ("
							+ arbol.token.getLexema() + ")");

						store.AppendValues (arbol.token.getLinea(),"booleano a entero no valido en ("
							+ arbol.token.getLexema() + ")");
					}
					else if (queTipo == (int)enumTok.Bool
						&& tipo == (int)enumTok.Int
						||
						queTipo == (int)enumTok.Bool
						&& tipo == (int)enumTok.Float)
					{
						Console.WriteLine("ERROR: estas asignando una expresión "+
							"numerica a un booleano en (" + arbol.token.getLexema() + ")");

						store.AppendValues (arbol.token.getLinea(), "Estas asignando una expresión "+
							"numerica a un booleano en (" + arbol.token.getLexema() + ")");
					}                        
					break;
				}

				for (int i = 0; i <= 4; i++)
				{
					analisaSintaxis(arbol.hijos[i]);

				}
				arbol = arbol.hermano;
			}
		}

		public int postOrden(Nodo exp) 
		{
			int izq=-1, der=-1;            
			if(terminar){
				return -1;
			}
			try
			{                
				// cuando encontramos operadores numericos, regresa entero o flotante
				if (exp.token.getTipoToken() == (int)enumTok.mas
					|| exp.token.getTipoToken() == (int)enumTok.menos
					|| exp.token.getTipoToken() == (int)enumTok.por
					|| exp.token.getTipoToken() == (int)enumTok.entre)
				{
					int queTipo = -1;
					izq = postOrden(exp.hijos[0]);
					der = postOrden(exp.hijos[1]);
					if (izq == der)
					{
						if (izq == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Int;
						}
						else if (izq == (int)enumTok.Float)
						{
							queTipo = (int)enumTok.Float;
						}
						return queTipo;
					}
					else
					{
						if (izq == (int)enumTok.Int && der == (int)enumTok.Float
							|| izq == (int)enumTok.Float && der == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Float;
						}
						else
						{
							if (terminar)
							{
								return -1;
							}
							queTipo = -1;
							Console.WriteLine("ERROR SEMANTICO: esperaba expresión numerica cerca de ("
								+ exp.token.getLexema() + ") linea " + exp.token.getLinea());

							store.AppendValues (exp.token.getLinea(),"Esperaba una " +
								"expresión numerica cerca de ("+ exp.token.getLexema() + ")");
						}
						return queTipo;
					}
				}
				// Cuando encontramos operadores de comparacion
				else if (exp.token.getTipoToken() == (int)enumTok.mayor
					|| exp.token.getTipoToken() == (int)enumTok.menor
					|| exp.token.getTipoToken() == (int)enumTok.mayorIgual
					|| exp.token.getTipoToken() == (int)enumTok.menorIgual)
				{
					int queTipo = -1;
					izq = postOrden(exp.hijos[0]);
					der = postOrden(exp.hijos[1]);
					if (izq == der)
					{
						if (izq == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Bool;
						}
						else if (izq == (int)enumTok.Float)
						{
							queTipo = (int)enumTok.Bool;
						}
						else
						{
							Console.WriteLine("ERROR: esperaba una compracion entre"+
								" expresiones numericas cerca de "
								+ exp.token.getLexema() + "' linea " + exp.token.getLinea());

							store.AppendValues (exp.token.getLinea(),"Esperaba  una " +
								"comparacion entre expresiones numericas cerca de " +
								"("+ exp.token.getLexema() + ")");
						}
						return queTipo;
					}
					else
					{
						if (izq == (int)enumTok.Int && der == (int)enumTok.Float
							|| izq == (int)enumTok.Float && der == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Bool;
						}
						else
						{
							if (terminar)
							{
								return -1;
							}
							queTipo = -1;
							Console.WriteLine("ERROR SEMANTICO: Se esperaba exp booleana cerca de ("
								+exp.token.getLexema() + ") linea "+exp.token.getLinea() );

							store.AppendValues (exp.token.getLinea(),"Esperaba una expresión booleana " +
								"cerca de ("+ exp.token.getLexema() + ")");
						}
						return queTipo;
					}
				}
				// Estos operadores pueden contener todo tipo de datos booleano,int float
				//  y regresa un booleano
				else if (exp.token.getTipoToken() == (int)enumTok.igualIgual
					|| exp.token.getTipoToken() == (int)enumTok.diferente)
				{
					int queTipo = -1;
					izq = postOrden(exp.hijos[0]);
					der = postOrden(exp.hijos[1]);

					if (izq == der)
					{
						if (izq == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Bool;
						}
						else if (izq == (int)enumTok.Float)
						{
							queTipo = (int)enumTok.Bool;
						}
						else if (izq == (int)enumTok.Bool)
						{                            
							queTipo = (int)enumTok.Bool;
						}
						else
						{
							Console.WriteLine("ERROR SEMANTICO: "+exp.token.getLexema()+"cerca de ("
								+ exp.token.getLexema() + ") linea " + exp.token.getLinea());

							store.AppendValues (exp.token.getLinea(),"Esperaba una " +
								"expresión numerarica en la comparacion, cerca de ("+ exp.token.getLexema() + ")");
						}
						return queTipo;
					}
					else
					{
						if (izq == (int)enumTok.Int && der == (int)enumTok.Float
							|| izq == (int)enumTok.Float && der == (int)enumTok.Int)
						{
							queTipo = (int)enumTok.Bool;
						}
						else
						{
							if (terminar)
							{
								return -1;
							}
							queTipo = -1;
							Console.WriteLine("ERROR SEMANTICO: la suma contiene una expresión erronea"+
								"cerca de ("+exp.token.getLexema() + ")"+" linea "+exp.token.getLinea());

							store.AppendValues (exp.token.getLinea(),"La comparacion contiene una expresión erronea "+
								"cerca de ("+exp.token.getLexema() + ")");
						}
						return queTipo;
					}
				}
				// Estos operadores esperan booleanos y regresan un booleano
				else if (exp.token.getTipoToken() == (int)enumTok.Or
					|| exp.token.getTipoToken() == (int)enumTok.And)
				{
					int queTipo = -1;
					izq = postOrden(exp.hijos[0]);
					der = postOrden(exp.hijos[1]);
					if (izq == der)
					{
						if (izq == (int)enumTok.Bool)
						{
							queTipo = (int)enumTok.Bool;
						}
						else
						{
							Console.WriteLine("ERROR SEMANTICO: Esperaba una expresión booleana "+
								"en cerca de ("+ exp.token.getLexema()+") linea "+exp.token.getLinea());

							store.AppendValues (exp.token.getLinea(),"Esperaba una expresión booleana "+
								"cerca de ("+exp.token.getLexema() + ")");
							queTipo = -1;
						}
					}
					else
					{
						if (terminar)
						{
							return -1;
						}
						Console.WriteLine("ERROR SEMANTICO: en expresión cerca de ("+exp.token.getLexema()+")"+
							"linea "+exp.token.getLinea());

						store.AppendValues (exp.token.getLinea(),"expresión booleana planteada mal"+
							"cerca de ("+exp.token.getLexema() + ")");
						queTipo = -1;
					}
					return (int)enumTok.Bool;
				}
				// Una palabra declarada, regresa int float o bool
				else if (exp.token.getTipoToken() == (int)enumTok.Id)
				{
					if (!hashtable.Contains(exp.token.getLexema()))
					{                        
						terminar = true;
						return -1;
					}
					string tipo = "";
					int queTipo = -1;
					if (hashtable.Contains(exp.token.getLexema()))
					{
						tipo = (string)hashtable[exp.token.getLexema()];
						switch (tipo)
						{
						case "int":
							queTipo = (int)enumTok.Int;
							break;
						case "float":
							queTipo = (int)enumTok.Float;
							break;
						case "bool":
							queTipo = (int)enumTok.Bool;
							break;
						}
						return queTipo;
					}
					else
					{
						return queTipo;
					}
				}
				// Negacion, regresa booleano
				else if (exp.token.getTipoToken() == (int)enumTok.Not)
				{
					int queTipo =-1;                 
					izq = postOrden(exp.hijos[0]);
					if (izq == (int)enumTok.Bool)
					{
						queTipo = (int)enumTok.Bool;
					}
					else
					{
						queTipo = -1;
						Console.WriteLine("ERROR SEMANTICO: depues de not debe haber una expresión booleana"+
							"linea "+exp.token.getLinea());

						store.AppendValues (exp.token.getLinea(),"despues de not debe haber una expresión booleana");
					}                    
					return queTipo;
				}
				// Regresa int
				else if (exp.token.getTipoToken() == (int)enumTok.Numero)
				{
					return (int)enumTok.Int;
				}
				// Regresa flotante
				else if (exp.token.getTipoToken() == (int)enumTok.NumeroFraccion)
				{
					return (int)enumTok.Float;
				}
				// Regresa booleano
				else if (exp.token.getTipoToken() == (int)enumTok.True
					|| exp.token.getTipoToken() == (int)enumTok.False)
				{
					return (int)enumTok.Bool;
				}
				else
				{                    
					return -1;
				}
			}
			catch (Exception exceptica) 
			{
				return -1;
			}
		}
		public int getTipoVariable(string lexema) 
		{
			int queTipo = -1;
			if (hashtable.Contains(lexema))
			{
				if ("int" == (string)hashtable[lexema])
				{
					queTipo = (int)enumTok.Int;
				}
				else if ("float" == (string)hashtable[lexema])
				{
					queTipo = (int)enumTok.Float;
				}
				else if ("bool" == (string)hashtable[lexema])
				{
					queTipo = (int)enumTok.Bool;
				}                
			}
			return queTipo;
		}
		public void espacios()
		{
			for (int i = 0; i < identar-1; i++)
			{                
				archivoArbolSeman += "   ";
			}            
			archivoArbolSeman +="└─";
		}
		int identar;
		int cont = 0;
		public void mostrar(Nodo arbol) 
		{
			identar += 2; ;
			cont++;
			while(arbol != null){
				espacios();
				if (arbol.token.getTipoToken() == (int)enumTok.igual)
				{
					archivoArbolSeman += "asign: ";
				}
				else if (arbol.token.getTipoToken() == (int)enumTok.Read) 
				{
					archivoArbolSeman += "read: ";
				}
				if(arbol.token.getTipoToken() == (int)enumTok.igual){

				}					
				archivoArbolSeman += TipoDato (arbol.token.getLexema()) + arbol.token.getLexema()+"\n";

				for (int i = 0; i <= 4; i++)
				{

					mostrar(arbol.hijos[i]);   

				}
				arbol = arbol.hermano;                   
			}
			identar -= 2;
			cont--;
		}
		public string TipoDato(string texto){
			if(hashtable.Contains(texto)){
				return " ("+(String)hashtable [texto]+") ";
			}
			return "";
		}
	}
}