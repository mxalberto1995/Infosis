using ProyectoVenta.Logica;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Formularios
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/RK1tRH");
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/VRgLgZ");
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            txtusuario.Text = "";
            txtclave.Text = "";
            this.Show();
            txtusuario.Focus();
        }

        public static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    //mbInfo = String.Format("{0,-25}{1}", propData.Name, Convert.ToString(propData.Value));
                    mbInfo = Convert.ToString(propData.Value);
            }

            return mbInfo;
        }

        public static Boolean DemoSistema()
        {
            Boolean Vencio = false;
            //Año - Mes - Dia
            int año = 2022;
            int mes = 06;
            int dia = 08;
            DateTime date_1 = new DateTime(año, mes, dia);
            DateTime date_2 = DateTime.Now.Date;

            //Valores negativos es que aun falta del período de prueba, valores positivos indican que ya acabo
            TimeSpan Diff_dates = date_2.Subtract(date_1);
            //Si Days es mayor que 0, el periodo de prueba acabo.
            int diasrest = 0;
            if (int.Parse(Diff_dates.Days.ToString()) > 0)
            {
                diasrest = Diff_dates.Days;
                MessageBox.Show("Obtenega su licencia, escribeme a mi correo: maxwellchaconnica@gmail.com, el demo a expírado.");
                Vencio = true;
            }
            if (int.Parse(Diff_dates.Days.ToString()) < 0)
            {
                diasrest = Diff_dates.Days * -1;
                MessageBox.Show("Período de evaluación activo, quedan: " + diasrest + " restantes.");
                Vencio = false;
            }

            return Vencio;

        }

        private void btningresar_Click(object sender, EventArgs e)
        {

            Boolean HabilitarDemo = false;
            Boolean DemoExpiro;
            if (HabilitarDemo == true)
            {
                DemoExpiro = DemoSistema();
                if (DemoExpiro == true)
                {
                    //Error
                    MessageBox.Show("Error de Demo Expirado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (DemoExpiro == false)
                {

                    //Iniciar Sesión
                    string mensaje = string.Empty;
                    bool encontrado = false;

                    if (txtusuario.Text == "administrador" && txtclave.Text == "13579123")
                    {
                        int respuesta = UsuarioLogica.Instancia.resetear();
                        if (respuesta > 0)
                        {
                            MessageBox.Show("La cuenta fue restablecida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {

                        List<Usuario> ouser = UsuarioLogica.Instancia.Listar(out mensaje);
                        encontrado = ouser.Any(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text);

                        if (encontrado)
                        {
                            Usuario objuser = ouser.Where(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text).FirstOrDefault();

                            Inicio frm = new Inicio();
                            frm.NombreUsuario = objuser.NombreUsuario;
                            frm.Clave = objuser.Clave;
                            frm.NombreCompleto = objuser.NombreCompleto;
                            frm.FechaHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                            frm.oPermisos = PermisosLogica.Instancia.Obtener(objuser.IdPermisos);
                            frm.Show();
                            this.Hide();
                            frm.FormClosing += Frm_Closing;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(mensaje))
                            {
                                MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                        }
                    }
                }
            }
            else if (HabilitarDemo == false) //No es un demo, va con licencia, se ejecuta este bloque.
            {
                
                String IDCPU = Conexion.miEquipo; 
                if (true) //Habilitar para desarrollo 
                //if (String.Equals(IDCPU, GetMotherBoardID())) //Habilitar para hacer instalador y desactivar la linea anterior.
                {

                    //Iniciar Sesión
                    string mensaje = string.Empty;
                    bool encontrado = false;

                    if (txtusuario.Text == "administrador" && txtclave.Text == "13579123")
                    {
                        int respuesta = UsuarioLogica.Instancia.resetear();
                        if (respuesta > 0)
                        {
                            MessageBox.Show("La cuenta fue restablecida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {

                        List<Usuario> ouser = UsuarioLogica.Instancia.Listar(out mensaje);
                        encontrado = ouser.Any(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text);

                        if (encontrado)
                        {
                            Usuario objuser = ouser.Where(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text).FirstOrDefault();

                            Inicio frm = new Inicio();
                            frm.NombreUsuario = objuser.NombreUsuario;
                            frm.Clave = objuser.Clave;
                            frm.NombreCompleto = objuser.NombreCompleto;
                            frm.FechaHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                            frm.oPermisos = PermisosLogica.Instancia.Obtener(objuser.IdPermisos);
                            frm.Show();
                            this.Hide();
                            frm.FormClosing += Frm_Closing;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(mensaje))
                            {
                                MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                        }
                    }
                }
                else
                {
                    //Funciona el programa
                    MessageBox.Show("La Licencia comprada solo aplica para el equipo con ID:" + IDCPU + "No para el Equipo con ID:" + GetMotherBoardID() + ", debe adquirir una licencia para este PC valida. Contáctame a mi correo: maxwellchaconnica@gmail.com para adquirir una nueva licencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


            //

            
        }

        private void iconPictureBox1_MouseHover(object sender, EventArgs e)
        {
            //iconPictureBox1.BackColor = Color.DarkCyan;
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //iconPictureBox1.BackColor = Color.Teal;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
