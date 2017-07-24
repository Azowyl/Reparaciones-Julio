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

namespace Reparaciones_Julio.Forms
{
	public partial class Editar : Form
	{
		private Cliente cliente;

		public Editar(Cliente clienteAEditar)
		{
			InitializeComponent();

			cliente = clienteAEditar;
			RellenarCampos(clienteAEditar);
		}

		private void RellenarCampos(Cliente cliente)
		{
			txtbEditarNombre.Text = cliente.Nombre;
			txtbEditarNumero.Text = cliente.Numero.ToString();
			txtbEditarMail.Text = cliente.Mail;
			txtbEditarObs.Text = cliente.Observaciones;
			txtbEditarPresupuesto.Text = cliente.pedido.Presupuesto.ToString();
			txtbEditarReparacion.Text = cliente.pedido.Reparacion;
			txtbEditarTel.Text = cliente.NumeroDeTelefono.ToString();

		}

		private void btnCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnGuardarEd_Click(object sender, EventArgs e)
		{
			int numCliente,presupuesto;
			long numTel;

			Int32.TryParse(txtbEditarNumero.Text, out numCliente);
			Int32.TryParse(txtbEditarPresupuesto.Text, out presupuesto);
			Int64.TryParse(txtbEditarTel.Text, out numTel);

			cliente.Nombre = txtbEditarNombre.Text;
			cliente.Numero = numCliente;
			cliente.Mail = txtbEditarMail.Text;
			cliente.Observaciones = txtbEditarObs.Text;
			cliente.pedido.Presupuesto = presupuesto;
			cliente.pedido.Reparacion = txtbEditarReparacion.Text;
			cliente.NumeroDeTelefono = numTel;

			this.Close();
		}
	}
}
