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
    public partial class FrmListaArticulos : Form
    {
        private List<Articulos> listaArticulo;
        private List<Imagenes> listaImagenes;
        public FrmListaArticulos()
        {
            InitializeComponent();
        }

        private void FormListaArticulos_Load(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            ImagenesNegocio negocioImag = new ImagenesNegocio();
            listaArticulo = negocio.Listar();
            listaImagenes = negocioImag.ListarXArticulo();
            DgwArticulos.DataSource = listaArticulo;
            pbxArticulo.Load(listaImagenes[0].ImagenURL);
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
    }
}
