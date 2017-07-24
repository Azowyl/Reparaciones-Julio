using Reparaciones_Julio.src.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Reparaciones_Julio.src.Interfaces;
using Reparaciones_Julio.src.Situaciones;

namespace Reparaciones_Julio.src.Pedidos
{
	public class PedidoVacio : Pedido
	{
		public override Fecha FechaEntrega
		{
			get
			{
				return null;
			}

			set
			{
				throw new PedidoInexistenteException();
			}
		}

		public override Fecha FechaIngreso
		{
			get
			{
				return null;
			}

			set
			{
				throw new PedidoInexistenteException();
			}
		}

		public override float Presupuesto
		{
			get
			{
				return 0;
			}

			set
			{
				throw new PedidoInexistenteException();
			}
		}

		public override string Reparacion
		{
			get
			{
				return "";
			}

			set
			{
				throw new PedidoInexistenteException();
			}
		}

		public override Situacion Situacion
		{
			get
			{
				return new SituacionIncompleto();
			}

			set
			{
				Situacion = new SituacionIncompleto();
			}
		}

		public override void completar()
		{
			throw new PedidoInexistenteException();
		}

		public override void enEspera()
		{
			throw new PedidoInexistenteException();
		}

		public override void vencer()
		{
			throw new PedidoInexistenteException();
		}
	}
}
