using Reparaciones_Julio.src.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src
{
	public class Listado
	{

		private List<List<List<Cliente>>> elementos;
		private List<int> anios;
		
		public Listado()
		{
			elementos = new List<List<List<Cliente>>>();
			anios = new List<int>();
		}

		public void agregarElemento(Cliente elemento, Mes mes, int anio)
		{

			int indexAnio;

			if (!anios.Contains(anio))
			{
				crearAnio(anio);
				indexAnio = anios.Count - 1;
			}
			else
				indexAnio = anios.IndexOf(anio);

			elementos[indexAnio][(int)mes - 1].Add(elemento);
		}

		private void crearAnio(int anio)
		{
			anios.Add(anio);

			elementos.Add(new List<List<Cliente>>());

			for( int i = 0; i < 12; i++)
			{
				elementos[anios.Count - 1].Add(new List<Cliente>());
			}
		}

		public List<Cliente> getElementos(Mes mes, int anio)
		{
			if (!anios.Contains(anio)) throw new ValorInexistenteException();

			return elementos[anios.IndexOf(anio)][(int)mes - 1];
		}

		public List<Cliente> getTodos()
		{
			List<Cliente> listaADevolver = new List<Cliente>();

			foreach(List<List<Cliente>> anio in elementos)
			{
				foreach(List<Cliente> mes in anio)
				{
					listaADevolver.AddRange(mes);
				}
			}

			return listaADevolver;
		}

		public void borrar(Cliente clienteABorrar)
		{
			foreach (List<List<Cliente>> anio in elementos)
			{
				foreach (List<Cliente> mes in anio)
				{
					if (mes.Contains(clienteABorrar))
					{
						mes.Remove(clienteABorrar);
						return;
					}
				}
			}
		}

		public List<int> getAnios()
		{
			return anios;
		}

	}
}
