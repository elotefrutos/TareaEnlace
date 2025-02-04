using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string nombre, telefono;
        public string Nombre { get => nombre; set { nombre = value; OnPropertyChanged(); OnPropertyChanged(nameof(PuedeGuardar)); } }
        public string Telefono { get => telefono; set { telefono = value; OnPropertyChanged(); OnPropertyChanged(nameof(PuedeGuardar)); } }
        public bool PuedeGuardar => !string.IsNullOrWhiteSpace(Nombre) && !string.IsNullOrWhiteSpace(Telefono);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void GuardarEnXML(object sender, RoutedEventArgs e)
        {
            string rutaArchivo = "contactos.xml";
            XDocument doc = File.Exists(rutaArchivo) ? XDocument.Load(rutaArchivo) : new XDocument(new XElement("Contactos"));
            doc.Root.Add(new XElement("Contacto", new XElement("Nombre", Nombre), new XElement("Telefono", Telefono)));
            doc.Save(rutaArchivo);
            MessageBox.Show("Contacto guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            Nombre = Telefono = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
