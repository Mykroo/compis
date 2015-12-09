using System;
using System.Collections;
namespace OCTO4
{
	public class TipoDato
	{
		string name;
		string valor;
		string tipo;
		public TipoDato ()
		{	
			name = "";
			valor = "";
			tipo = "null";
		}
		public TipoDato (string name,string valor,string tipo)
		{
			this.name = name;
			this.valor = valor;
			this.tipo = tipo;
		}
		public string GetName(){
			return name;
		}
		public string GetValor(){
			return valor;
		}
		public string GetTipo(){
			return tipo;
		}
		public void setvalor(string tp){
			this.valor = tp;
		}
	}
}

