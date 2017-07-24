using System;
using System.Drawing;

namespace Reparaciones_Julio.src
{
	[Serializable()]
	public class Configuracion
	{
		private int colorVencido;
		private int colorEspera;
		private int colorCompleto;

		private bool numeroAutomatico;

		public int ColorVencido { get { return colorVencido; } set { colorVencido = value; } }
		public int ColorEspera { get { return colorEspera; } set { colorEspera = value; } }
		public int ColorCompleto { get { return colorCompleto; } set { colorCompleto = value; } }

		public bool NumeroAutomatico { get { return numeroAutomatico; } set { numeroAutomatico = value; } }
	}
}
