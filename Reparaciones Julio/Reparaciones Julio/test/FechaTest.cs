using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reparaciones_Julio.src;

namespace Reparaciones_Julio.test
{
	[TestClass]
	public class FechaTest
	{

		[TestMethod]
		public void compararFechaDevuelveTrueSiCoincidenDiaMesYAnio()
		{
			Fecha unaFecha = new Fecha(8, 1, 2017);
			Fecha otraFecha = new Fecha(8, 1, 2017);

			Assert.IsTrue(unaFecha.esIgual(otraFecha));
		}

		[TestMethod]
		public void compararFechaDevuelveFalseSiNoCoincidenDiaMesYAnio()
		{
			Fecha unaFecha = new Fecha(8, 1, 2017);
			Fecha otraFecha = new Fecha(9, 1, 2017);

			Assert.IsFalse(unaFecha.esIgual(otraFecha));
		}

		[TestMethod]
		public void siguienteADevuleveElMesSiguiente()
		{
			Mes enero = Mes.ENERO;
			Mes junio = Mes.JUNIO;

			Assert.AreEqual(Mes.FEBRERO, enero.siguiente());
			Assert.AreEqual(Mes.JULIO, junio.siguiente());
		}

	}
}
