using Reparaciones_Julio.src.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src
{
	[Serializable()]
	public class Cliente
	{
		public Pedido pedido;

		private string nombre;
		private int numero;
		private string mail;
		private long numeroDeTelefono;
		private string observaciones;

		public string Nombre { get { return nombre; } set { nombre = value; } }
		public string Mail { get { return mail; } set { mail = value; } }
		public int Numero { get { return numero; } set { numero = value; } }
		public long NumeroDeTelefono { get { return numeroDeTelefono; } set { numeroDeTelefono = value; } }
		public string Observaciones { get { return observaciones; } set { observaciones = value; } }
		public Pedido Pedido { get { return pedido; } }
		
		public Cliente()
		{
			pedido = new PedidoVacio();
		}

		public void agregarPedido(Pedido pedido)
		{
			this.pedido = pedido;

			if (pedido.Presupuesto == 0)
				pedido.enEspera();
		}

		public void completarPedido()
		{
			pedido.completar();
		}

		public bool pedidoCompleto()
		{
			return pedido.Situacion.estaCompleto();
		}

		public void setPedidoEnEspera()
		{
			pedido.enEspera();
		}

		public void vencerPedido()
		{
			pedido.vencer();
		}

		public bool estaEnEspera()
		{
			return pedido.Situacion.estaEnEspera();
		}

		public bool estaVencido()
		{
			return pedido.Situacion.estaVencido();
		}
	}
}
