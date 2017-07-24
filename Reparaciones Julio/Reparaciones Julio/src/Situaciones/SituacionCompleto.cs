using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reparaciones_Julio.src.Situaciones
{
	public class SituacionCompleto : Situacion
	{
		public override bool estaCompleto()
		{
			return true;
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
