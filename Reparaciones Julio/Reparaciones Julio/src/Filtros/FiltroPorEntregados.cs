using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Filtros
{
	class FiltroPorEntregados : Filtro
	{
		public List<Cliente> aplicar(List<Cliente> clientes)
		{
			List<Cliente> nuevaLista = new List<Cliente>();

			foreach (Cliente cliente in clientes)
			{
				if (cliente.pedidoCompleto())
					nuevaLista.Add(cliente);
			}

			return nuevaLista;
		}
	}
}
