using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;



namespace Logica.Libreria
{
   
        public class updateimag
        {


        // cleacion del objecto de OpenFileDialog
        public OpenFileDialog Ofd = new OpenFileDialog();

        /* ste método CargarImageen permite a los usuarios seleccionar una imagen de su sistema de
         archivos y cargarla en un control PictureBox. La imagen se ajusta automáticamente para
         adaptarse al tamaño del control.
        */
        public void CargarImageen(PictureBox pictureBox)
            {

                pictureBox.WaitOnLoad = true;
                Ofd.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp";
                Ofd.ShowDialog();


                if (Ofd.FileName != string.Empty)
                {
                    pictureBox.ImageLocation = Ofd.FileName;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                }

            }


        /*
         método llamado ImagetoByte, que convierte una imagen de tipo System.Drawing.Image en un arreglo
         de bytes (byte[]). Aquí tienes una descripción de cómo funciona este método:
        */
        public byte[] ImagetoByte(System.Drawing.Image img)
        {
            var convert = new ImageConverter();

            return (byte[])convert.ConvertTo(img,typeof(byte[]));
        }



        /*
         ByteArrayToImage, que convierte un arreglo de bytes (byte[]) en un objeto de tipo 
        System.Drawing.Image

        */
        public System.Drawing.Image ByteArrayToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);

            return System.Drawing.Image.FromStream(ms);
        }


    }
    
}
