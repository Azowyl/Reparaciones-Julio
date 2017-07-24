using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Situaciones
{
	public class SituacionEnEspera : Situacion
	{
		public override bool estaCompleto()
		{
			return false;
		}

		public override bool estaEnEspera()
		{
			return true;
		}

		public override bool estaVencido()
		{
			return false;
		}
	}
}
