using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_II_Juan_Hernandez
{
    public partial class FrmPedido : Form
    {
        public FrmPedido()
        {
            InitializeComponent();
        }

        Pedido pedido = new Pedido();
        Producto Producto;

        //Lista para almacenar los detalles del pedido
        List<DetallePedido> detallePedidosLista = new List<DetallePedido>();

        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalAPagar = 0;

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            DetalleDataGridView.DataSource = detallePedidosLista;
        }
    }
}
