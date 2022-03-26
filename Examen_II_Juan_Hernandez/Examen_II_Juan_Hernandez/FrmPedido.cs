using Datos.Accesos;
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

        ProductoDA productoDA = new ProductoDA();
        PedidoDA pedidoDA = new PedidoDA();
        Pedido pedido = new Pedido();
        Producto producto;

        //Lista para almacenar los detalles del pedido
        List<DetallePedido> detallePedidoLista = new List<DetallePedido>();

        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalAPagar = 0;

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            DetalleDataGridView.DataSource = detallePedidoLista;
        }

        private void CodigoProductoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                producto = new Producto();
                producto = productoDA.GetProductoPorCodigo(CodigoProductoTextBox.Text);
                DescripcionProductoTextBox.Text = producto.Descripcion;
                CantidadTextBox.Focus();
            }
            else
            {
                producto = null;
                DescripcionProductoTextBox.Clear();
                CantidadTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.CodigoProducto = producto.Codigo;
                detallePedido.Descripcion = producto.Descripcion;
                detallePedido.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detallePedido.Precio = producto.Precio;
                detallePedido.Total = producto.Precio * Convert.ToInt32(CantidadTextBox.Text);

                subTotal += detallePedido.Total;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv;

                SubTotalTextBox.Text = subTotal.ToString();
                ISVTextBox.Text = isv.ToString();
                TotalTextBox.Text = totalAPagar.ToString();

                detallePedidoLista.Add(detallePedido);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = detallePedidoLista;
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            pedido.IdentidadCliente = IdentidadMaskedTextBox.Text;
            pedido.Cliente = NombreTextBox.Text;
            pedido.Fecha = FechaDateTimePicker.Value;
            pedido.SubTotal = Convert.ToDecimal(SubTotalTextBox.Text);
            pedido.ISV = Convert.ToDecimal(ISVTextBox.Text);
            pedido.Total = Convert.ToDecimal(TotalTextBox.Text);

            int idPedido = 0;

            idPedido = pedidoDA.InsertarPedido(pedido);

            if (idPedido != 0)
            {
                foreach (var item in detallePedidoLista)
                {
                    item.IdPedido = idPedido;
                    pedidoDA.InsertarDetalle(item);
                }
            }
        }
    }
}
