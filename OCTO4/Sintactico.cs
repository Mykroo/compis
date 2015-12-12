using System;
using System.Collections.Generic;
using Gtk;
namespace OCTO4
{
	public class Sintactico
	{
		Queue<Token> tokens;
		Token actualTkn, anterior;
		bool terminar;
		ListStore store;
		string archivoTokens;
		string archivoArbol;
		public Sintactico(Queue<Token> tokens, ListStore store)
		{
			this.tokens = tokens;
			this.store = store;
			this.terminar = false;
			archivoTokens = "";
			archivoArbol = "";
		}
		public ListStore GetListStore()
		{
			return this.store;
		}
		// Si hay un error de token nulo viene de aqui
		public bool compara(string tkn)
		{
			if (actualTkn.getLexema().Equals(tkn))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public string getArchivoTokes()
		{
			return this.archivoTokens;
		}
		public string ArchivoArbol()
		{
			return this.archivoArbol;
		}
		public bool siguiente()
		{
			if (tokens.Count > 0)
			{
				anterior = actualTkn;
				actualTkn = tokens.Dequeue();
				archivoTokens += "Id "+actualTkn.getTipoToken ()+
					" ==> "+actualTkn.getLexema()+"\n";
				return true;
			}
			else
			{
				if (anterior != null)
				{
					actualTkn = new Token("fin", (int)enumTok.final, anterior.getLinea());
				}
				else 
				{
					actualTkn = new Token("fin", (int)enumTok.final, -1);
				}

				terminar = true;
				return false;
			}
		}
		public Nodo creaArbol()
		{
			Nodo nodo = null;
			if (siguiente())
			{

				nodo = program();
			}
			else
			{
			}
			return nodo;
		}
		public string GetArbol()
		{
			return "";
		}
		public Nodo program()
		{
			Nodo nodo = null;
			if (tokens.Count < 3) {
				store.AppendValues ("Khe?", "Programa mal estructurado,"+
					" tokens insuficientes");
				return nodo;
			}
			if (compara("program"))
			{
				nodo = new Nodo(actualTkn);
				siguiente();
			}
			else 
			{			
				nodo = new Nodo(new Token("Esperaba 'Program'",-1,actualTkn.getLinea()));
				store.AppendValues (actualTkn.getLinea (), "Falta 'Program' al inicio de programa");
			}
			if (compara("{"))
			{
				siguiente();
			}
			else {
				store.AppendValues (actualTkn.getLinea (), "falta '{' después de program");
			}
				nodo.hijos[0] = lista_Declaracion();
				nodo.hijos[1] = lista_sentencias();
			if (compara("}"))
			{
				siguiente();
				if (tokens.Count > 0)
				{						
					store.AppendValues (actualTkn.getLinea (), "Tokens después del final del programa ");
				}
			}
			else {
				store.AppendValues (actualTkn.getLinea (), "Esperaba '}' al final del programa");

			}
			GeneraArchivoArbol(nodo);
			return nodo;
		}

		//bool first = true;
		public void espacios()
		{
			for (int i = 0; i < identar-1; i++)
			{                
				archivoArbol += "   ";
			}            
			archivoArbol += "└─";
		}
		int identar;
		int cont = 0;
		public void GeneraArchivoArbol(Nodo arbol) 
		{
			identar += 2; ;
			cont++;
			while(arbol != null){
				espacios();
				if (arbol.token.getTipoToken() == (int)enumTok.igual)
				{
					archivoArbol += "asignacion: ";
				}
				else if (arbol.token.getTipoToken() == (int)enumTok.Read) 
				{
					archivoArbol += "read: ";
				}
				archivoArbol += arbol.token.getLexema()+"\n";

				for (int i = 0; i <= 4; i++)
				{

					GeneraArchivoArbol(arbol.hijos[i]);   

				}
				arbol = arbol.hermano;                   
			}
			identar -= 2;
			cont--;
		}
		public Nodo lista_Declaracion()
		{
			Nodo nodo = null;
			if (terminar)
			{
				return nodo;
			}
			nodo = declaracion();
			if (actualTkn.getTipoToken().Equals((int)enumTok.Int) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Float) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Bool))
			{
				nodo.hermano = lista_Declaracion();
			}
			else
			{

			}
			return nodo;
		}
		public Nodo declaracion()
		{

			Nodo nodo = null;
			nodo = tipo();
			try
			{
				nodo.hijos[0] = lista_id();
			}catch(Exception ceroDeclar)
			{                
			}
			if (nodo != null && actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
			{
				siguiente();
			}
			else
			{
				if (nodo != null)
				{
					store.AppendValues (actualTkn.getLinea (), "Esperaba ';' en la declaracion");
				}
			}       
			return nodo;
		}
		public Nodo tipo()
		{
			Nodo nodo = null;
			switch (actualTkn.getTipoToken())
			{
			case (int)enumTok.Int:
				nodo = new Nodo(actualTkn);
				siguiente();
				break;
			case (int)enumTok.Float:
				nodo = new Nodo(actualTkn);
				siguiente();
				break;
			case (int)enumTok.Bool:
				nodo = new Nodo(actualTkn);
				siguiente();
				break;
			}
			return nodo;
		}
		public Nodo lista_id()
		{
			Nodo nodo = null;
			if (terminar)
			{
				return nodo;
			}
			try
			{
				if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
				{
					nodo = new Nodo(actualTkn);
					siguiente();				
				}
				if (actualTkn.getLexema().Equals(","))
				{
					if (tokens.Peek().getLexema().Equals(","))
					{				
						store.AppendValues (actualTkn.getLinea (), "Tienes dos a mas ',' en una declaracion");
					}
					else if (!tokens.Peek().getTipoToken().Equals((int)enumTok.Id))
					{					
						store.AppendValues (actualTkn.getLinea (), "Esperaba un id despues de ',' en declaracion");
					}
					else
					{
						siguiente();
						nodo.hermano = lista_id();
					}
				}
			}
			catch (Exception e)
			{
				//se terminaro los tokens, error al hacer peek
			}
			return nodo;
		}
		public Nodo lista_sentencias()
		{
			Nodo nodo = null;
			nodo = sentencia();
			if (actualTkn.getTipoToken().Equals((int)enumTok.If) ||
				actualTkn.getTipoToken().Equals((int)enumTok.While) ||
				actualTkn.getTipoToken().Equals((int)enumTok.For) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Do) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Read) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Write) ||
				actualTkn.getTipoToken().Equals((int)enumTok.llaveAbre) ||
				actualTkn.getTipoToken().Equals((int)enumTok.Id))
			{
				try
				{
					//Nodo nodoq = null;
					nodo.hermano = lista_sentencias();
				}
				catch (Exception e2)
				{

				}
			}                        

			return nodo;
		}
		public Nodo sentencia()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.If))
			{
				nodo = seleccion();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.While))
			{
				nodo = iteracion();

			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.For))
			{
				nodo = iteracion_for();
			}            
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Do))
			{
				nodo = repeticion();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Read))
			{
				nodo = sent_read();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Write))
			{
				nodo = sent_write();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.llaveAbre))
			{
				nodo = bloque();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
			{
				nodo = asignacion();
				if (actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
				{
					siguiente();
				}
				else
				{                
					// Cuando ce cicla, infinito
				}
			}
			return nodo;
		}

		public Nodo seleccion()
		{
			Nodo nodo = null;
			nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.If, actualTkn.getLinea()));
			siguiente();
			if (actualTkn.getTipoToken().Equals((int)enumTok.ParentesisAbre))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba '(' en sentencia if");
			}
			nodo.hijos[0] = b_expresión();
			if (actualTkn.getTipoToken().Equals((int)enumTok.parentesisCierra))
			{
				siguiente();
			}
			else
			{			
				store.AppendValues (actualTkn.getLinea (), "Esperaba ')' en sentencia if");
			}
			nodo.hijos[1] = bloque ();            
			if (actualTkn.getTipoToken().Equals((int)enumTok.Else))
			{
				siguiente();
				nodo.hijos[2] = bloque();
			}
			return nodo;
		}
		public Nodo iteracion()
		{
			Nodo nodo = null;
			nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.While, actualTkn.getLinea()));
			siguiente();
			if (actualTkn.getTipoToken().Equals((int)enumTok.ParentesisAbre))
			{
				siguiente();
			}
			else
			{		
				store.AppendValues (actualTkn.getLinea (), "Esperaba '(' en sentencia while");
			}

			nodo.hijos[0] = b_expresión();
			if (actualTkn.getTipoToken().Equals((int)enumTok.parentesisCierra))
			{
				siguiente();
			}
			else
			{		
				store.AppendValues (actualTkn.getLinea (), "Esperaba ')' en sentencia while");
			}
			nodo.hijos[1] = bloque();

			return nodo;
		}
		public Nodo iteracion_for()
		{
			Nodo nodo = null;
			nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.For, actualTkn.getLinea()));
			siguiente();
			if (actualTkn.getTipoToken().Equals((int)enumTok.ParentesisAbre))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba '(' en sentencia for");
			}

			//nodo.hijos[0] = declaracion();
			nodo.hijos[1] = b_expresión();

			if (actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
			{
				siguiente();
			}
			else 
			{			
				store.AppendValues (actualTkn.getLinea (), "Esperaba ';' en sentencia for");
			}

			nodo.hijos[2] = lista_sentencias();

			if (actualTkn.getTipoToken().Equals((int)enumTok.parentesisCierra))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba ')' en sentencia for");
			}
			nodo.hijos[3] = bloque();
			return nodo;
		}
		public Nodo repeticion()
		{
			Nodo nodo = null;
			nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.Do, actualTkn.getLinea()));
			siguiente();

			nodo.hijos[0] = bloque();

			if (actualTkn.getTipoToken().Equals((int)enumTok.Until))
			{
				siguiente();
			}
			else
			{			
				store.AppendValues (actualTkn.getLinea (), "Esperaba 'until'");
			}
			if (actualTkn.getTipoToken().Equals((int)enumTok.ParentesisAbre))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba '(' despues de until en sentencia do ");
			}


			nodo.hijos[1] = b_expresión();


			if (actualTkn.getTipoToken().Equals((int)enumTok.parentesisCierra))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba ')' despues de until en sentencia do ");
			}

			if (actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba ';' despues de until en sentencia do ");
			}
			return nodo;
		}
		public Nodo sent_read()
		{
			Nodo nodo = null;
			siguiente();


			if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.Read, actualTkn.getLinea()));
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba id despues de read");
			}
			if (actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba ';' al finalizar read");
			}            
			return nodo;
		}
		public Nodo sent_write()
		{
			Nodo nodo = null;
			siguiente();


			nodo = new Nodo(new Token("write", (int)enumTok.Write, actualTkn.getLinea()));
			nodo.hijos[0] = b_expresión();
			if (actualTkn.getTipoToken().Equals((int)enumTok.puntoComa))
			{
				siguiente();
			}
			else
			{
				store.AppendValues (actualTkn.getLinea (), "Esperaba ';' al finalizar write ");
			}
			return nodo;
		}
		public Nodo bloque()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.llaveAbre)) 
			{
				siguiente();
				nodo = lista_sentencias();

				if (actualTkn.getTipoToken().Equals((int)enumTok.llaveCierra))
				{
					siguiente();
				}
				else 
				{
					store.AppendValues (actualTkn.getLinea (), "Esperaba '}' para finalizar bloque ");
				}
			}

			return nodo;
		}
		public Nodo asignacion()
		{
			Nodo nodo = null;

			if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
			{
				try
				{

					if (tokens.Peek().getTipoToken().Equals((int)enumTok.igual))
					{
						nodo = new Nodo(new Token(actualTkn.getLexema(),(int)enumTok.igual,actualTkn.getLinea()));
						siguiente();
						siguiente();
						nodo.hijos[0] = b_expresión();
						if(actualTkn.getLexema() != ";") 
						{						
							store.AppendValues (actualTkn.getLinea (), "Esperaba ';' despues de asignacion *");
						}else{
							siguiente();
						}
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.masIgual))
					{
						nodo = new Nodo(new Token("+= " + actualTkn.getLexema(), (int)enumTok.igual, actualTkn.getLinea()));

						siguiente();
						siguiente();
						nodo.hijos[0] = b_expresión();
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.menosIgual))
					{
						nodo = new Nodo(new Token("-= " + actualTkn.getLexema(), (int)enumTok.igual, actualTkn.getLinea()));
						siguiente();
						siguiente();					
						nodo.hijos[0] = b_expresión();
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.entreIgual))
					{
						nodo = new Nodo(new Token("/= " + actualTkn.getLexema(), (int)enumTok.igual, actualTkn.getLinea()));
						siguiente();
						siguiente();					
						nodo.hijos[0] = b_expresión();
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.porIgual))
					{
						nodo = new Nodo(new Token("*= " + actualTkn.getLexema(), (int)enumTok.igual, actualTkn.getLinea()));
						siguiente();
						siguiente();
						nodo.hijos[0] = b_expresión();
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.masMas))
					{
						nodo = new Nodo(new Token(actualTkn.getLexema(), (int)enumTok.masMas,actualTkn.getLinea()));
						siguiente();
					}
					else if (tokens.Peek().getTipoToken().Equals((int)enumTok.menosMenos))
					{

					}
					else 
					{                        
						if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
						{								
							actualTkn = new Token("null", (int)enumTok.Null, actualTkn.getLinea());
							store.AppendValues (actualTkn.getLinea (), "Tienes ID's seguidos o posible expresión incompleta ");
						}
					}
				}
				catch (Exception e)
				{
					//al hacer peek ya no hay tokens, comparas null con otros datos
				}

			}
			return nodo;
		}
		// b_expresión() y b_term() estan cicladas en si mismas, pero hay dudas aqui
		public Nodo b_expresión()
		{
			Nodo nodo = null;
			nodo = b_term();
			while(actualTkn.getTipoToken().Equals((int)enumTok.Or))
			{
				Nodo nodCp;
				nodCp = new Nodo(new Token(actualTkn.getLexema(),actualTkn.getTipoToken(),actualTkn.getLinea()));
				nodCp.hijos[0] = nodo;
				nodo = nodCp;
				siguiente();
				nodo.hijos[1] = b_expresión();
			}

			return nodo;
		}
		public Nodo b_term()
		{
			Nodo nodo = null;
			nodo = not_factor();
			if (actualTkn.getTipoToken().Equals((int)enumTok.And))
			{
				Nodo nodCp;
				nodCp = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				nodCp.hijos[0] = nodo;
				nodo = nodCp;
				siguiente();
				nodo.hijos[1] = b_term();
			}
			return nodo;
		}
		public Nodo not_factor()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.Not))
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				nodo.hijos[0] = not_factor();
			}
			else
			{
				nodo = b_factor();
			}
			return nodo;
		}
		public Nodo b_factor()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.True) || 
				actualTkn.getTipoToken().Equals((int)enumTok.False)) 
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
			}else
			{
				nodo = relacion();
			}
			return nodo;
		}
		public Nodo relacion()
		{
			Nodo nodo = null;
			nodo = expresión();
			if (actualTkn.getTipoToken().Equals((int)enumTok.mayor) ||
				actualTkn.getTipoToken().Equals((int)enumTok.menor) ||
				actualTkn.getTipoToken().Equals((int)enumTok.mayorIgual) ||
				actualTkn.getTipoToken().Equals((int)enumTok.menorIgual) ||
				actualTkn.getTipoToken().Equals((int)enumTok.igualIgual) ||
				actualTkn.getTipoToken().Equals((int)enumTok.diferente ))
			{               
				Nodo nodCp;                
				nodCp = relop();
				nodCp.hijos[0] = nodo;             
				nodo = nodCp;
				nodo.hijos[1] = expresión();
			}
			return nodo;
		}
		public Nodo relop()
		{
			Nodo nodo = null;
			switch (actualTkn.getTipoToken()) 
			{
			case (int)enumTok.menor:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.menorIgual:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.mayor:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.mayorIgual:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.igualIgual:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.diferente:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			}
			return nodo;
		}
		public Nodo expresión()
		{
			Nodo nodo = null;
			nodo = termino();
			while (actualTkn.getTipoToken().Equals((int)enumTok.mas) || actualTkn.getTipoToken().Equals((int)enumTok.menos))
			{
				Nodo nodCp;
				nodCp = suma_op();
				nodCp.hijos[0] = nodo;
				nodo = nodCp;
				nodo.hijos[1] = termino();
			}
			return nodo;
		}
		public Nodo suma_op()
		{
			Nodo nodo = null;
			switch (actualTkn.getTipoToken())
			{
			case (int)enumTok.menos:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.mas:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			}
			return nodo;
		}
		public Nodo termino()
		{
			Nodo nodo = null;
			nodo = signo_factor();
			while (actualTkn.getTipoToken().Equals((int)enumTok.por) || actualTkn.getTipoToken().Equals((int)enumTok.entre))
			{
				Nodo nodCp;
				nodCp = mult_op();
				nodCp.hijos[0] = nodo;
				nodo = nodCp;
				nodo.hijos[1] = signo_factor();
			}
			return nodo;
		}
		public Nodo mult_op()
		{
			Nodo nodo = null;
			switch (actualTkn.getTipoToken())
			{
			case (int)enumTok.por:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			case (int)enumTok.entre:
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				break;
			}
			return nodo;
		}
		public Nodo signo_factor()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.menos) || actualTkn.getTipoToken().Equals((int)enumTok.mas))
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				nodo.hijos[0] = factor();
			}
			else 
			{
				nodo = factor();
			}
			return nodo;
		}
		public Nodo factor()
		{
			Nodo nodo = null;
			if (actualTkn.getTipoToken().Equals((int)enumTok.ParentesisAbre))
			{
				siguiente();
				nodo = b_expresión();
				if (actualTkn.getTipoToken().Equals((int)enumTok.parentesisCierra))
				{
					siguiente();
				}
				else
				{
					store.AppendValues (actualTkn.getLinea (), "Esperaba un ')'");
				}
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Numero) ||
				actualTkn.getTipoToken().Equals((int)enumTok.NumeroFraccion))
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
			}
			else if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
			{
				nodo = new Nodo(new Token(actualTkn.getLexema(), actualTkn.getTipoToken(), actualTkn.getLinea()));
				siguiente();
				if (actualTkn.getTipoToken().Equals((int)enumTok.Id))
				{
					store.AppendValues (actualTkn.getLinea (), "Tienes dos ID's seguidos o posible expresión incompleta ");
					actualTkn = new Token("null",(int)enumTok.Null,actualTkn.getLinea());
				}
			}
			return nodo;
		}
	}
}

