using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reparaciones_Julio.src;
using Reparaciones_Julio.src.Excepciones;
using Reparaciones_Julio.src.Filtros;

namespace Reparaciones_Julio.test
{
	[TestClass]
	public class ListadoTest
	{

		[TestMethod]
		public void listadoGuardaElementosPorAnioYMes()
		{
			Listado listado = new Listado();
			Cliente cliente = new Cliente();
			listado.agregarElemento(cliente, Mes.FEBRERO, 2017);

			Assert.AreEqual(cliente, listado.getElementos(Mes.FEBRERO, 2017)[0]);
		}

		[TestMethod]
		public void listadoCon2Anios()
		{
			Listado listado = new Listado();

			Cliente primerCliente = new Cliente();
			Cliente segundoCliente = new Cliente();

			listado.agregarElemento(primerCliente, Mes.FEBRERO, 2017);
			listado.agregarElemento(segundoCliente, Mes.FEBRERO, 2018);

			Assert.AreEqual(primerCliente, listado.getElementos(Mes.FEBRERO, 2017)[0]);
			Assert.AreEqual(segundoCliente, listado.getElementos(Mes.FEBRERO, 2018)[0]);
		}

		[TestMethod]
		public void getTodosDevuelveTodosLosElementos()
		{
			Listado listado = new Listado();

			Cliente primerCliente = new Cliente();
			Cliente segundoCliente = new Cliente();

			listado.agregarElemento(primerCliente, Mes.FEBRERO, 2017);
			listado.agregarElemento(segundoCliente, Mes.FEBRERO, 2018);

			Assert.AreEqual(2, listado.getTodos().Count);
		}

		[TestMethod][ExpectedException(typeof(ValorInexistenteException))]
		public void getElementoDeAnioInexistenteLanzaExcepcion()
		{
			Listado listado = new Listado();
			Cliente cliente = new Cliente();

			listado.agregarElemento(cliente, Mes.FEBRERO, 2017);

			listado.getElementos(Mes.FEBRERO, 2019);
		}

		[TestMethod]
		public void borrarElementoBorraElElemento()
		{
			Listado listado = new Listado();

			Cliente primerCliente = new Cliente();
			Cliente segundoCliente = new Cliente();

			listado.agregarElemento(primerCliente, Mes.FEBRERO, 2017);
			listado.borrar(primerCliente);

			Assert.AreEqual(0, listado.getTodos().Count);
		}
	}
}
