using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm
{
    public partial class FormAltaArticulo : Form
    {
        public FormAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulos nuevo = new Articulos();
            ArticulosNegocio negocio = new ArticulosNegocio();
            Imagenes imagenNuevo = new Imagenes();
            ImagenesNegocio negocioImagen = new ImagenesNegocio();

            try
            {
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Categoria = (Categorias)cboCategoria.SelectedItem;
                nuevo.Marca = (Marcas)cboMarca.SelectedItem;

                negocio.Agregar(nuevo);
                if (!(string.IsNullOrEmpty(txtUrlImagen.Text)))
                {
                    imagenNuevo.ImagenURL = txtUrlImagen.Text;
                    imagenNuevo.IdArticulo = negocio.IdArticulo(nuevo.Codigo);
                    negocioImagen.Agregar(imagenNuevo);

                }
                MessageBox.Show("Articulo agregado");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void FormAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriasNegocio catNegocio = new CategoriasNegocio();
            MarcasNegocio marNegocio = new MarcasNegocio();

            try
            {
                cboCategoria.DataSource = catNegocio.listar();
                cboMarca.DataSource = marNegocio.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            try
            {
                pbxArticuloAlta.Load(txtUrlImagen.Text.ToString());
            }
            catch (Exception)
            {

                pbxArticuloAlta.Image = Properties.Resources.Image_not_found;
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
