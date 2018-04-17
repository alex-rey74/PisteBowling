using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;


namespace Caisse
{
    class Crud
    {
        //Récupère un obj joueur et l'enregistre dans la bdd
        public void inscrireJoueur(joueur p)
        {
            using(var context = new bddbowlingEntities())
            {
                context.joueur.Add(p);
                context.SaveChanges();
            }
        }

        //Renvoi la liste des joueurs
        public List<joueur> recupListeJoueur()
        {
            using (var context = new bddbowlingEntities())
            {
                var Data = (from d in context.joueur select d).ToList();
                return Data;
            }
        }

        //Renvoi la liste des parties
        public List<partie> recupListePartie()
        {
            using (var context = new bddbowlingEntities())
            {
                var Data = (from d in context.partie select d).ToList();
                return Data;
            }
        }

        //Créer une nouvelle partie
        public void addPartie(List<int> listIdJoueur, int numPiste)
        {
            //Créer un obj partie, ajoute le num de piste et le datetime actuel
            partie objPartie = new partie();
            objPartie.numPiste = numPiste;
            objPartie.datePartie = DateTime.Now;

            //Envoie la requête de création autant de fois qu'il y a de joueurs dans la partie
            for (int i = 0; i < listIdJoueur.Count; i++)
            {
                using (var context = new bddbowlingEntities())
                {
                    objPartie.joueur_id = listIdJoueur[i];
                    context.partie.Add(objPartie);
                    context.SaveChanges();
                }
            }
        }
    }
}