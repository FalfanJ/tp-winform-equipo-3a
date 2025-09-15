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
    public partial class FormModificacionArticulo : Form
    {
        private Articulos articulos = null;
        private Imagenes imagenes = null;
        public FormModificacionArticulo()
        {
            InitializeComponent();
        }

        public FormModificacionArticulo(Articulos articulo, Imagenes imagenes)
        {
            InitializeComponent();
            this.articulos = articulo;
            this.imagenes = imagenes;
        }

        private void FormModificacionArticulo_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void cargar()
        {
            CategoriasNegocio catNegocio = new CategoriasNegocio();
            MarcasNegocio marNegocio = new MarcasNegocio();

            try
            {
                cboCategoria.DataSource = catNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marNegocio.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Marca";

                txtCodigo.Text = articulos.Codigo;
                txtNombre.Text = articulos.Nombre;
                txtDescripcion.Text = articulos.Descripcion;
                txtPrecio.Text = articulos.Precio.ToString();
                txtUrlImagen.Text = imagenes.ImagenURL;
                cargarImagen(txtUrlImagen.Text);

                if(articulos.Categoria != null)
                {
                    cboCategoria.SelectedValue = articulos.Categoria.Id;

                }
                if(articulos.Marca != null)
                {
                    cboMarca.SelectedValue = articulos.Marca.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string url)
        {
            try
            {
                pbxArticuloModificaion.Load(url);
            }
            catch (Exception)
            {

                pbxArticuloModificaion.Image = Properties.Resources.Image_not_found;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulos modArticulo = new Articulos();
            Imagenes modImagen = new Imagenes();
            ArticulosNegocio negArticulo = new ArticulosNegocio();
            ImagenesNegocio negImg = new ImagenesNegocio();

            try
            {
                modArticulo.Id = articulos.Id;
                modArticulo.Codigo = txtCodigo.Text;
                modArticulo.Nombre = txtNombre.Text;
                modArticulo.Descripcion = txtDescripcion.Text;
                modArticulo.Precio = decimal.Parse(txtPrecio.Text);
                modArticulo.Categoria = (Categorias)cboCategoria.SelectedItem;
                modArticulo.Marca = (Marcas)cboMarca.SelectedItem;

                negArticulo.Modificar(modArticulo);
                if (!(string.IsNullOrEmpty(txtUrlImagen.Text)))
                {
                    modImagen.ImagenURL = txtUrlImagen.Text;
                    modImagen.IdArticulo = negArticulo.IdArticulo(modArticulo.Codigo);
                    negImg.Modificar(modImagen);

                }
                MessageBox.Show("Articulo modificado");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
