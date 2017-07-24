using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Situaciones
{
	public class SituacionIncompleto : Situacion
	{
		public override bool estaCompleto()
		{
			return false;
		}

		public override bool estaEnEspera()
		{
			return false;
		}

		public override bool estaVencido()
		{
			return false;
		}
	}
}
