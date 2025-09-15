using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace TPWinForm
{
    public partial class FormMarcas : Form
    {
        private MarcasNegocio negocio;

        public FormMarcas()
        {
            InitializeComponent();
            negocio = new MarcasNegocio();
        }

        private void FormMarcas_Load(object sender, EventArgs e)
        {
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            dataGridViewMarcas.DataSource = null;
            dataGridViewMarcas.DataSource = negocio.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim();
            dataGridViewMarcas.DataSource = negocio.Buscar(criterio);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
            {
                MessageBox.Show("Ingrese un nombre de marca.");
                return;
            }

            var nueva = new Marcas { Marca = txtNuevaMarca.Text.Trim() };
            negocio.Agregar(nueva);
            CargarMarcas();
            txtNuevaMarca.Clear();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridViewMarcas.CurrentRow != null)
            {
                var seleccionada = (Marcas)dataGridViewMarcas.CurrentRow.DataBoundItem;
                if (string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
                {
                    MessageBox.Show("Ingrese el nuevo nombre para modificar la marca.");
                    return;
                }
                seleccionada.Marca = txtNuevaMarca.Text.Trim();
                negocio.Modificar(seleccionada);
                CargarMarcas();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewMarcas.CurrentRow != null)
            {
                var seleccionada = (Marcas)dataGridViewMarcas.CurrentRow.DataBoundItem;
                var confirm = MessageBox.Show($"¿Está seguro de eliminar la marca {seleccionada.Marca}?",
                                              "Confirmar eliminación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    negocio.Eliminar(seleccionada.Id);
                    CargarMarcas();
                }
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dataGridViewMarcas.CurrentRow != null)
            {
                var seleccionada = (Marcas)dataGridViewMarcas.CurrentRow.DataBoundItem;
                var detalle = negocio.ObtenerPorId(seleccionada.Id);
                MessageBox.Show($"ID: {detalle.Id}\nMarca: {detalle.Marca}", "Detalle de Marca");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMarcas();
        }
    }
}