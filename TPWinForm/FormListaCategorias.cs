using Dominio;
using Negocio;
using System;
using System.Windows.Forms;

namespace TPWinForm
{
    public partial class FormListaCategorias : Form
    {
        private CategoriasNegocio negocio = new CategoriasNegocio();

        public FormListaCategorias()
        {
            InitializeComponent();
        }

        private void FormListaCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            dgvCategorias.DataSource = negocio.listar();
        }

        // ---- Evento para agregar categoria
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtAgregar.Text.Trim();
            // ---- Validacion por si el campo esta vacio

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese el nombre de la nueva categoría.El campo no puede estar vacio.");
                return;
            }

            Categorias nueva = new Categorias { Descripcion = nombre };
            negocio.agregar(nueva);
            txtAgregar.Clear();
            CargarCategorias();
        }

        // ---- Evento para editar categoria
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null) return;

            int id = (int)dgvCategorias.CurrentRow.Cells["Id"].Value;

            string nuevoNombre = txtEditar.Text.Trim();

            // ---- Validacion por si el campo esta vacio
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("El campo no puede estar vacio.");
                return;
            }

            Categorias cat = new Categorias { Id = id, Descripcion = nuevoNombre };
            negocio.editar(cat);
            CargarCategorias();
        }

        // ---- Evento eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow == null) return;

            int id = (int)dgvCategorias.CurrentRow.Cells["Id"].Value;

            // ---- Mensaje de confirmacion

            DialogResult res = MessageBox.Show(
                "¿Está seguro de eliminar la categoria?",
                "Eliminar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            // ---- Si confirma, eliminamosa
            if (res == DialogResult.Yes)
            {
                negocio.eliminar(id);
                CargarCategorias();
            }
        }

        // ---- Evento buscar por categoria (descripcion)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrWhiteSpace(filtro))
                CargarCategorias();
            else
                dgvCategorias.DataSource = negocio.buscar(filtro);
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow != null)
            {
                txtEditar.Text = dgvCategorias.CurrentRow.Cells["Descripcion"].Value.ToString();
            }
        }

        // ---- Evento resetear busqueda
 
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarCategorias();
        }
    }
}
