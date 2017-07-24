using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src
{
	public static class Extenciones
	{

		public static Mes siguiente(this Mes mes)
		{
			int numeroDeMes = (int)mes;

			numeroDeMes++;

			return (Mes) Enum.Parse(typeof(Mes), numeroDeMes.ToString());
		}

	}
}
