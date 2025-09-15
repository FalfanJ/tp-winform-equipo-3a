using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMarcas formMarcas = new FormMarcas();
            formMarcas.ShowDialog();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListaCategorias formCategorias = new FormListaCategorias();
            formCategorias.ShowDialog();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormListaArticulos formArticulos = new FormListaArticulos();
            formArticulos.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

