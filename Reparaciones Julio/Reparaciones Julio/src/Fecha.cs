using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src
{
	[Serializable()]
	public class Fecha
	{
		private int dia;
		private int mes;
		private int anio;

		public int Dia { get { return dia; } set { dia = value; } }
		public int Mes { get { return mes; } set { mes = value; } }
		public int Anio { get { return anio; } set { anio = value; } }

		public Fecha() { }

		public Fecha(int dia, int mes, int anio)
		{
			this.dia = dia;
			this.mes = mes;
			this.anio = anio;
		}

		public bool esIgual(Fecha fecha)
		{
			return ( dia == fecha.Dia && mes == fecha.Mes && anio == fecha.Anio);
		}

		public override string ToString()
		{
			return dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
		}

		public bool yaPaso()
		{
			if (DateTime.Today.Year == anio)
			{
				if (DateTime.Today.Month == mes)
				{
					if (DateTime.Today.Day <= dia)
						return false;
					else
						return true;
				}
				else
					if (DateTime.Today.Month < mes)
					return false;
				else
					return true;
			}
			else
				if (DateTime.Today.Year < anio)
				return false;
			else
				return true;
		}
	}
}
