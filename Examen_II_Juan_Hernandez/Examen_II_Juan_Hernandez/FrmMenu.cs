using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Examen_II_Juan_Hernandez
{
    public partial class FrmMenu : Syncfusion.Windows.Forms.Office2010Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        FrmProducto frmProducto = null;

        private void ProductosToolStripButton_Click(object sender, EventArgs e)
        {
            if (frmProducto == null)
            {
                frmProducto = new FrmProducto();
                frmProducto.MdiParent = this;
                frmProducto.FormClosed += FrmProducto_FormClosed;
                frmProducto.Show();
            }
            else
            {
                frmProducto.Activate();
            }
        }

        private void FrmProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmProducto = null;
        }

        //---------------------------------------------------------------
        private void PedidosToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}
