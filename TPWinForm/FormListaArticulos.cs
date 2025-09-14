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
        public FormListaArticulos()
        {
            InitializeComponent();
        }

        private void FormListaArticulos_Load(object sender, EventArgs e)
        {
            ArticulosNegocio negocio = new ArticulosNegocio();
            DgwArticulos.DataSource = negocio.Listar();
        }
    }
}
