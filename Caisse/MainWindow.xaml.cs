using System;
using System.Collections.Generic;
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
using System.IO;
using System.Data.Entity;
using Caisse.View;


namespace Caisse
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
         InitializeComponent();

            Container.Children.Clear();
            Home HomeObj = new Home();
            Container.Children.Add(HomeObj);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            Inscription InscriptionObj = new Inscription();
            Container.Children.Add(InscriptionObj);

        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            CreerPartie CreerPartieObj = new CreerPartie();
            Container.Children.Add(CreerPartieObj);

        }
    }
}
