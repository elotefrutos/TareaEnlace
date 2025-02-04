using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TareaEnlace
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActualizarEstadoBoton(object sender, RoutedEventArgs e)
        {
            lblNombre.Text = txtNombre.Text;
            lblTelefono.Text = txtTelefono.Text;
            btnGuardar.IsEnabled = !string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtTelefono.Text);
        }

        private void GuardarEnXML(object sender, RoutedEventArgs e)
        {
            XElement contacto = new XElement("Contacto",
                new XElement("Nombre", txtNombre.Text),
                new XElement("Telefono", txtTelefono.Text)
            );

            string rutaArchivo = "contactos.xml";
            XDocument doc;

            if (File.Exists(rutaArchivo))
            {
                doc = XDocument.Load(rutaArchivo);
                doc.Root.Add(contacto);
            }
            else
            {
                doc = new XDocument(new XElement("Contactos", contacto));
            }

            doc.Save(rutaArchivo);
            MessageBox.Show("Contacto guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
