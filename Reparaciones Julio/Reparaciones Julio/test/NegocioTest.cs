using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reparaciones_Julio.src;
using Reparaciones_Julio.src.Excepciones;
using Reparaciones_Julio.src.Filtros;

namespace Reparaciones_Julio.test
{
	[TestClass]
	public class NegocioTest
	{

		[TestMethod]
		public void nuevoClienteCreaNuevoCliente()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");

			Assert.AreEqual(1, negocio.cantidadTotalDeClientes());
		}

		[TestMethod]
		public void cantidadTotalDeClientesDevuelveTodosLosClientesHastaElMomento()
		{
			Negocio negocio = new Negocio();
			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", 2, "queseyo", 2345, "es un capo");
			negocio.nuevoCliente("olliver", 3, "queseyo", 2346, "es un capo");

			Assert.AreEqual(3, negocio.cantidadTotalDeClientes());
		}

		[TestMethod]
		public void nuevoClienteGuardaLosDatosDelCliente()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");

			Assert.AreEqual("facundo", negocio.getCliente(1).Nombre);
		}

		[TestMethod][ExpectedException(typeof(ClienteNoExistenteException))]
		public void getClienteInexistenteLanzaExcepcion()
		{
			Negocio negocio = new Negocio();

			negocio.getCliente(1);
		}

		[TestMethod]
		public void filtrarClientesPorNombreDevuelveTodosLosClientesQueContienenLaStringPasada()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", 2, "queseyo", 2345, "es un capo");
			negocio.nuevoCliente("olliver", 3, "queseyo", 2346, "es un capo");

			negocio.filtrarClientes(new FiltroPorNombre("a"));

			Assert.AreEqual(2, negocio.getClientesFiltrados().Count);
			Assert.AreEqual("facundo", negocio.getClientesFiltrados()[0].Nombre);
			Assert.AreEqual("agustin", negocio.getClientesFiltrados()[1].Nombre);
		}

		[TestMethod]
		public void pedidoCompletoDevuelveFalseSinoSeCompletoElPedido()
		{
			Negocio negocio = new Negocio();

			int numeroDeCliente = 1;

			negocio.nuevoCliente("facundo", numeroDeCliente, "queseyo", 234, "es un capo");
			negocio.agregarPedido(numeroDeCliente, new ReparacionDeMotor());

			Assert.IsFalse(negocio.pedidoDeClienteCompletado(numeroDeCliente));
		}

		[TestMethod]
		public void pedidoCompletoDevuelveTrueSiSeCompletoElPedido()
		{
			Negocio negocio = new Negocio();

			int numeroDeCliente = 1;

			negocio.nuevoCliente("facundo", numeroDeCliente, "queseyo", 234, "es un capo");
			negocio.agregarPedido(numeroDeCliente, new ReparacionDeMotor());
			negocio.completarPedidoDeCliente(numeroDeCliente);

			Assert.IsTrue(negocio.pedidoDeClienteCompletado(numeroDeCliente));
		}

		[TestMethod]
		public void filtrarClientesPorEntregadosDevuelveTodosLosClientesConPedidoCompleto()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", 2, "queseyo", 2345, "es un capo");
			negocio.nuevoCliente("olliver", 3, "queseyo", 2346, "es un capo");

			negocio.agregarPedido(1, new ReparacionDeMotor());
			negocio.agregarPedido(2, new ReparacionDeMotor());
			negocio.agregarPedido(3, new ReparacionDeMotor());

			negocio.completarPedidoDeCliente(1);
			negocio.completarPedidoDeCliente(3);

			negocio.filtrarClientes(new FiltroPorEntregados());

			Assert.AreEqual(2, negocio.getClientesFiltrados().Count);
			Assert.AreEqual("facundo", negocio.getClientesFiltrados()[0].Nombre);
			Assert.AreEqual("olliver", negocio.getClientesFiltrados()[1].Nombre);
		}

		[TestMethod]
		public void filtrarClientesPorFechaDeEntregaDevuelveTodosLosClientesConMismaFechaDeEntrega()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", 2, "queseyo", 2345, "es un capo");
			negocio.nuevoCliente("olliver", 3, "queseyo", 2346, "es un capo");

			negocio.agregarPedido(1, new ReparacionDeMotor());
			negocio.agregarPedido(2, new ReparacionDeMotor());
			negocio.agregarPedido(3, new ReparacionDeMotor());

			negocio.completarPedidoDeCliente(1);
			negocio.completarPedidoDeCliente(3);

			negocio.filtrarClientes(new FiltroPorEntregados());

			Assert.AreEqual(2, negocio.getClientesFiltrados().Count);
			Assert.AreEqual("facundo", negocio.getClientesFiltrados()[0].Nombre);
			Assert.AreEqual("olliver", negocio.getClientesFiltrados()[1].Nombre);
		}

		[TestMethod][ExpectedException(typeof(PedidoInexistenteException))]
		public void completarPedidoCuandoNoHayPedidoLanzaExcepcion()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");

			negocio.completarPedidoDeCliente(1);
		}

		[TestMethod]
		public void borrarClienteBorraElClienteDelNegocio()
		{
			Negocio negocio = new Negocio();
			int numCliente = 1;
			bool borrado = false;

			negocio.nuevoCliente("facundo", numCliente, "queseyo", 234, "es un capo");
			negocio.borrarCliente(numCliente);

			try
			{
				negocio.getCliente(numCliente);
			}
			catch (ClienteNoExistenteException e)
			{
				borrado = true;
			}

			Assert.IsTrue(borrado);
		}

		[TestMethod]
		public void getClientesFiltradosSinAplicarFiltroDevuelveTodosLosClientes()
		{
			Negocio negocio = new Negocio();

			negocio.nuevoCliente("facundo", 1, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", 2, "queseyo", 2345, "es un capo");
			negocio.nuevoCliente("olliver", 3, "queseyo", 2346, "es un capo");

			Assert.AreEqual(3, negocio.getClientesFiltrados().Count);
		}

		[TestMethod]
		public void enEsperaDevuelveTrueSiElPedidoEstaEnEspera()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;

			negocio.nuevoCliente("facundo", numDeCliente, "queseyo", 234, "es un capo");
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());

			negocio.setPedidoDeClienteEnEspera(numDeCliente);

			Assert.IsTrue(negocio.pedidoDeClienteEnEspera(numDeCliente));
		}

		[TestMethod]
		public void enEsperaDevuelveFalseSiElPedidoNoEstaEnEspera()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Pedido pedido = new ReparacionDeMotor();

			pedido.Presupuesto = 100;	//pedido sin presupuesto esta en espera

			negocio.nuevoCliente("facundo", numDeCliente, "queseyo", 234, "es un capo");
			negocio.agregarPedido(numDeCliente, pedido);

			Assert.IsFalse(negocio.pedidoDeClienteEnEspera(numDeCliente));
		}

		[TestMethod]
		public void agregarClienteAgregaElClientePasado()
		{
			Negocio negocio = new Negocio();

			negocio.agregarCliente(new Cliente());

			Assert.AreEqual(1, negocio.cantidadTotalDeClientes());
		}

		[TestMethod]
		public void crearClienteCreaClienteEnElMesQueFueAgregado()
		{
			//hoy es 19 de febrero

			Negocio negocio = new Negocio();
			int numDeCliente = 1;

			negocio.nuevoCliente("facundo", numDeCliente, "queseyo", 234, "pero que facha");

			Assert.AreEqual("facundo", negocio.getClientes(Mes.FEBRERO)[0].Nombre);
		}

		[TestMethod]
		public void agregarClienteEnMesAgregaClienteEnElMesPasadoComoParametro()
		{
			Negocio negocio = new Negocio();
			Cliente cliente = new Cliente();

			negocio.agregarCliente(cliente, Mes.MARZO);

			Assert.AreEqual(cliente, negocio.getClientes(Mes.MARZO)[0]);
		}

		[TestMethod]
		public void agregarClienteEnMesYAnioAgregaClienteEnElMesYAnioPasadosComoParametro()
		{
			Negocio negocio = new Negocio();
			Cliente cliente = new Cliente();

			negocio.agregarCliente(cliente, Mes.MARZO, 2017);

			Assert.AreEqual(cliente, negocio.getClientes(Mes.MARZO, 2017)[0]);
		}

		[TestMethod]
		public void getAniosConClientesDevulveUnaListaConLosAniosEnLosQueHuboClientes()
		{
			Negocio negocio = new Negocio();
			Cliente cliente = new Cliente();

			negocio.agregarCliente(cliente, Mes.MARZO, 2017);
			negocio.agregarCliente(cliente, Mes.MARZO, 2018);
			negocio.agregarCliente(cliente, Mes.MARZO, 2019);

			Assert.AreEqual(3, negocio.getAniosConClientes().Count);
			Assert.AreEqual(2017, negocio.getAniosConClientes()[0]);
			Assert.AreEqual(2018, negocio.getAniosConClientes()[1]);
			Assert.AreEqual(2019, negocio.getAniosConClientes()[2]);
		}

		[TestMethod]
		public void filtrarClientesPorMesYAnioFiltraSoloLosClientesDeEseMesYAnio()
		{
			Negocio negocio = new Negocio();

			Cliente primerCliente = new Cliente();
			Cliente segundoCliente = new Cliente();
			Cliente tercerCliente = new Cliente();

			primerCliente.Nombre = "Facundo";
			segundoCliente.Nombre = "Matias";
			tercerCliente.Nombre = "Julio";

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarCliente(segundoCliente, Mes.FEBRERO, 2017);
			negocio.agregarCliente(tercerCliente, Mes.FEBRERO, 2017);

			negocio.filtrarClientes(new FiltroPorNombre("Facundo"),Mes.FEBRERO, 2017);

			Assert.AreEqual(0, negocio.getClientesFiltrados().Count);
		}

		[TestMethod][ExpectedException(typeof(ClienteNoExistenteException))]
		public void getClienteDeMesYAnioDevuelveElClienteCreadoEnEsaFecha()
		{
			Negocio negocio = new Negocio();
			Cliente cliente = new Cliente();

			int numCliente = 1;

			cliente.Numero = numCliente;

			negocio.agregarCliente(cliente, Mes.MARZO, 2017);

			negocio.getCliente(numCliente, Mes.FEBRERO, 2017);
		}

		[TestMethod]
		[ExpectedException(typeof(ClienteExistenteException))]
		public void agregarClienteConNumeroYaExistenteLanzaExcepcion()
		{
			Negocio negocio = new Negocio();

			int numCliente = 1;

			negocio.nuevoCliente("facundo", numCliente, "queseyo", 234, "es un capo");
			negocio.nuevoCliente("agustin", numCliente, "queseyo", 2345, "es un capo");
		}

		[TestMethod]
		public void pedidoNoPuedeEstarEnEsperaUnaVezVencido()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Cliente primerCliente = new Cliente();

			primerCliente.Numero = numDeCliente;

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());
			negocio.vencerPedidoDeCliente(1);
			negocio.setPedidoDeClienteEnEspera(1);

			Assert.IsFalse(negocio.pedidoDeClienteEnEspera(numDeCliente));
		}

		[TestMethod]
		public void vencerPedidoVencePedidoDeCliente()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;

			negocio.nuevoCliente("facundo", numDeCliente, "", 2, "");
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());			
			negocio.vencerPedidoDeCliente(numDeCliente);

			Assert.IsTrue(negocio.pedidoDeClienteVencido(numDeCliente));
		}

		[TestMethod]
		public void pedidoVencidoSePuedeCompletar()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Cliente primerCliente = new Cliente();

			primerCliente.Numero = numDeCliente;

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());

			negocio.vencerPedidoDeCliente(1);
			negocio.completarPedidoDeCliente(1);

			Assert.IsTrue(negocio.pedidoDeClienteCompletado(numDeCliente));
		}

		[TestMethod]
		public void pedidoCompletoNoPuedeVencer()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Cliente primerCliente = new Cliente();

			primerCliente.Numero = numDeCliente;

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());

			negocio.completarPedidoDeCliente(1);
			negocio.vencerPedidoDeCliente(1);

			Assert.IsTrue(negocio.pedidoDeClienteCompletado(numDeCliente));
		}

		[TestMethod]
		public void pedidoSinPresupuestoEsPedidoEnEspera()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Cliente primerCliente = new Cliente();

			primerCliente.Numero = numDeCliente;

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());

			Assert.IsTrue(negocio.pedidoDeClienteEnEspera(numDeCliente));
		}

		[TestMethod]
		public void alCambiarPresupuestoDeCeroAOtroValorPedidoDejaDeEstarEnEspera()
		{
			Negocio negocio = new Negocio();
			int numDeCliente = 1;
			Cliente primerCliente = new Cliente();

			primerCliente.Numero = numDeCliente;

			negocio.agregarCliente(primerCliente, Mes.MARZO, 2017);
			negocio.agregarPedido(numDeCliente, new ReparacionDeMotor());

			negocio.getCliente(numDeCliente).Pedido.Presupuesto = 300;

			Assert.IsFalse(negocio.pedidoDeClienteEnEspera(numDeCliente));
		}
	}
}
