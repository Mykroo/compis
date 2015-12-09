using System;

namespace OCTO4
{
	public class Nodo
	{
		public Nodo[] hijos;
		public Nodo hermano;
		public Token token;
		public Nodo(Token token) 
		{
			hijos = new Nodo[5];
			this.token = token;
		}
		public Nodo() 
		{       
		}
	}
}

