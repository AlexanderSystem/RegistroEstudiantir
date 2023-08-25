using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Libreria
{
    public class TextBoxEvent
    {


        /*
          Este código controla la entrada de texto en un campo específico, permitiendo letras, teclas de
          control (como Backspace) y separadores (como el espacio), mientras evita saltos de línea y otros  
          caracteres no deseados en el campo.
        */
        public void TextKeyPress(KeyPressEventArgs e)
        {  // Condiccion que solo permite ingresar datos de tipo texto
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            // Condicion Que no permite dar salto de linea Cuando le presione Enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }

            // condicion Que Nos Permite utlizar la tecla backspce

            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            //Condicion que nos permite utilizar la tecla espacio

            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }


        /*
           Este manejador de eventos numKeyPress controla la entrada de teclado en un campo de texto,
           permitiendo solo dígitos y teclas de control mientras evita otros caracteres como letras,
           saltos de línea y espacios.
        */
        public void numKeyPress(KeyPressEventArgs e)
        {
            // Condiccion que solo permite ingresar datos de tipo texto
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // Condicion Que no permite dar salto de linea Cuando le presione Enter
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }

            // condicion Que Nos Permite utlizar la tecla backspce

            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            //Condicion que nos permite utilizar la tecla espacio

            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            // condicion que nos permite no admitir  datos de tipo text
            else if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }

            else
            {
                e.Handled = true;
            }
        }


        /*
          Este manejador de eventos emailKeyPress se utiliza para controlar la entrada de teclado en un
          contexto relacionado con la entrada de correo electrónico. Se asegura de que no se puedan 
          ingresar espacios (separadores) en el campo de entrada y permite que otros caracteres se muestren.
        */
        public void emailKeyPress(KeyPressEventArgs e)
        {
             //Condicion que nos permite utilizar la tecla espacio

            if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
            else { e.Handled = false; } 
        }


        /*
         este método VerificarFormatoDeEmail utiliza una expresión regular para verificar si una 
         dirección de correo electrónico tiene el formato válido. Devuelve true si el formato es válido
        y false si no lo es. Esto puede ser útil para validar direcciones de correo electrónico ingresadas
        por los usuarios en una aplicación, asegurándose de que cumplan con ciertos criterios de formato.
        */
        public bool VerificarFormatoDeEmail(string email)
        {
            // Expresión regular para validar el formato de una dirección de correo electrónico
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (Regex.IsMatch(email, pattern))
            {
                return true; // Formato de correo electrónico válido
            }
            else
            {
                return false; ; // Formato de correo electrónico inválido
            }
        }






    }

}


