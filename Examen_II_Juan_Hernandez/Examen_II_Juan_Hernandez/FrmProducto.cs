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
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();
        }

        string operacion = string.Empty;
        ProductoDA productoDA = new ProductoDA();

        //Metodos para los Controles
        private void HabilitarControles()
        {
            CodigoTextBox.Enabled = true;
            DescripcionTextBox.Enabled = true;
            PrecioTextBox.Enabled = true;
            ExistenciaTextBox.Enabled = true;

            GuardarButton.Enabled = true;
            NuevoButton.Enabled = false;
        }

        private void DesabilitarControles()
        {
            CodigoTextBox.Enabled = false;
            DescripcionTextBox.Enabled = false;
            PrecioTextBox.Enabled = false;
            ExistenciaTextBox.Enabled = false;

            GuardarButton.Enabled = false;
            NuevoButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            CodigoTextBox.Clear();
            DescripcionTextBox.Clear();
            PrecioTextBox.Clear();
            ExistenciaTextBox.Clear();
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            operacion = "Nuevo";
            HabilitarControles();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Condiciones por si el usuario deja vacio algun textbox
                if (string.IsNullOrEmpty(CodigoTextBox.Text))
                {
                    errorProvider1.SetError(CodigoTextBox, "Ingrese el Código");
                    CodigoTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(DescripcionTextBox.Text))
                {
                    errorProvider1.SetError(DescripcionTextBox, "Ingrese una Descripción");
                    DescripcionTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(PrecioTextBox.Text))
                {
                    errorProvider1.SetError(PrecioTextBox, "Ingrese un Precio");
                    PrecioTextBox.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ExistenciaTextBox.Text))
                {
                    errorProvider1.SetError(ExistenciaTextBox, "Ingrese una Existencia");
                    ExistenciaTextBox.Focus();
                    return;
                }

                //Aqui se pasan los datos de textbox a la base de datos
                Producto producto = new Producto();
                producto.Codigo = CodigoTextBox.Text;
                producto.Descripcion = DescripcionTextBox.Text;
                producto.Precio = Convert.ToDecimal(PrecioTextBox.Text);
                producto.Existencia = Convert.ToInt32(ExistenciaTextBox.Text);

                if (operacion == "Nuevo")
                {
                    bool inserto = productoDA.InsertarProducto(producto);

                    if (inserto)
                    {
                        DesabilitarControles();
                        LimpiarControles();
                        ListarProductos();
                        MessageBox.Show("Producto Insertado");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            ListarProductos();
        }

        //Metodo para mostrar la lista en pantalla
        private void ListarProductos()
        {
            ProductosDataGridView.DataSource = productoDA.ListarProductos();
        }

        //Este metodo lo que hace es detectar que tecla se preciona al escribir sobre el
        private void PrecioTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Condición para que no se pueda escribir caracteres ni mas de dos puntos en el textbox
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                //Esto lo que hace es que evitaria que se ingrese el caracter
                e.Handled = true;
            }
            
            //El IndexOf lo que hace es que cuenta la cantidad en que se ingreso un caracter
            //Esta condición es para que no se ingrese mas de un punto en el textbox
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        //Este metodo lo que hace es detectar que tecla se preciona al escribir sobre el
        private void ExistenciaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Condición para que no se pueda escribir caracteres ni mas de dos puntos en el textbox
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                //Esto lo que hace es que evitaria que se ingrese el caracter
                e.Handled = true;
            }
        }
    }
}
