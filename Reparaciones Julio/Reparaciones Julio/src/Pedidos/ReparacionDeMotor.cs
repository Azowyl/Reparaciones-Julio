using System;
using System.Xml.Serialization;
using Reparaciones_Julio.src.Interfaces;
using Reparaciones_Julio.src.Situaciones;

namespace Reparaciones_Julio.src
{
	public class ReparacionDeMotor : Pedido
	{
		private string rep;
		private float presupuesto;
		private Fecha fechaIngreso;
		private Fecha fechaEntrega;
		private Situacion situacion;

		public ReparacionDeMotor()
		{
			situacion = new SituacionIncompleto();
			presupuesto = 0;
		}

		public override float Presupuesto
		{
			get
			{
				return presupuesto;
			}
			set
			{
				presupuesto = value;
				if (situacion.estaEnEspera() && presupuesto != 0)
					situacion = new SituacionIncompleto();
			}
		}

		public override string Reparacion
		{
			get
			{
				return rep;
			}

			set
			{
				rep = value;
			}
		}

		public override Fecha FechaIngreso
		{
			get
			{
				return fechaIngreso;
			}

			set
			{
				fechaIngreso = value;
			}
		}

		public override Fecha FechaEntrega
		{
			get
			{
				return fechaEntrega;
			}

			set
			{
				fechaEntrega = value;
			}
		}

		public override Situacion Situacion
		{
			get
			{
				return situacion;
			}

			set
			{
				situacion = value;
			}
		}

		public override void completar()
		{
			situacion = new SituacionCompleto();
		}

		public override void enEspera()
		{
			if (!situacion.estaVencido())
				situacion = new SituacionEnEspera();
		}

		public override void vencer()
		{
			if (!situacion.estaCompleto())
				situacion = new SituacionVencido();
		}
	}
}
