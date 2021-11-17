using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_de_venta_codigo369_CHARP
{
    public partial class usuariosok : Form
    {
        public usuariosok()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Este es el boton btnGuardar, no sé porque hijueputas no funciona el nombre >:(
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = CONEXION.ConexionMaestra.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("Insertar_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombres", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Login", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Rol", txtRol.Text);

                    //Tratar los archivos de imágenes
                    //Crea un objeto de tipo binario
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //Rescatar y guardar la imagen en un formato que SQL server comprenda o eso creo jaja
                    Icono.Image.Save(ms, Icono.Image.RawFormat);

                    cmd.Parameters.AddWithValue("@Icono", ms.GetBuffer());
                    cmd.Parameters.AddWithValue("@NombreIcono", lblnumeroIcono.Text);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    mostrar();
                    panel4.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "aqui esta el error");
                }
            }
        }

        private void mostrar()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = CONEXION.ConexionMaestra.conexion;
                con.Open();
                da = new SqlDataAdapter("Mostrar_usuario", con);
                da.Fill(dt);
                dataListado.DataSource = dt;
                con.Close();
                dataListado.Columns[1].Visible = false;
                dataListado.Columns[5].Visible = false;
                dataListado.Columns[6].Visible = false;
                dataListado.Columns[7].Visible = false;
                dataListado.Columns[8].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {

        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            PanelIcono.Visible = true;
        }

        private void Icono_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox3.Image;
            lblnumeroIcono.Text = "1";
            lblAnuncioIcono.Visible = false;
            PanelIcono.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox4.Image;
            lblnumeroIcono.Text = "2";
            lblAnuncioIcono.Visible = false;
            PanelIcono.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox5.Image;
            lblnumeroIcono.Text = "3";
            lblAnuncioIcono.Visible = false;
            PanelIcono.Visible = false;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox6.Image;
            lblnumeroIcono.Text = "4";
            lblAnuncioIcono.Visible = false;
            PanelIcono.Visible = false;
        }

        private void usuariosok_Load(object sender, EventArgs e)
        {
            //el panel4 es el de la sombra, el panel5 es el de la información
            panel4.Visible = false;
            PanelIcono.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
