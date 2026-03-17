using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GestionnaireDeBibliothèque
{
    internal class Livres
    {
        public int id { get; set; }
        public string titre { get; }
        public string auteur { get; }
        public int annee { get; }
        public Genre genre { get; }
        public bool dispo { get; private set; }

        public static List<int> idList = new List<int>();

        /// <summary>
        /// CTOR pour que l'utilisateur ajoute un livre manuellement 
        /// </summary>
        /// <param name="titre"></param>
        /// <param name="auteur"></param>
        /// <param name="annee"></param>
        /// <param name="genre"></param>
        public Livres(string titre, string auteur, int annee, Genre genre)
        {
            this.titre = titre;
            this.auteur = auteur;
            this.annee = annee;
            this.genre = genre;
            this.id = GenerationId(genre);
            this.dispo = true;
        }
        /// <summary>
        /// CTOR pour charger la bibliothèque à partir d'un fichier txt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="auteur"></param>
        /// <param name="annee"></param>
        /// <param name="genre"></param>
        /// <param name="dispo"></param>
        public Livres(int id, string titre, string auteur, int annee, Genre genre, bool dispo) 
        {
            this.titre = titre;
            this.auteur = auteur;
            this.annee = annee;
            this.genre = genre;
            this.id = id;
            this.dispo = true;
        }
        /// <summary>
        /// Génère un identifiant unique pour un livre en fonction du genre spécifié.
        /// </summary>
        /// <remarks>L'identifiant généré combine un préfixe spécifique au genre et une valeur aléatoire.
        /// L'appel répété de cette méthode garantit que chaque identifiant est unique dans la collection
        /// actuelle.</remarks>
        /// <param name="genre">Le genre du livre pour lequel l'identifiant doit être généré. Ce paramètre détermine le préfixe de
        /// l'identifiant.</param>
        /// <returns>Un entier représentant l'identifiant unique généré pour le livre. L'identifiant est garanti unique parmi
        /// ceux déjà générés.</returns>
        private int GenerationId(Genre genre) 
        {   
            int idGlobal;
            do
            {
                
                int idFin = Random.Shared.Next(10000);
                string identifiant;

                switch (genre) 
                {
                    case Genre.Fiction: identifiant = "10"; break;
                    case Genre.Fantasy: identifiant = "20"; break;
                    case Genre.Sciencefiction: identifiant = "30"; break;
                    case Genre.Policier: identifiant = "40"; break;
                    default: identifiant = "50"; break;
                }

                idGlobal = int.Parse(identifiant + idFin.ToString());
            }
            while (idList.Contains(idGlobal));

            idList.Add(idGlobal);
            return idGlobal; 
        }
        /// <summary>
        /// Enregistre la liste d'identifiants unique dans un fichier texte nommé "idListeUnique.txt" à l'emplacement du
        /// répertoire courant.
        /// </summary>
        /// <remarks>Si une erreur survient lors de la sauvegarde, un message d'échec est affiché dans la
        /// console. Le fichier est écrasé à chaque appel de la méthode.</remarks>
        public static void SauvegardeListeId() 
        {
            try
            {
                string chemin = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fichierTexte\idListeUnique.txt"));
                using StreamWriter writer = new StreamWriter(chemin);
                foreach (int id in idList)
                {
                    string ligne = id.ToString();
                    writer.WriteLine(ligne);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Sauvegarde liste ID impossible");
            }
        }
        /// <summary>
        /// Modifie en false ou true si un livre est dispo ou pas
        /// </summary>
        /// <param name="valeur"></param>
        public void Disponible(bool valeur) 
        {
            dispo = valeur;
        }
        /// <summary>
        /// Enumération des genres de livre
        /// </summary>
        public enum Genre
        {
            Fiction,
            Fantasy,
            Sciencefiction,
            Policier,
            Romance

        }
    }
}
