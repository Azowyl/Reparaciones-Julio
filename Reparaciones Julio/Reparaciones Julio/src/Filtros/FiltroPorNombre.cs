using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Filtros
{
	class FiltroPorNombre : Filtro
	{
		private string nombre;

		public FiltroPorNombre(string nombre)
		{
			this.nombre = nombre;
		}

		public List<Cliente> aplicar(List<Cliente> clientes)
		{
			List<Cliente> nuevaLista = new List<Cliente>();

			foreach(Cliente cliente in clientes)
			{
				if (cliente.Nombre.Contains(nombre)) nuevaLista.Add(cliente);
			}

			return nuevaLista;
		}
	}
}
