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


namespace Caisse.View
{
    /// <summary>
    /// Logique d'interaction pour CreerPartie.xaml
    /// </summary>
    public partial class CreerPartie : UserControl
    {
        public List<String> listePseudo= new List<string> ();
        public CreerPartie()
        {
            InitializeComponent();
            //Récupère liste des joueurs
            Crud objCrud = new Crud();
            List<joueur> listJoueur = objCrud.recupListeJoueur();

            //créer la liste des pseudos et change le mdp par des * pour afficage dans le dg
            foreach(var item in listJoueur)
            {
                listePseudo.Add(item.pseudo);
                item.mdp = "********";
            }
            
            //Affichage dg
            dgListeJoueur.ItemsSource = listJoueur;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = tbPseudo.Text;

            //Si pseudo introuvable dans list pseudo ou si pseudo déjà dans la listbox, affiche message d'erreur, sinon, on ajoute dans la listbox
            if (!(listePseudo.Contains(pseudo)))
            {
                MessageBox.Show("Pseudo introuvable", "Erreur");
            }
            else if(ListBoxJoueur.Items.Contains(pseudo))
            {
                MessageBox.Show("Vous avez déjà ajouté ce joueur", "Erreur");
            }
            else
            {
                ListBoxJoueur.Items.Add(pseudo);
                tbPseudo.Text = "";
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            string pseudo = (String)ListBoxJoueur.SelectedValue;

            //Vérifie si un pseudo est déjà sélectionné, si oui, le supprime de la lb, sinon message d'erreur
            if (pseudo == null) MessageBox.Show("Selectionnez un pseudo", "Erreur");
            else ListBoxJoueur.Items.Remove(ListBoxJoueur.SelectedItem);
        }

        private void btValid_Click(object sender, RoutedEventArgs e)
        {
            List<String> listPseudoPrPartie = new List<string>();

            //Parcours la lb et créer la liste listPseudoPrPartie
            foreach(Object item in ListBoxJoueur.Items)
            {
                listPseudoPrPartie.Add((string)item);
            }

            //Vérifie si la liste est vide
            if(listPseudoPrPartie.Count == 0)
            {
                MessageBox.Show("Vous devez sélectionner au moins 1 joueur", "Erreur");
            }
            else
            {
                try
                {
                    int numPiste = addJoueurPartie(listPseudoPrPartie);
                    MessageBox.Show("Partie créée ! Votre piste est la numéro " + numPiste, "Information");
                }
                catch(Exception excep)
                {
                    MessageBox.Show("Erreur lors de la création de la partie.", "Erreur");
                    Console.WriteLine(excep);
                }
            }
        }

        private int addJoueurPartie(List<String> listJoueurAdd)
        {
            Crud objCrud = new Crud();
            //recup liste joueurs et parties
            List<joueur> listJoueur = objCrud.recupListeJoueur();
            List<partie> listPartie = objCrud.recupListePartie();

            //créer listes des numPistes des parties existantes + liste desid des joueurs à ajouter
            List<int> listNumPistePartie = new List<int>();
            List<int> listIdAddPartie = new List<int>();

            //Parcours liste des joueurs à ajouter et liste des joueurs pour récupérer les id à ajouter
            for (int i = 0; i < listJoueurAdd.Count; i++)
            { 
                for(int w = 0; w < listJoueur.Count; w++)
                {
                    if (listJoueurAdd[i] == listJoueur[w].pseudo)
                    {
                        listIdAddPartie.Add(listJoueur[w].id);
                    }
                }
            }

            //Tri lea liste des parties pour enlever les doublons de numéros de pistes
            for(int i = 0; i < listPartie.Count; i++)
            {
                if (!(listNumPistePartie.Contains(listPartie[i].numPiste.Value)))
                {
                    listNumPistePartie.Add(listPartie[i].numPiste.Value);
                }
            }

            objCrud.addPartie(listIdAddPartie, listNumPistePartie.Count);

            //Renvoie le numéro de la piste pour la partie créée
            return listNumPistePartie.Count;
        }

        private void dgListeJoueur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                joueur selectedJoueur = new joueur();

                //Récupère l'objet sélectionné dans le datagrid
                selectedJoueur = (joueur)dgListeJoueur.SelectedItem;

                //Vérifie s'il existe pas déjà dans la listBox
                if (ListBoxJoueur.Items.Contains(selectedJoueur.pseudo))
                {
                    MessageBox.Show("Vous avez déjà ajouté ce joueur", "Erreur");
                }
                else
                {
                    ListBoxJoueur.Items.Add(selectedJoueur.pseudo);
                }
            }
            catch(Exception excep)
            {
                MessageBox.Show("Erreur Data Grid", "Erreur");
            }
        }
    }
}