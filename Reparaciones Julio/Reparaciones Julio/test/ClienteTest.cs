using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reparaciones_Julio.src;
using Reparaciones_Julio.src.Excepciones;
using System;

namespace Reparaciones_Julio.test
{
	[TestClass]
	public class ClienteTest
	{

		[TestMethod]
		public void completarPedidoCompletaElPedidoDelCliente()
		{
			Cliente cliente = new Cliente();

			cliente.agregarPedido(new ReparacionDeMotor());
			cliente.completarPedido();

			Assert.IsTrue(cliente.pedidoCompleto());
		}

		[TestMethod]
		public void ElPedidoDelClienteNoSeCompletaHastaInvocarACompletarPedido()
		{
			Cliente cliente = new Cliente();

			cliente.agregarPedido(new ReparacionDeMotor());

			Assert.IsFalse(cliente.pedidoCompleto());
		}

		[TestMethod][ExpectedException(typeof(PedidoInexistenteException))]
		public void completarPedidoCuandoNoHayPedidosLanzaExcepcion()
		{
			Cliente cliente = new Cliente();

			cliente.completarPedido();
		}

		[TestMethod]
		public void pedidoCompletoDevuelveFalseSiNoHayPedido()
		{
			Cliente cliente = new Cliente();

			Assert.IsFalse(cliente.pedidoCompleto());
		}
	}
}
