using Reparaciones_Julio.src;
using Reparaciones_Julio.src.Excepciones;
using Reparaciones_Julio.src.Interfaces;
using System;
using System.Collections.Generic;

namespace Reparaciones_Julio.src
{
	public class Negocio
	{
		private Listado clientes;
		private List<Cliente> clientesFiltrados;

		public Negocio()
		{
			clientesFiltrados = new List<Cliente>();

			clientes = new Listado();
		}

		public void nuevoCliente(string nombre, int numero, string mail, long tel, string obs)
		{
			if (existeCliente(numero)) throw new ClienteExistenteException();

			Cliente nuevoCliente = new Cliente();

			nuevoCliente.Nombre = nombre;
			nuevoCliente.Numero = numero;
			nuevoCliente.Mail = mail;
			nuevoCliente.NumeroDeTelefono = tel;
			nuevoCliente.Observaciones = obs;

			Mes hoy = (Mes) Enum.Parse(typeof(Mes), DateTime.Today.Month.ToString());
			clientes.agregarElemento(nuevoCliente,hoy, DateTime.Today.Year);

			clientesFiltrados.Add(nuevoCliente);
		}

		private bool existeCliente(int numero)
		{
			try
			{
				getCliente(numero);
				return true;
			}
			catch(ClienteNoExistenteException exc)
			{
				return false;
			}
		}

		public int cantidadTotalDeClientes()
		{
			return getClientes().Count;
		}

		public Cliente getCliente(int numero)
		{
			Cliente clienteBuscado = getClientes().Find(x => x.Numero == numero);

			if (clienteBuscado == null) throw new ClienteNoExistenteException();
			else return clienteBuscado;
		}

		public Cliente getCliente(int numero, Mes mes, int anio)
		{
			Cliente clienteBuscado = getClientes(mes,anio).Find(x => x.Numero == numero);

			if (clienteBuscado == null) throw new ClienteNoExistenteException();
			else return clienteBuscado;
		}

		public List<Cliente> getClientes()
		{
			return  clientes.getTodos();
		}

		public void filtrarClientes(Filtro filtro)
		{
			clientesFiltrados = filtro.aplicar(getClientes());
		}

		public void filtrarClientes(Filtro filtro, Mes mes, int anio)
		{
			clientesFiltrados = filtro.aplicar(getClientes(mes, anio));
		}

		public List<Cliente> getClientesFiltrados()
		{
			return clientesFiltrados;
		}

		public void agregarPedido(int numCliente, Pedido pedido)
		{
			getCliente(numCliente).agregarPedido(pedido);
		}

		public bool pedidoDeClienteCompletado(int numeroDeCliente)
		{
			return getCliente(numeroDeCliente).pedidoCompleto();
		}

		public void completarPedidoDeCliente(int numCliente)
		{
			getCliente(numCliente).completarPedido();
		}

		public void borrarCliente(int numCliente)
		{
			Cliente clienteABorrar = this.getCliente(numCliente);

			clientes.borrar(clienteABorrar);
		}

		public void setPedidoDeClienteEnEspera(int numDeCliente)
		{
			getCliente(numDeCliente).setPedidoEnEspera();
		}

		public bool pedidoDeClienteEnEspera(int numDeCliente)
		{
			return getCliente(numDeCliente).estaEnEspera();
		}

		public void agregarCliente(Cliente cliente)
		{
			Mes hoy = (Mes)Enum.Parse(typeof(Mes), DateTime.Today.Month.ToString());

			clientes.agregarElemento(cliente, hoy, DateTime.Today.Year);
		}

		public List<Cliente> getClientes(Mes mes)
		{
			return clientes.getElementos(mes, DateTime.Today.Year);
		}

		public List<Cliente> getClientes(Mes mes, int anio)
		{
			return clientes.getElementos(mes, anio);
		}

		public void agregarCliente(Cliente cliente, Mes mes)
		{
			clientes.agregarElemento(cliente, mes, DateTime.Today.Year);
		}

		public void agregarCliente(Cliente cliente, Mes mes, int anio)
		{
			clientes.agregarElemento(cliente, mes, anio);
		}

		public List<int> getAniosConClientes()
		{
			return clientes.getAnios();
		}

		public void vencerPedidoDeCliente(int numDeCliente)
		{
			getCliente(numDeCliente).vencerPedido();
		}

		public bool pedidoDeClienteVencido(int numCliente)
		{
			return getCliente(numCliente).estaVencido();
		}
	}
}