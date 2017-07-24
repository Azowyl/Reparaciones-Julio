using Reparaciones_Julio.Forms;
using Reparaciones_Julio.src;
using Reparaciones_Julio.src.Excepciones;
using Reparaciones_Julio.src.Filtros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Reparaciones_Julio
{
	public partial class ReparacionesJulio : Form
	{
		private const string DATOS_INCORRECTOS = "Datos Incorrectos";
		private const string EXITO = "Cliente Creado Con Exito";
		private const string VERSION = "1.0";
		private string NOMBRE_ARCHIVO = "\\Reparaciones_Julio_";
		private string NOMBRE_CARPETA = "\\Reparaciones_Julio_";

		private Color COLOR_COMPLETADO = Color.Blue;
		private Color COLOR_ESPERA = Color.Yellow;
		private Color COLOR_VENCIDO = Color.Red;

		private bool numeroDeClienteAuto = false;
		private int numeroDeCliente;

		private Negocio negocio;
		private List<Cliente> clientesMostrados;

		private Mes mesMostrado;
		private int anioMostrado;

		public ReparacionesJulio()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			negocio = new Negocio();

			string folderPath = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_ARCHIVO;

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			readXML();

			chequearVencidos();
	
			clientesMostrados = new List<Cliente>();

			dtpIngreso.MaxDate = DateTime.Today;
			dtpEntrega.MinDate = DateTime.Today;
		}

		private void chequearVencidos()
		{
			foreach(Cliente cliente in negocio.getClientes())
			{
				if (cliente.Pedido.FechaEntrega.yaPaso() && !cliente.pedidoCompleto())
					cliente.Pedido.vencer();
			}
		}

		private int getNumeroClienteMayor()
		{
			if (negocio.cantidadTotalDeClientes() == 0)
				return 0;

			int max = 0;

			foreach(Cliente cliente in negocio.getClientes())
			{
				if (cliente.Numero > max)
					max = cliente.Numero;
			}

			return max;
		}

		#region MENU PRINCIPAL

		private void btnNuevoCliente_Click_1(object sender, EventArgs e)
		{
			panelMenu.Visible = false;
			boxNuevoCliente.Visible = true;
			limpiarCampos();

			if (numeroDeClienteAuto)
			{
				numeroDeCliente = getNumeroClienteMayor() + 1;
				txtbNumero.ReadOnly = true;
			}
			else
				txtbNumero.ReadOnly = false;
				
		}

		private void btnVerClientes_Click(object sender, EventArgs e)
		{
			panelMenu.Visible = false;
			panelOpcVerClientes.Visible = true;
		}

		private void inicializarVerClientes(List<Cliente> clientes, Mes mes, int anio)
		{
			dataGridView.Rows.Clear();
			txtbBusqueda.Clear();

			mesMostrado = mes;
			anioMostrado = anio;

			if (clientes != null)
				mostrarClientes(clientes);

			if (seEstanMostrandoTodos())
			{
				lblFecha.Text = "Todos";
			}
			else
				lblFecha.Text = mes + " " + anio.ToString();
		}

		private void limpiarCampos()
		{
			txtbNombre.Clear();
			txtbNumero.Clear();
			txtbObs.Clear();
			txtbPresupuesto.Clear();
			txtbReparacion.Clear();
			txtbTel.Clear();
			txtbMail.Clear();

			lblInformacion.Visible = false;
		}

		private void mostrarClientes(List<Cliente> clientes)
		{
			foreach (Cliente cliente in clientes)
			{
	
				string[] row = { cliente.Numero.ToString(), cliente.Nombre, cliente.NumeroDeTelefono.ToString(), cliente.Pedido.Reparacion, cliente.Pedido.FechaIngreso.ToString(), cliente.Pedido.FechaEntrega.ToString(), "$" + cliente.Pedido.Presupuesto.ToString() };

				dataGridView.Rows.Add(row);
				clientesMostrados.Add(cliente);

				int lastRowIndex = dataGridView.Rows.Count - 1;
				PintarFilaSegunEstado(dataGridView.Rows[lastRowIndex], cliente );
				
			}
		}

		private void PintarFilaSegunEstado(DataGridViewRow row, Cliente cliente)
		{
			if (cliente.pedidoCompleto())
				pintarFila(row, COLOR_COMPLETADO);
			else
				if (cliente.estaEnEspera())
				pintarFila(row, COLOR_ESPERA);
			else
				if (cliente.estaVencido())
				pintarFila(row, COLOR_VENCIDO);
		}

		private void btnVerClientesActuales_Click(object sender, EventArgs e)
		{
			panelMenu.Visible = false;
			panelVerCliente.Visible = true;

			Mes mesDeHoy = (Mes)Enum.Parse(typeof(Mes), DateTime.Today.Month.ToString());

			List<Cliente> clientesAMostrar;

			if (negocio.cantidadTotalDeClientes() == 0)
				clientesAMostrar = new List<Cliente>();
			else
				clientesAMostrar = negocio.getClientes(mesDeHoy, DateTime.Today.Year);

			inicializarVerClientes(clientesAMostrar, mesDeHoy, DateTime.Today.Year);
		}

		private void btnAjustes_Click(object sender, EventArgs e)
		{
			inicializarAjustes();
		}

		#endregion

		#region CREAR CLIENTE

		private void btnMenu_Click(object sender, EventArgs e)
		{
			boxNuevoCliente.Visible = false;
			panelMenu.Visible = true;
		}

		private void btnCrear_Click(object sender, EventArgs e)
		{
			if (txtbPresupuesto.Text.Length == 0)
				txtbPresupuesto.Text = "0";

			if (datosValidos())
			{

				int numero = 0;
				long tel = 0;

				if (numeroDeClienteAuto)
					numero = numeroDeCliente;
				else
					Int32.TryParse(txtbNumero.Text, out numero);

				Int64.TryParse(txtbTel.Text, out tel);

				negocio.nuevoCliente(txtbNombre.Text.ToLower(), numero, txtbMail.Text.ToLower(), tel, txtbObs.Text.ToLower());

				Pedido reparacion = new ReparacionDeMotor();

				reparacion.Reparacion = txtbReparacion.Text;
				reparacion.Presupuesto = float.Parse(txtbPresupuesto.Text);
				reparacion.FechaIngreso = new Fecha(dtpIngreso.Value.Date.Day, dtpIngreso.Value.Date.Month, dtpIngreso.Value.Date.Year);
				reparacion.FechaEntrega = new Fecha(dtpEntrega.Value.Date.Day, dtpEntrega.Value.Date.Month, dtpEntrega.Value.Date.Year);

				negocio.agregarPedido(numero, reparacion);

				lblInformacion.ForeColor = Color.Green;
				lblInformacion.Text = EXITO;
				lblInformacion.Visible = true;
			}
			else
			{
				lblInformacion.ForeColor = Color.Red;
				lblInformacion.Text = DATOS_INCORRECTOS;
				lblInformacion.Visible = true;
			}
		}

		private bool datosValidos()
		{
			int num;
			bool esNumero = Int32.TryParse(txtbNombre.Text, out num);

			bool nombreValido = txtbNombre.Text.Length != 0 && !esNumero;

			bool clienteYaExiste = true;

			bool numeroValido;

			if (numeroDeClienteAuto)
				numeroValido = true;
			else
			{
				esNumero = Int32.TryParse(txtbNumero.Text, out num);

				try
				{
					negocio.getCliente(num);
				}
				catch (ClienteNoExistenteException exc)
				{
					clienteYaExiste = false;
				}

				numeroValido = esNumero && !clienteYaExiste;
			}
		
			return numeroValido && nombreValido;
		}

		#endregion

		#region VER CLIENTES

		private void btnVolver_Click(object sender, EventArgs e)
		{
			panelMenu.Visible = true;
			panelVerCliente.Visible = false;
		}

		private void btnBuscar_Click(object sender, EventArgs e)
		{
			if (dataGridView.Rows.Count == 0)       //si no hay clientes mostrados no hay nada que buscar
				return;

			try
			{
				if (seEstanMostrandoTodos())
					negocio.filtrarClientes(new FiltroPorNombre(txtbBusqueda.Text));
				else
					negocio.filtrarClientes(new FiltroPorNombre(txtbBusqueda.Text), mesMostrado, anioMostrado);
			}
			catch (ValorInexistenteException) { }

			if (negocio.getClientesFiltrados().Count == 0)
			{
				int numCliente;
				Int32.TryParse(txtbBusqueda.Text, out numCliente);

				Cliente clienteBuscado;

				try
				{
					if (seEstanMostrandoTodos())
						clienteBuscado = negocio.getCliente(numCliente);
					else
						clienteBuscado = negocio.getCliente(numCliente, mesMostrado, anioMostrado);

					inicializarVerClientes(new List<Cliente>() { clienteBuscado }, mesMostrado, anioMostrado);
				}
				catch(Exception exc)
				{
					inicializarVerClientes(new List<Cliente>(), mesMostrado, anioMostrado);
				}

			}
			else
				try
				{
					inicializarVerClientes(negocio.getClientesFiltrados(), mesMostrado, anioMostrado);
				}
				catch(ValorInexistenteException exc) { }
		}

		private void btnVerDetalles_Click(object sender, EventArgs e)
		{
			Cliente clienteAVerDetalles = null;
			int numDeCliente = 0;

			if (dataGridView.Rows.Count > 0 && dataGridView.CurrentRow.Cells["Numero"].Value != null)
				Int32.TryParse(dataGridView.CurrentRow.Cells["Numero"].Value.ToString(), out numDeCliente);

			if (numDeCliente != 0)
				clienteAVerDetalles = negocio.getCliente(numDeCliente);

			if (clienteAVerDetalles != null)
			{
				Form formVerCliente = new VerCliente(clienteAVerDetalles);
				formVerCliente.Show();
			}

		}

		private void btnCompletar_Click(object sender, EventArgs e)
		{
			if (dataGridView.Rows.Count == 0)
				return;

			int num;
			string numeroCliente = dataGridView.CurrentRow.Cells["Numero"].Value.ToString();

			Int32.TryParse(numeroCliente, out num);

			try
			{
				negocio.completarPedidoDeCliente(num);
				pintarFila(dataGridView.CurrentRow, COLOR_COMPLETADO);
			}
			catch(ClienteNoExistenteException exc)
			{
				MessageBox.Show("Cliente_No_Existente_Exception");
			}
		}

		private void pintarFila(DataGridViewRow row, Color color)
		{
			for ( int i = 0; i < row.Cells.Count; i++)
			{
				row.Cells[i].Style.ForeColor = color;
			}
		}

		private void btnEditar_Click(object sender, EventArgs e)
		{
			if (dataGridView.Rows.Count == 0)
				return;

			int num;
			string numeroCliente = dataGridView.CurrentRow.Cells["Numero"].Value.ToString();

			Int32.TryParse(numeroCliente, out num);

			try
			{
				Editar editarForm = new Editar(negocio.getCliente(num));
				editarForm.FormClosing += actualizar;
				editarForm.ShowDialog();
			}
			catch (ClienteNoExistenteException exc) { }
		}

		private void actualizar(object sender, FormClosingEventArgs e)
		{
			if (seEstanMostrandoTodos())
				inicializarVerClientes(negocio.getClientes(), mesMostrado, anioMostrado);
			else
				inicializarVerClientes(negocio.getClientes(mesMostrado, anioMostrado), mesMostrado, anioMostrado);
		}

		private bool seEstanMostrandoTodos()
		{
			return anioMostrado == 0;
		}

		private void btnEnEspera_Click(object sender, EventArgs e)
		{
			if (dataGridView.Rows.Count == 0)
				return;

			int num;
			string numeroCliente = dataGridView.CurrentRow.Cells["Numero"].Value.ToString();

			Int32.TryParse(numeroCliente, out num);

			try
			{
				negocio.setPedidoDeClienteEnEspera(num);
			}
			catch(ClienteNoExistenteException exc)
			{
				MessageBox.Show("Cliente_No_Existente_Exception");
			}

			PintarFilaSegunEstado(dataGridView.CurrentRow, negocio.getCliente(num));
		}

		private void btnBorrar_Click(object sender, EventArgs e)
		{
			if (dataGridView.Rows.Count == 0)
				return;

			int num;
			string numeroCliente = dataGridView.CurrentRow.Cells["Numero"].Value.ToString();
			string nombreCliente = dataGridView.CurrentRow.Cells["Nombre"].Value.ToString();

			Int32.TryParse(numeroCliente, out num);

			DialogResult dialogResult = MessageBox.Show("Esta Seguro de borrar al cliente: " + nombreCliente + "?", nombreCliente, MessageBoxButtons.OKCancel);

			if (dialogResult == DialogResult.OK)
			{
				try
				{
					clientesMostrados.Remove(negocio.getCliente(num));
					negocio.borrarCliente(num);
					dataGridView.Rows.Remove(dataGridView.CurrentRow);
				}
				catch(ClienteNoExistenteException exc) { }
			}
		}

		private void btnVolverAMenu_Click(object sender, EventArgs e)
		{
			panelOpcVerClientes.Visible = false;
			panelMenu.Visible = true;
		}

		private void btnVerTodos_Click_1(object sender, EventArgs e)
		{
			panelOpcVerClientes.Visible = false;
			panelVerCliente.Visible = true;

			inicializarVerClientes(negocio.getClientes(),Mes.FEBRERO, 0);
		}

		private void btnSelecMes_Click(object sender, EventArgs e)
		{
			panelOpcVerClientes.Visible = false;
			panelSelecMes.Visible = true;
		}

		private void btnVoverASelecMes_Click(object sender, EventArgs e)
		{
			panelSelecMes.Visible = false;
			panelOpcVerClientes.Visible = true;
		}

		private void btnIr_Click(object sender, EventArgs e)
		{
			if (dudMes.SelectedItem == null || dudAnio.SelectedItem == null)
				return;

			Mes mesAMostrar = (Mes)Enum.Parse(typeof(Mes), dudMes.SelectedItem.ToString().ToUpper());

			int anioAMostrar;
			Int32.TryParse(dudAnio.SelectedItem.ToString(), out anioAMostrar);

			List<Cliente> clientesAMostrar = null;
			try
			{
				clientesAMostrar = negocio.getClientes(mesAMostrar, anioAMostrar);
			}
			catch(Exception exc){}

			inicializarVerClientes(clientesAMostrar, mesAMostrar, anioAMostrar);
			panelSelecMes.Visible = false;
			panelVerCliente.Visible = true;
		}

		private void btnMostrarTodos_Click(object sender, EventArgs e)
		{
			if (seEstanMostrandoTodos())
				inicializarVerClientes(negocio.getClientes(), mesMostrado, anioMostrado);
			else
				try
				{
					inicializarVerClientes(negocio.getClientes(mesMostrado, anioMostrado), mesMostrado, anioMostrado);
				}
				catch(ValorInexistenteException exc) { }
		}

		#endregion

		#region AJUSTES

		private void inicializarAjustes()
		{
			panelMenu.Visible = false;
			panelAjustes.Visible = true;

			lblGuardado.Visible = false;
			chkbNumAuto.Checked = numeroDeClienteAuto;

			btnColorCompleto.BackColor = COLOR_COMPLETADO;
			btnColorEspera.BackColor = COLOR_ESPERA;
			btnColorVencido.BackColor = COLOR_VENCIDO;

			colorDialogCompleto.Color = COLOR_COMPLETADO;
			colorDialogEspera.Color = COLOR_ESPERA;
			colorDialogVencido.Color = COLOR_VENCIDO;
		}

		private void btnVolverAjustes_Click(object sender, EventArgs e)
		{
			panelAjustes.Visible = false;
			panelMenu.Visible = true;

			btnColorCompleto.BackColor = COLOR_COMPLETADO;
			btnColorEspera.BackColor = COLOR_ESPERA;
			btnColorVencido.BackColor = COLOR_VENCIDO;

		}

		private void btnOkAjustes_Click(object sender, EventArgs e)
		{
			COLOR_COMPLETADO = colorDialogCompleto.Color;
			COLOR_ESPERA = colorDialogEspera.Color;
			COLOR_VENCIDO = colorDialogVencido.Color;

			numeroDeClienteAuto = chkbNumAuto.Checked;

			lblGuardado.Visible = true;
		}

		private void btnColorVencido_Click(object sender, EventArgs e)
		{
			colorDialogVencido.ShowDialog();
			btnColorVencido.BackColor = colorDialogVencido.Color;
		}

		private void btnColorEspera_Click(object sender, EventArgs e)
		{
			colorDialogEspera.ShowDialog();
			btnColorEspera.BackColor = colorDialogEspera.Color;
		}

		private void btnColorCompleto_Click(object sender, EventArgs e)
		{
			colorDialogCompleto.ShowDialog();
			btnColorCompleto.BackColor = colorDialogCompleto.Color;
		}

		#endregion

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			panel.Dock = DockStyle.Fill;
		}

		public void saveToXML()
		{
			//archivos principales
			System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Cliente>));

			foreach (int anio in negocio.getAniosConClientes())
			{
				for (Mes mes = Mes.ENERO; (int)mes <= 12; mes = mes.siguiente())
				{
					List<Cliente> clientes = negocio.getClientes(mes, DateTime.Today.Year);

					var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + NOMBRE_ARCHIVO + mes + "_" + anio + ".xml";
					System.IO.FileStream file = System.IO.File.Create(path);

					writer.Serialize(file, clientes);
					file.Close();
				}
			}

			//archivo con anios con datos
			System.Xml.Serialization.XmlSerializer writer2 = new System.Xml.Serialization.XmlSerializer(typeof(List<int>));

			var path2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + "\\Reparaciones_Julio_Anios.xml";
			System.IO.FileStream file2 = System.IO.File.Create(path2);

			writer2.Serialize(file2, negocio.getAniosConClientes());
			file2.Close();

			//archivo de configuracion
			System.Xml.Serialization.XmlSerializer writer3 = new System.Xml.Serialization.XmlSerializer(typeof(Configuracion));

			Configuracion config = new Configuracion();

			config.ColorEspera = COLOR_ESPERA.ToArgb();
			config.ColorVencido = COLOR_VENCIDO.ToArgb();
			config.ColorCompleto = COLOR_COMPLETADO.ToArgb();
			config.NumeroAutomatico = numeroDeClienteAuto;

			var path3 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + "//Configuracion.xml";
			System.IO.FileStream file3 = System.IO.File.Create(path3);

			writer3.Serialize(file3, config);
			file3.Close();
		}

		public void readXML()
		{
			System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Cliente>));
			System.Xml.Serialization.XmlSerializer reader2 = new System.Xml.Serialization.XmlSerializer(typeof(List<int>));
	
			List<int> anios;

			try
			{
				System.IO.StreamReader file2 = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + "\\Reparaciones_Julio_Anios.xml");
				anios = (List<int>)reader2.Deserialize(file2);
				file2.Close();
			}
			catch (FileNotFoundException exc)
			{
				anios = new List<int>() { DateTime.Today.Year };
			}

			foreach (int anio in anios)
			{
				for (Mes mes = Mes.ENERO; (int)mes <= 12; mes = mes.siguiente())
				{
					try
					{
						System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + NOMBRE_ARCHIVO + mes + "_" + anio + ".xml");

						List<Cliente> clientes = (List<Cliente>)reader.Deserialize(file);
						cargarClientes(clientes, mes, anio);

						file.Close();
					}
					catch(Exception exp) { }
					
				}
			}

			//archivo de configuracion
			System.Xml.Serialization.XmlSerializer reader3 = new System.Xml.Serialization.XmlSerializer(typeof(Configuracion));
			try
			{
				System.IO.StreamReader file3 = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + NOMBRE_CARPETA + "//Configuracion.xml");
				Configuracion config = (Configuracion)reader3.Deserialize(file3);
				file3.Close();

				COLOR_COMPLETADO = Color.FromArgb(config.ColorCompleto);
				COLOR_ESPERA = Color.FromArgb(config.ColorEspera);
				COLOR_VENCIDO = Color.FromArgb(config.ColorVencido);
				numeroDeClienteAuto = config.NumeroAutomatico;
			}
			catch (Exception exc)
			{
				COLOR_COMPLETADO = Color.Blue;
				COLOR_ESPERA = Color.Yellow;
				COLOR_VENCIDO = Color.Red;
				numeroDeClienteAuto = false;
			}
		}

		private void cargarClientes(List<Cliente> clientes, Mes mes, int anio)
		{
			foreach(Cliente cliente in clientes)
			{
				negocio.agregarCliente(cliente, mes, anio);
			}
		}

		private void ReparacionesJulio_FormClosing(object sender, FormClosingEventArgs e)
		{
			saveToXML();
		}
	}
}
