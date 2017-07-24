using Reparaciones_Julio.src.Interfaces;
using Reparaciones_Julio.src.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reparaciones_Julio.src
{
	[XmlInclude(typeof(ReparacionDeMotor))]
	[XmlInclude(typeof(PedidoVacio))]
	public abstract class Pedido
	{
		public abstract string Reparacion { get; set; }
		public abstract float Presupuesto { get; set; }
		public abstract Fecha FechaIngreso { get; set; }
		public abstract Fecha FechaEntrega { get; set; }
		public abstract Situacion Situacion { get; set; }

		public abstract void completar();
		public abstract void enEspera();
		public abstract void vencer();
	}
}
