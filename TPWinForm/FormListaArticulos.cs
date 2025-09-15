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
    public partial class FormListaArticulos : Form
    {
        private List<Articulos> listaArticulo;
        private List<Imagenes> listaImagenes;
        public FormListaArticulos()
        {
            InitializeComponent();
        }

        private void FormListaArticulos_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            ImagenesNegocio negocioImag = new ImagenesNegocio();
            try
            {
                listaArticulo = negocio.Listar();
                listaImagenes = negocioImag.ListarXArticulo();
                DgwArticulos.DataSource = listaArticulo;
                DgwArticulos.Columns["Id"].Visible = false;
                pbxArticulo.Load(listaImagenes[0].ImagenURL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }

        private void DgwArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulos selecionado = (Articulos) DgwArticulos.CurrentRow.DataBoundItem;
            int idSelecionado = selecionado.Id;
            Imagenes aux = new Imagenes();

            foreach (Imagenes item in listaImagenes)
            {
                if (item.IdArticulo == idSelecionado)
                {
                    aux.ImagenURL = item.ImagenURL;
                }
            }
            try
            {
                pbxArticulo.Load(aux.ImagenURL);
            }
            catch (Exception)
            {
                pbxArticulo.Image = Properties.Resources.Image_not_found;
             
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAltaArticulo alta = new FormAltaArticulo();
            alta.ShowDialog();
            Cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            Articulos articuloSelect;
            Imagenes imagenSelect = new Imagenes();
            ImagenesNegocio imgNegocio = new ImagenesNegocio();

            articuloSelect = (Articulos)DgwArticulos.CurrentRow.DataBoundItem;
            imagenSelect = imgNegocio.imagen(articuloSelect.Id);
            FormModificacionArticulo mod = new FormModificacionArticulo(articuloSelect, imagenSelect);
            mod.ShowDialog();
            Cargar();
        }
    }
}
