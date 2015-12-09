using System;

namespace OCTO4
{
	public class Token
	{
		public Token ()
		{

		}
		string lexema;
		int tipoToken;
		int linea;
		public Token(string lexema,int tipoToken,int linea) 
		{
			this.lexema = lexema;
			this.tipoToken = tipoToken;
			this.linea = linea;
		}
		public String getLexema() 
		{ 
			return lexema; 
		}
		public void setLexema(string lexema) 
		{ 
			this.lexema = lexema; 
		}
		public int getLinea() 
		{
			return this.linea; 
		}
		public void setLinea(int linea) 
		{
			this.linea = linea; 
		}
		public int getTipoToken()
		{
			return this.tipoToken;
		}
		public void setTipoToken(int tipoToken)
		{
			this.tipoToken = tipoToken;
		}
	}
}

