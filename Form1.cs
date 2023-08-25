using Data;
using Logica;
using Logica.Libreria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Estudiante


{
    
    public partial class Form1 : Form
    {
        /*
        Este código se encuentra en el constructor de una clase llamada Form1 y crea instancias de varios
        objetos, inicializa componentes visuales del formulario y crea una instancia de Estudiante2 con 
        ciertos argumentos. La clase Estudiante2 podría tener funcionalidades relacionadas con
        la interacción y manipulación de los controles y objetos proporcionados como argumentos.
         */
        Estudiante0 E  = new Estudiante0();
        Estudiante1  E1 = new Estudiante1();
        Estudiante2  E2 = new Estudiante2();
 
        public Form1()
        {
            InitializeComponent();
            var ListTexBox = new  List<TextBox>();
            ListTexBox.Add(textBoxID);
            ListTexBox.Add(textBoxNombre);
            ListTexBox.Add(textBoxApellido);
            ListTexBox.Add(textBoxEmail);
           
            var Listlabel = new List<Label>();
            Listlabel.Add(labelID);
            Listlabel.Add(labelNombre);
            Listlabel.Add(labelApellido);
            Listlabel.Add(labelEmail);
            Listlabel.Add(labelPagina);

            Object[] objects = { pictureBox1, Properties.Resources.tecnologia_verde, dataGridView1, numericUpDown1 };


            E2 = new Estudiante2(ListTexBox, Listlabel,objects);

        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxID.Text == "")
            {
                labelID.ForeColor = Color.Black;
            }

            else
            {
                labelID.ForeColor = Color.Blue;
                labelID.Text = "ID SECCION";

            }
        }

        private void TextBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            E.numKeyPress(e);
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNombre.Text == "")
            {
                labelNombre.ForeColor = Color.Black;
                
            }

            else
            {
                labelNombre.ForeColor = Color.Blue;
                labelNombre.Text = "NOMBRE";


            }
        }

        private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            E.TextKeyPress(e);  
        }

        private void textBoxApellido_TextChanged(object sender, EventArgs e)
        {
            if (textBoxApellido.Text == "")
            {
                labelApellido.ForeColor = Color.Black;
            }

            else
            {
                labelApellido.ForeColor = Color.Blue;
                labelApellido.Text = "APELLIDO";



            }

        }

        private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            E.TextKeyPress(e);

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "")
            {
                labelEmail.ForeColor = Color.Black;
            }

            else
            {
                labelEmail.ForeColor = Color.Blue;
                labelEmail.Text = "EMAIL";

            }
        }

        private void textBoxEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            E.emailKeyPress(e);
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            
            E2.Registrar();
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            E2.BuscarEstudiante(textBox1.Text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            E2.Registro_Pagina();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                E2.GetEstudiante();
            }


        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                E2.GetEstudiante();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            E2.Paginador("Primero");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            E2.Paginador("Anterior");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            E2.Paginador("Siguiente");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            E2.Paginador("Ultimo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            E2.Restablecer();
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            E2.Paginador("Primero");
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            E2.Paginador("Anterior");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            E2.Paginador("Siguiente");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            E2.Paginador("Ultimo");
        }

        private void buttonCargarImage2_Click(object sender, EventArgs e)
        {
            E1.CargarImageen(pictureBox1);
        }

        private void labelNombre_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de cancelar la operación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Operacion Cancelada");
                E2.Restablecer();
            }
        }

     
          
        private void button3_Click_1(object sender, EventArgs e)
        {
            E2.Eliminar();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro de que quieres cerrar el programa?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true; // Cancelar el cierre del formulario
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
