using Data;
using LinqToDB;
using Logica.Libreria;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Logica
{
    // Esta clase esta Heredando de la clase  TexBoxEvent
    public class Estudiante0 : TextBoxEvent
    {

    }

    // Esta clase esta Heredando de la clase  updateimag
    public class Estudiante1 : updateimag
    {
       
    }

    public class Estudiante2 
    { 

        // Aqui se estan definiendo  los campos de clase con los que se van a Trabajar

        private Estudiante0 estudiante = new Estudiante0();
        private Estudiante1 estudiante1 = new Estudiante1();
        private List<TextBox> listTexbox;
        private List<Label> listlabel;
        private PictureBox Imagen;
        private Bitmap Imagenbitmap;
        private DataGridView dataGridView;
        private NumericUpDown numericUpDown;
        private string accion = "insert";
        private int _run_pagina =2 ;
        private int _re_por_pagina=10;
        private int idEstudiante = 0;
        private Paginador<Estudiante> _paginador;
        private List<Estudiante> listEstudiante;

        //Este es El metodo Constructo por defecto de esta clase
        public Estudiante2()
        {

        }
        // Este es el segundo metodo constructor  que se va a utilizar en la clase,
        // usaremos sobre carhga de constructore
        public Estudiante2(List<TextBox> listTexbox, List<Label> listlabel, Object[] objects)
        {
            this.listTexbox = listTexbox;
            this.listlabel = listlabel;
            Imagen = (PictureBox)objects[0];
            Imagenbitmap = (Bitmap)objects[1];
            dataGridView = (DataGridView)objects[2];
            numericUpDown = (NumericUpDown)objects[3];
            Restablecer();



        }

        /*   
           Este método Registrar() realiza una serie de validaciones para asegurarse de que los campos requeridos
           estén llenos y que el correo electrónico tenga un formato válido antes de intentar
           registrar la información de un estudiante en algún sistema.
        */

        public void Registrar()
        {
            if (listTexbox[0].Text.Equals(""))
            {
                listlabel[0].Text = "El campo ID es requerido";
                listlabel[0].ForeColor = Color.Red;
                listlabel[0].Focus();

            }

            else
            {
                if (listTexbox[1].Text.Equals(""))
                {
                    listlabel[1].Text = "El campo Nombre es requerido";
                    listlabel[1].ForeColor = Color.Red;
                    listlabel[1].Focus();
                }
                else

                {
                    if (listTexbox[2].Text.Equals(""))
                    {
                        listlabel[2].Text = "El campo Apellido es requerido";
                        listlabel[2].ForeColor = Color.Red;
                        listlabel[2].Focus();
                    }

                    else
                    {
                        if (listTexbox[3].Text.Equals(""))
                        {
                            listlabel[3].Text = "El campo Email es requerido";
                            listlabel[3].ForeColor = Color.Red;
                            listlabel[3].Focus();
                        }
                        else
                        {
                            if (estudiante.VerificarFormatoDeEmail(listTexbox[3].Text))
                            {
                                var db = new Connection();
                                var use = db._Estudiante.Where(u => u.Email.Equals(listTexbox[3].Text)).ToList();
                                if(use.Count.Equals(0)) 
                                {
                                  Guardar(); 
                                }
                                else
                                {
                                    if (use[0].ID.Equals(idEstudiante))
                                    {
                                        Guardar();
                                    }
                                    else
                                    {
                                        listlabel[3].Text = "EL Email ya esta Registrado";
                                        listlabel[3].ForeColor = Color.Green;
                                        listlabel[3].Focus();
                                    }

                                }

                            }
                            else
                            {
                                listlabel[3].Text = "El Email no es Valido";
                                listlabel[3].ForeColor = Color.Red;
                                listlabel[3].Focus();
                            }

                        }    
                           
                     }

                }
            } 

        }

        /*
         el método Guardar() se encarga de manejar la lógica para insertar o actualizar información 
         de estudiantes en una base de datos utilizando una transacción para garantizar la integridad 
         de los datos. También maneja la conversión de la imagen del estudiante a bytes y la limpieza 
         del formulario después de una operación exitosa.
         */
        private void Guardar()
        {
            var ArrayImage = estudiante1.ImagetoByte(Imagen.Image);
            var db = new Connection();

            using (var transaction = db.BeginTransaction())
                try
                {
                    switch (accion)
                    {
                        case "insert":
                        db._Estudiante.Value(e => e.Seccion, listTexbox[0].Text)
                       .Value(e => e.Nombre, listTexbox[1].Text)
                       .Value(e => e.Apellido, listTexbox[2].Text)
                       .Value(e => e.Email, listTexbox[3].Text)
                       .Value(e => e.Imagen, ArrayImage)
                       .Insert();
                            break;

                        case "update":

                            db._Estudiante.Where(u => u.ID.Equals(idEstudiante))
                                .Set(e => e.Seccion, listTexbox[0].Text)
                                .Set(e => e.Nombre, listTexbox[1].Text)
                                .Set(e => e.Apellido, listTexbox[2].Text)
                                .Set(e => e.Email, listTexbox[3].Text)
                                .Set(e => e.Imagen, ArrayImage)
                                .Update();

                            break;
                    }

                    
                    // Si todo está bien, confirmar la transacción
                    transaction.Commit();
                    Restablecer();
                }
                catch (Exception)
                {
                    // Si ocurre un error, revertir la transacción
                    transaction.Rollback();
                    throw;
                }
        }


        /* 
           BuscarEstudiante(string campo) se encarga de buscar estudiantes en la base de datos
           según ciertos criterios y muestra los resultados en un control dataGridView, con soporte
           para paginación y visualización de información específica dependiendo de si se encuentran 
           resultados o no.
         */
        public void BuscarEstudiante(string campo)
        {
            var db = new Connection();

            List<Estudiante> query = new List<Estudiante>();
            int inicio = (_run_pagina - 1) * _re_por_pagina;
            if (campo.Equals(0))
            {
                query= db._Estudiante.ToList();

            }
            else 
            { 
               query = db._Estudiante.Where(c => c.Seccion.StartsWith(campo) || c.Nombre.StartsWith(campo) 
                       || c.Apellido.StartsWith(campo)).ToList();
            }

            if (query.Count > 0)
            {
                dataGridView.DataSource = query.Select(c => new
                {   c.ID,
                    c.Seccion,
                    c.Nombre,
                    c.Apellido,
                    c.Email,
                    c.Imagen,
                    c.FechaInicio
                }) .Skip(inicio).Take(_re_por_pagina).ToList();
                dataGridView.Columns[5].Visible = false;
            }

            else
            {
                dataGridView.DataSource = query.Select(c => new
                {
                    c.ID,
                    c.Nombre,
                    c.Apellido,
                    c.Email
                }).ToList();     
            }

        }


        /*
          el método GetEstudiante() se utiliza para obtener los detalles de un estudiante seleccionado
          y mostrar esos detalles en los campos del formulario, incluyendo la imagen del estudiante.
          En caso de no poder obtener la imagen, se muestra una imagen predeterminada o de respaldo.
        */
        public void GetEstudiante()
        {
            accion = "update";
            idEstudiante = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
            listTexbox[0].Text = Convert.ToString(dataGridView.CurrentRow.Cells[1].Value);
            listTexbox[1].Text = Convert.ToString(dataGridView.CurrentRow.Cells[2].Value);
            listTexbox[2].Text = Convert.ToString(dataGridView.CurrentRow.Cells[3].Value);
            listTexbox[3].Text = Convert.ToString(dataGridView.CurrentRow.Cells[4].Value);

            try
            {
                byte[] arrayImage = (byte[])dataGridView.CurrentRow.Cells[5].Value;
                Imagen.Image = new Estudiante1().ByteArrayToImage(arrayImage);
            }
            catch (Exception)
            {

                Imagen.Image = Imagenbitmap;
            }
        }


        /*
         el método Registro_Pagina() se encarga de establecer los parámetros iniciales para la
         paginación de registros de estudiantes en el formulario, creando una instancia del paginador
         y mostrando los registros de la primera página si hay estudiantes en la base de datos.
        */
        public void Registro_Pagina()
        {
            var db = new Connection();
            _run_pagina = 1;
            _re_por_pagina = (int)numericUpDown.Value;
            var list = db._Estudiante.ToList();

            if(0 < list.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listlabel[4], _re_por_pagina);
                BuscarEstudiante("");
            }
        }


        /*
         El método Eliminar() se utiliza para eliminar un estudiante de la base de datos después 
         de mostrar una confirmación al usuario. Si el estudiante no está seleccionado, se muestra 
         un mensaje de advertencia. Después de la eliminación exitosa, se muestra un mensaje informativo
         y se llama a Restablecer() para limpiar el formulario.
         */
        public void Eliminar()
        {
            var db = new Connection();
            if (idEstudiante.Equals(0))
            {
                MessageBox.Show("Selecione un Registro");
            }
            else
            {
                if (MessageBox.Show("¿Estas Seguro de Eliminar el Estudiante?","Eliminar Estudiante",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db._Estudiante.Where(c => c.ID.Equals(idEstudiante)).Delete();
                    MessageBox.Show("Registro Eliminado");
                    Restablecer();
                }

                
            }
        }


        /*
         el método Restablecer() se utiliza para restablecer el formulario a sus valores y estados
         predeterminados después de una operación. Restaura los campos, colores y valores de las cajas
         de texto, la paginación y otras configuraciones a un estado inicial para preparar el formulario
         para futuras entradas o acciones
        */
        public void Restablecer()
        {
            accion = "insert";
            idEstudiante = 0;
            _run_pagina = 1;
            var db = new Connection();
            Imagen.Image = Imagenbitmap;
            listlabel[0].Text = "ID SECCION";
            listlabel[1].Text = "NOMBRE";
            listlabel[2].Text = "APELLIDO";
            listlabel[3].Text = "EMAIL";
            listlabel[0].ForeColor = Color.Black;
            listlabel[1].ForeColor = Color.Black;
            listlabel[2].ForeColor = Color.Black;
            listlabel[3].ForeColor = Color.Black;
            listTexbox[0].Text = "";
            listTexbox[1].Text = "";
            listTexbox[2].Text = "";
            listTexbox[3].Text = "";
            listEstudiante = db._Estudiante.ToList();
            if(0 < listEstudiante.Count)
            {
                _paginador = new Paginador<Estudiante>(listEstudiante, listlabel[4], _re_por_pagina);
             
            }
            BuscarEstudiante("");

        }


        /*
         Paginador(string metodo) se utiliza para gestionar las acciones de paginación, como ir a 
         la primera, anterior, siguiente o última página de los registros de estudiantes. Luego de 
         realizar la acción de paginación, se llama al método BuscarEstudiante("") para actualizar 
         la visualización en el formulario.
        */
        public void Paginador(string metodo)
        {
            switch (metodo)
            {
                case "Primero":
                    _run_pagina = _paginador.Primero();
                    break;

                case "Anterior":
                    _run_pagina = _paginador.Anterior();
                    break;

                case "Siguiente":
                    _run_pagina = _paginador.Siguiente();
                    break;

                case "Ultimo":
                    _run_pagina = _paginador.Ultimo();
                    break;
            }


            BuscarEstudiante("");
        }
    }


    public class Paginador<T>
    {  
       // Aqui se estan defidiendo las campos de clase con los que vamos a trabajar;
        private List<T> _datelist;
        private Label _label;
        private static int _maxPagi = 1;
        private static int _reg_por_pagina = 1;
        private static int _pagiCount = 1;
        private static int  _numPagi = 1;

        //Aqui definimos el Metodo  constructos el cual recibe tres parametros
        public Paginador(List<T> datelist, Label label, int re_por_pagina  ) 
        { 
          _datelist = datelist;
          _label = label;
          _reg_por_pagina = re_por_pagina;
           CargarDatos();
        
        }



        /*
          el método CargarDatos() se utiliza para calcular y cargar información relacionada con la 
          paginación en algún contexto específico. Calcula el número total de páginas necesarias para
          paginar los elementos y actualiza un label o texto para mostrar la página actual y el número
          total de páginas disponibles.
        */
        private void CargarDatos()
        {
            _numPagi = 0;
            _maxPagi = _datelist.Count;
            _pagiCount = (_maxPagi / _reg_por_pagina);

            if ((_maxPagi % _reg_por_pagina) > 0)
            {
                _pagiCount+=1;
            }

            _label.Text = $"Pagina 1/{_pagiCount}";
        }


        /*
        el método Primero() se utiliza para moverse a la primera página dentro de un contexto de paginación.
        Actualiza el valor de _numPagi, actualiza el texto del label y devuelve el nuevo número de página,
        que es 1.
        */
        public int Primero()
        {
            _numPagi = 1;
            _label.Text = $"Pagina {_numPagi}/{_pagiCount}";
            return _numPagi;
        }


        /*
         el método Anterior() se utiliza para moverse a la página anterior en un contexto de paginación,
         asegurándose de no ir a una página anterior si ya se encuentra en la primera página. Actualiza
         _numPagi, el texto del label y devuelve el nuevo número de página actual.
         */
        public int Anterior()
        {
            if(_numPagi > 1)
            { 
                _numPagi-=1;
                _label.Text = $"Pagina {_numPagi}/{_pagiCount}";
            }
            return _numPagi;
        }


        /*
          el método Siguiente() se utiliza para moverse a la página siguiente en un contexto de 
          paginación. Se asegura de que se pueda avanzar a la siguiente página incluso si ya se 
          encuentra en la última. Luego, actualiza _numPagi, el texto del label y devuelve el nuevo 
          número de página actual.
        */
        public int Siguiente()
        {
            if(_numPagi == _pagiCount)
            {
                _numPagi += 1;
            }
            if(_numPagi < _pagiCount)
            {
                _numPagi+=1;
                _label.Text = $"Pagina {_numPagi}/{_pagiCount}";
            }
            return _numPagi;
        }


        /*
      el método Ultimo() se utiliza para moverse a la última página dentro de un contexto de paginación. 
      Actualiza el valor de _numPagi, actualiza el texto del label y devuelve el nuevo número de página,
      que es el número total de páginas _pagiCount.
        */
        public int Ultimo()
        {
            _numPagi = _pagiCount;
            _label.Text = $"Pagina {_numPagi}/{_pagiCount}";
            return _numPagi;
        }


    }


}
  


