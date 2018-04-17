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
using MySql.Data.MySqlClient;

namespace Caisse.View
{
    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : UserControl
    {
        public Inscription()
        {
            InitializeComponent();
        }

        private void Button_ClickSave(object sender, RoutedEventArgs e)
        {
            //Vérife si l'un des champs n'est pas vide
            if (!(PseudoJoueur.Text == "" || PseudoJoueur.Text == "" || NomJoueur.Text == "" || PrenomJoueur.Text == "" || Calendrier.Text == "" || MailJoueur.Text == "" || MDPJoueur.Text == ""))
            {
                try
                {
                    //Créer et rempli l'obj joueur a ajouter à la bdd
                    joueur objJoueur = new joueur();
                    objJoueur.pseudo = PseudoJoueur.Text;
                    objJoueur.nom = NomJoueur.Text;
                    objJoueur.prenom = PrenomJoueur.Text;
                    objJoueur.dateCreation = DateTime.Parse(Calendrier.Text);
                    objJoueur.mail = MailJoueur.Text;
                    objJoueur.mdp = MDPJoueur.Text;


                    Crud objCrud = new Crud();
                    objCrud.inscrireJoueur(objJoueur);

                    MessageBox.Show("Inscription réussie", "Information");
                }
                catch (Exception excep)
                {
                    MessageBox.Show("Une erreur s'est produite", "Erreur");
                }
            }
            else
            {
                MessageBox.Show("Un champ n'est pas rempli", "Erreur");
            }

        }
    }
}
