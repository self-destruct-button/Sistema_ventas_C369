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
using System.IO;
using System.Text.RegularExpressions;

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
        //Validar el correo electrónico con regex
        public bool validar_Mail(string sMail)
        {
            string regMail = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
            return Regex.IsMatch(sMail, regMail);
        }
        //Método que guarda e inserta la creación de un nuevo usuario
        //Este es el boton btnGuardar, no sé porque hijueputas no funciona el nombre >:(
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!validar_Mail(txtCorreo.Text))
            {
                MessageBox.Show("Dirección de correo no válida, revise esa vaina", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                txtCorreo.SelectAll();
            }
            else
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
                        cmd.Parameters.AddWithValue("@Estado", "Activo");

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
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        //Método para listar los usuarios en una grilla
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
        //Guardar los cambios al editar un usuario seleccionado
        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = CONEXION.ConexionMaestra.conexion;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("Update_usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", lblId_Usuario.Text);
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void lblAnuncioIcono_Click(object sender, EventArgs e)
        {
            PanelIcono.Visible = true;
        }

        private void Icono_Click(object sender, EventArgs e)
        {
            PanelIcono.Visible = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        //---Inicio Iconos predeterminados
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
        //----Fin Iconos predeterminados

        //Cuando recién carga el programa lo que se va a mostrar
        private void usuariosok_Load(object sender, EventArgs e)
        {
            //el panel4 es el de la sombra, el panel5 es el de la información
            panel4.Visible = false;
            PanelIcono.Visible = false;
            mostrar();
        }
        //Al darle al botón de nuevo usuario se limpien los textbox y oculte el botón de guardar cambios
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            lblAnuncioIcono.Visible = true;
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtPassword.Text = "";            
            txtUsuario.Text = "";
            btnGuardarCambios.Visible = false;
            btnGuardar2.Visible = true;
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Cargar_Estado_iconos()
        {
            try
            {
                foreach(DataGridViewRow row in dataListado.Rows)
                {
                    try
                    {
                        string Icono = Convert.ToString(row.Cells["NombreIcono"].Value);

                        switch (Icono)
                        {
                            case "1":

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Obtener la información del usuario al seleccionarlo de la grilla con doble clic
        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblId_Usuario.Text = dataListado.SelectedCells[1].Value.ToString();
            txtNombre.Text = dataListado.SelectedCells[2].Value.ToString();
            txtUsuario.Text = dataListado.SelectedCells[3].Value.ToString();
            txtPassword.Text = dataListado.SelectedCells[4].Value.ToString();

            Icono.BackgroundImage = null;
            byte[] b = (Byte[])dataListado.SelectedCells[5].Value;
            MemoryStream ms = new MemoryStream(b);
            Icono.Image = Image.FromStream(ms);
            //Icono.SizeMode = PictureBoxSizeMode.Zoom;
            lblAnuncioIcono.Visible = false;

            lblnumeroIcono.Text = dataListado.SelectedCells[6].Value.ToString();
            txtCorreo.Text = dataListado.SelectedCells[7].Value.ToString();
            txtRol.Text = dataListado.SelectedCells[8].Value.ToString();
            panel4.Visible = true;
            btnGuardar2.Visible = false;
            btnGuardarCambios.Visible = true;
        }

        //Eliminar de manera lógica un usuario del listado
        private void dataListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataListado.Columns["Eli"].Index)
            {
                DialogResult result;
                result = MessageBox.Show("¿Desea eliminar el usuario?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if(result == DialogResult.OK)
                {
                    SqlCommand cmd;

                    try
                    {
                        foreach(DataGridViewRow row in dataListado.SelectedRows)
                        {
                            int onekey = Convert.ToInt32(row.Cells["IdUsuario"].Value);
                            string usuario = Convert.ToString(row.Cells["Login"].Value);

                            try
                            {
                                SqlConnection con = new SqlConnection();
                                con.ConnectionString = CONEXION.ConexionMaestra.conexion;
                                con.Open();
                                cmd = new SqlCommand("Eliminar_usuario", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@IdUsuario", onekey);
                                cmd.Parameters.AddWithValue("@Login", usuario);
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
                mostrar();
            }

        }
    }
}
