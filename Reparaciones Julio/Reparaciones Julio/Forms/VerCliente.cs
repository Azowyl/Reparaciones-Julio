using Reparaciones_Julio.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reparaciones_Julio
{
	public partial class VerCliente : Form
	{
		private Cliente cliente;

		public VerCliente(Cliente cliente)
		{
			InitializeComponent();
			this.cliente = cliente;
		}

		private void VerClientes_Load(object sender, EventArgs e)
		{
			try
			{
				lblNombreCliente.Text = cliente.Nombre;
				lblNumeroCliente.Text = cliente.Numero.ToString();
				lblMailCliente.Text = cliente.Mail;
				lblTelCliente.Text = cliente.NumeroDeTelefono.ToString();
				lblPresupuestoCliente.Text = cliente.Pedido.Presupuesto.ToString();
				lblFechaIngresoCliente.Text = cliente.Pedido.FechaIngreso.ToString();
				lblFechaEntregaCliente.Text = cliente.Pedido.FechaEntrega.ToString();
				lblReparacionesCliente.Text = cliente.Pedido.Reparacion;
				lblObsCliente.Text = cliente.Observaciones;
			}
			catch(Exception exc) { }	
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void btnSalir_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
