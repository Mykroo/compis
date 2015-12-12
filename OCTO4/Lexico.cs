using System;
using System.Collections.Generic;
using Gtk;
namespace Compi_segundo
{
	public class Lexico
	{
		enum State {
			inicial,numeros,fraccion,letras,
			verificaComentario,comentLinea,
			comentBloque,finBloque,tieneIgual
		}
		char[] letra;
		Queue<Token> listaTok,copia;
		int estado,Id;
		string cadena,ArchivoToken;
		int linea;
		ListStore store;
		public Lexico(string texto,ListStore store)
		{
			texto += " ";
			letra = texto.ToCharArray ();
			copia = listaTok = new Queue<Token>();
			estado = 0;
			Id = 0;
			cadena = "";
			ArchivoToken = "";
			linea = 1;
			this.store = store;
		}
		public string GetTokens()
		{
			return ArchivoToken;
		}
		public Queue<Token> GetListaTokens(){
			return this.listaTok;
		}
		public Queue<Token> Tokenize()
		{
			int cont;
			char temporal= ' ';

			for(cont=0;cont<letra.Length;cont++)
			{
				switch(estado){
				case (int)State.inicial:
					temporal = ' ';
					// Elimina espacios, tabulaciones y saltos de linea.
					if (letra[cont].Equals(' ') || letra[cont].Equals('\t')
						|| letra[cont].Equals('\n'))
					{
						if (letra[cont].Equals('\n'))
						{
							this.linea++;
						}
						break;
					}                         
					if (Char.IsLetter(letra[cont]))
					{
						estado = (int)State.letras;
						cont--;
						break;
					}
					else if (Char.IsNumber(letra[cont])) {
						estado = (int)State.numeros;
						cont--;
						break;
					}
					else if (letra[cont].Equals('+')) {
						temporal = '+';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('-'))
					{
						temporal = '-';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('*'))
					{
						temporal = '*';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('/'))
					{
						estado = (int)State.verificaComentario;
						break;
					}
					else if (letra[cont].Equals('>'))
					{
						temporal = '>';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('<'))
					{
						temporal = '<';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('='))
					{
						temporal = '=';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals('!'))
					{
						temporal = '!';
						estado = (int)State.tieneIgual;
					}
					else if (letra[cont].Equals(','))
					{
						listaTok.Enqueue(new Token(",", (int)enumTok.coma, this.linea));
						//Console.WriteLine(",");
					}
					else if (letra[cont].Equals(';'))
					{
						listaTok.Enqueue(new Token(";", (int)enumTok.puntoComa, this.linea));
						//Console.WriteLine(";");
					}
					else if (letra[cont].Equals('('))
					{
						//Console.WriteLine("(");
						listaTok.Enqueue(new Token("(", (int)enumTok.ParentesisAbre, this.linea));
					}
					else if (letra[cont].Equals(')'))
					{
						listaTok.Enqueue(new Token(")", (int)enumTok.parentesisCierra, this.linea));
						//Console.WriteLine(")");
					}
					else if (letra[cont].Equals('{'))
					{
						//Console.WriteLine("{ ");
						listaTok.Enqueue(new Token("{", (int)enumTok.llaveAbre, this.linea));
					}
					else if (letra[cont].Equals('}'))
					{
						listaTok.Enqueue(new Token("}", (int)enumTok.llaveCierra, this.linea));
						//Console.WriteLine("}");
					}
					else 
					{
						listaTok.Enqueue(new Token("error (" + letra[cont] + ")", (int)enumTok.Error, this.linea));
						//Console.WriteLine("ERRROR: caracter desconocido ("+letra[cont]+") Linea "+this.linea);
						store.AppendValues (this.linea, "Caracter inesperado ("+letra[cont]+")");
					}            
					break;
				case (int)State.letras:
					if (Char.IsLetter(letra[cont]) 
						|| Char.IsNumber(letra[cont]))
					{
						//Console.Write(letra[cont]);
						cadena += letra[cont];
					} 
					else 
					{
						int val = isReservada(cadena);

						// Si hay problemas con if, aqui puede estar la respuesta
						if (val.Equals((int)enumTok.Id))
						{
							listaTok.Enqueue(new Token(cadena, (int)enumTok.Id, this.linea));
						}
						else {
							listaTok.Enqueue(new Token(cadena,val, this.linea));
						}					
						cadena ="";
						estado = (int)State.inicial;
						cont--;                             
					}
					break;
				case (int)State.numeros:
					if (Char.IsNumber(letra[cont]))
					{
						cadena += letra[cont];
						break;
					}
					else {
						if (letra[cont].Equals('.'))
						{
							cadena += ".";
							estado = (int)State.fraccion;
						}
						else 
						{                                 
							if (char.IsLetter(letra[cont]))
							{
								listaTok.Enqueue(new Token("Er) " + cadena, (int)enumTok.Numero, this.linea));															
								store.AppendValues (this.linea, "Hay un numero combinado con letras");
							}
							else {
								listaTok.Enqueue(new Token(cadena, (int)enumTok.Numero, this.linea));                                 
							}
							cadena = "";
							estado = (int)State.inicial;
							cont--;                            
						}                             
					}
					break;
					// Numeros con punto
				case (int)State.fraccion:
					if (Char.IsNumber(letra[cont]))
					{
						cadena+=letra[cont];
						break;
					}
					else if (!letra[cont].Equals('\n') && !letra[cont].Equals('\t')
						&& !letra[cont].Equals(' ') && Char.IsLetter(letra[cont]))
					{
						listaTok.Enqueue(new Token(" Er) " + cadena, (int)enumTok.Error, this.linea));
						store.AppendValues (this.linea, "Hay un numero flotante combinado con letras");
					}
					else
					{
						char []cp = cadena.ToCharArray();
						if (cp[cadena.Length-1] == '.')
						{
							listaTok.Enqueue(new Token(" Er) " + cadena, (int)enumTok.Error, this.linea));
							store.AppendValues (this.linea, "Esperaba numeros despues del punto");
						}
						else {
							listaTok.Enqueue(new Token(cadena, (int)enumTok.NumeroFraccion, this.linea));                             
						}
						estado = (int)State.inicial;
						cadena = "";
						cont--;
					}
					break;                         
				case (int)State.verificaComentario:
					if (letra[cont].Equals('*'))
					{
						estado = (int)State.comentBloque;
					}
					else if (letra[cont].Equals('/'))
					{
						estado = (int)State.comentLinea;
					}
					else 
					{                             
						temporal = '/';
						estado = (int)State.tieneIgual;
						cont--;
					}
					break;
					// Si encuentra * , saltara a finBloque para
					// buscar una / y terminar el comentario
				case (int)State.comentBloque:
					if (letra[cont].Equals('*'))
					{
						estado = (int)State.finBloque;
					}
					if (letra[cont].Equals('\n'))
					{
						this.linea++;
					}
					break;
				case (int)State.finBloque:
					if (letra[cont].Equals('/'))
					{
						estado = (int)State.inicial;
					}
					else 
					{
						estado = (int)State.comentBloque;
					}
					if (letra[cont].Equals('\n'))
					{
						this.linea++;
					}
					break;
				case (int)State.comentLinea:
					if (letra[cont].Equals('\n'))
					{
						linea++;
						estado = (int)State.inicial;
					}
					break;

					// Estado para ver si hay un igual
				case (int)State.tieneIgual:
					switch (temporal) { 
					case '+':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token("+=",(int)enumTok.masIgual, this.linea));
						}
						else if (letra[cont].Equals('+'))
						{
							listaTok.Enqueue(new Token("++", (int)enumTok.masMas, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("+",(int)enumTok.mas, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					case '-':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token("-=", (int)enumTok.menosIgual, this.linea));

						}
						else if (letra[cont].Equals('-'))
						{
							listaTok.Enqueue(new Token("--",  (int)enumTok.menosMenos, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("-",(int)enumTok.menos, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					case '*':
						if (letra[cont].Equals('='))
						{						
							listaTok.Enqueue(new Token("*=", (int)enumTok.porIgual, this.linea));
						}
						if (letra[cont].Equals('*'))
						{						
							listaTok.Enqueue(new Token("**",(int)enumTok.porPor, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("*", (int)enumTok.por, this.linea));						
							cont--;
						}
						estado = (int)State.inicial;
						break;
					case '/':
						if (letra[cont].Equals('='))
						{						
							listaTok.Enqueue(new Token("/=",(int)enumTok.entreIgual, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("/", (int)enumTok.entre, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					case '>':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token(">=", (int)enumTok.mayorIgual, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token(">", (int)enumTok.mayor, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					case '<':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token("<=", (int)enumTok.menorIgual, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("<", (int)enumTok.menor, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					case '=':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token("==", (int)enumTok.igualIgual, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("=", (int)enumTok.igual, this.linea));
							cont--;
						}
						estado = (int)State.inicial;
						break;
					case '!':
						if (letra[cont].Equals('='))
						{
							listaTok.Enqueue(new Token("!=", (int)enumTok.diferente, this.linea));
						}
						else
						{
							listaTok.Enqueue(new Token("!", (int)enumTok.Not, this.linea));
							cont--;                                     
						}
						estado = (int)State.inicial;
						break;
					}
					break;
				}
			}
			if (estado == (int)State.comentBloque)
			{
				listaTok.Enqueue(new Token("error", (int)enumTok.Error, this.linea));
				store.AppendValues (this.linea, "Hay un comentario de bloque sin cerrar");
			}
			return listaTok;
		}
		public int isReservada(string buscar)
		{
			String[] palabras = {"if","then","else","fi","do"
				,"until","while","read","write","float"
				,"int","bool","program","and","or"
				,"not","for","true","false"};
				for (int i = 0; i < palabras.Length;i++ )
				{
					if(buscar.Equals(palabras[i])){
						return i;
					}
				}
			return (int)enumTok.Id;
		}
		public ListStore GetListStore()
		{
			return store;
		}
	}
}

