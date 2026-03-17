using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace GestionnaireDeBibliothèque
{
    internal class Bibliotheque
    {
        /// <summary>
        /// Ajoute un nouveau livre à la bibliothèque en recueillant les informations auprès de l'utilisateur.
        /// </summary>
        /// <remarks>Cette méthode interagit avec la console pour obtenir les détails du livre, valider
        /// les entrées et afficher l'identifiant du livre ajouté. Elle ne retourne aucune valeur et modifie la liste
        /// passée en paramètre.</remarks>
        /// <param name="BibliothequeL">La liste de livres à laquelle le nouveau livre sera ajouté. Ne doit pas être null.</param>
        public static void AjouterLivre(List<Livres> BibliothequeL)
        {
            #region Insertion des données du livre

            bool validationLivre = false;
            string titreL;
            string auteurL;
            int anneeL;
            Livres.Genre genreL = Livres.Genre.Fiction;

            do
            {

                #region Encodage titre

                string patternRegTitre = @"^[\p{L}\p{N}][\p{L}\p{N}\s'’""\-:;,.!?()&]*$";

                Console.WriteLine("""

                    Quel est le titre du livre ?

                    """);
                do
                {
                    titreL = Console.ReadLine();
                    if (!Regex.IsMatch(titreL, patternRegTitre)) Console.WriteLine("Titre incorrect !");
                }
                while (!Regex.IsMatch(titreL, patternRegTitre));

                #endregion

                #region Encodage Auteur

                string patternRegAuteur = @"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '-][A-Za-zÀ-ÖØ-öø-ÿ]+)+$";

                Console.WriteLine("""

                    Quel est l'auteur du livre ? 

                    """);

                do
                {
                    auteurL = Console.ReadLine();
                    if (!Regex.IsMatch(auteurL, patternRegAuteur)) Console.WriteLine("Nom d'auteur incorrect !");
                }
                while (!Regex.IsMatch(auteurL, patternRegAuteur));

                #endregion

                #region Encodage Annee


                Console.WriteLine("""

                    Quel est l'année de parrution du livre ? 

                    """);


                while (!int.TryParse(Console.ReadLine(), out anneeL) || anneeL.ToString().Length != 4)
                {
                    if (!int.TryParse(Console.ReadLine(), out anneeL) || anneeL.ToString().Length != 4) Console.WriteLine("Date incorrect !");
                }

                #endregion

                #region Encodage Genre

                string choix;
                bool choixGenre = false;

                Console.WriteLine($"""

                Quel est le genre du livre ? 

                *{Livres.Genre.Fiction.ToString()}
                *{Livres.Genre.Fantasy.ToString()}
                *{Livres.Genre.Sciencefiction.ToString()}
                *{Livres.Genre.Policier.ToString()}
                *{Livres.Genre.Romance.ToString()}

                """);
                do
                {
                    choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "fiction": genreL = Livres.Genre.Fiction; choixGenre = true; break;
                        case "fantasy": genreL = Livres.Genre.Fantasy; choixGenre = true; break;
                        case "sciencefiction": genreL = Livres.Genre.Sciencefiction; choixGenre = true; break;
                        case "policier": genreL = Livres.Genre.Policier; choixGenre = true; break;
                        case "romance": genreL = Livres.Genre.Romance; choixGenre = true; break;
                        default: Console.WriteLine("Genre inexistant !! "); break;
                    }
                }
                while (!choixGenre);

                Console.WriteLine($"""

                    Est ce que toute les informations sont valide ? Oui ou Non

                    Titre : {titreL} | Auteur : {auteurL} | Année : {anneeL} | Genre : {genreL.ToString()}
                    """);

                string validation = Console.ReadLine()?.ToLower()?.Trim();
                if (validation == "oui") validationLivre = true;
                #endregion
            }
            while (!validationLivre);

            #endregion

            #region Ajout du livre dans la bibliotheque

            Livres livre = new Livres(titreL, auteurL, anneeL, genreL);

            Console.WriteLine($"""

                Voici l'identifiant de votre livre : {livre.id}

                """);

            BibliothequeL.Add(livre);

            #endregion
        }
        /// <summary>
        /// Supprime un livre de la bibliothèque en fonction de l'identifiant fourni par l'utilisateur.
        /// </summary>
        /// <remarks>Si aucun livre avec l'identifiant spécifié n'est trouvé, aucun élément n'est supprimé
        /// et un message d'erreur est affiché. Cette méthode interagit avec la console pour obtenir l'identifiant du
        /// livre à supprimer.</remarks>
        /// <param name="BibliothequeL">La liste des livres dans laquelle le livre correspondant à l'identifiant sera supprimé. Ne doit pas être
        /// null.</param>
        public static void SupprimerLivreParId(List<Livres> BibliothequeL)
        {
            int idUtilisateur;
            int countBibli = BibliothequeL.Count();
            Livres livreASupp = null;


            Console.WriteLine($"""

                Quel livre voulez vous supprimer ? 
                
                Veuillez écrire son ID !

                """);

            idUtilisateur = int.Parse(Console.ReadLine());


            foreach (Livres livres in BibliothequeL)
            {
                if (livres.id == idUtilisateur) livreASupp = livres;
            }

            if (livreASupp != null) BibliothequeL.Remove(livreASupp);

            if (countBibli == BibliothequeL.Count()) Console.WriteLine("Livre inexistant");
            else
            {
                Console.WriteLine("Votre livre a bien été retirer de notre bibliothèque");
            }
        }
        /// <summary>
        /// Affiche les informations détaillées de chaque livre contenu dans la liste spécifiée sur la console.
        /// </summary>
        /// <remarks>Utilisez cette méthode pour visualiser rapidement l'ensemble des livres d'une bibliothèque dans un
        /// format lisible. La méthode affiche chaque livre sur la console, incluant l'identifiant, le titre, l'auteur, l'année,
        /// le genre et la disponibilité.</remarks>
        /// <param name="BibliothequeL">La liste des objets de type Livres à afficher. Ne doit pas être null. Chaque élément représente un livre dont les
        /// détails seront affichés.</param>
        public static void AfficherLivres(List<Livres> BibliothequeL)
        {
            foreach (Livres livre in BibliothequeL)
            {
                Console.WriteLine($"""
                    -----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ID : {livre.id} Titre : {livre.titre} | Auteur : {livre.auteur} | Année : {livre.annee} | Genre : {livre.genre.ToString()} | Disponible {(livre.dispo ? "Oui" : "Non")}
                    -----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    """);
            }
        }
        /// <summary>
        /// Recherche et affiche les informations d'un livre dans la bibliothèque en fonction du titre, de l'auteur ou
        /// de l'identifiant saisi par l'utilisateur.
        /// </summary>
        /// <remarks>La méthode effectue une recherche interactive via la console. Si aucun livre
        /// correspondant n'est trouvé, un message est affiché à l'utilisateur. La recherche n'est pas sensible à la
        /// casse pour le titre et l'auteur.</remarks>
        /// <param name="BibliothequeL">La liste des livres disponibles dans la bibliothèque à parcourir pour la recherche. Ne doit pas être null.</param>
        public static void RechercheLivres(List<Livres> BibliothequeL)
        {
            Console.WriteLine($"""

                Quel est le livre que vous cherchez ? 

                """);

            string auteurTitre = Console.ReadLine();
            bool estNbr = int.TryParse(auteurTitre, out int idRecherche);
            bool correspondance = false;
            bool livreTrouve = false;

            foreach (Livres livre in BibliothequeL)
            {
                if (livre.titre.Equals(auteurTitre, StringComparison.OrdinalIgnoreCase) || livre.auteur.Equals(auteurTitre, StringComparison.OrdinalIgnoreCase) || livre.id == idRecherche)
                {
                    correspondance = true;
                    livreTrouve = true;
                    Console.WriteLine($"""
                    -----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    ID : {livre.id} | Titre : {livre.titre} | Auteur : {livre.auteur} | Année : {livre.annee} | Genre : {livre.genre.ToString()} | Disponible {(livre.dispo ? "Oui" : "Non")}
                    -----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    """);
                }
                else { correspondance = false; }

            }

            if(!livreTrouve) Console.WriteLine("Le livre n'est pas présent chez nous");
        }
        /// <summary>
        /// Enregistre la liste des livres dans un fichier texte au format délimité par des barres verticales.
        /// </summary>
        /// <remarks>Le fichier est créé ou remplacé à l'emplacement 'DB_Bibliotheque.txt' dans le
        /// répertoire courant. Si une erreur survient lors de la sauvegarde, un message d'erreur est affiché dans la
        /// console.</remarks>
        /// <param name="BibliothequeL">La collection de livres à sauvegarder. Chaque livre de la liste sera écrit dans le fichier. Ne doit pas être
        /// null.</param>
        public static void Sauvegarder(List<Livres> BibliothequeL)
        {
            try
            {
                string chemin = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fichierTexte\DB_Bibliotheque.txt"));
                using StreamWriter writer = new StreamWriter(chemin);
                foreach (Livres livre in BibliothequeL)
                {
                    string ligne = $"{livre.id}|{livre.titre}|{livre.auteur}|{livre.annee}|{livre.genre}|{livre.dispo}";
                    writer.WriteLine(ligne);
                }
                Console.WriteLine("Sauvegarde des ID reussi !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur de sauvegarde");
            }
        }
        /// <summary>
        /// Charge la liste des livres depuis le fichier de base de données et met à jour la collection fournie.
        /// </summary>
        /// <remarks>Le fichier source doit exister et être accessible sous le nom 'DB_Bibliotheque.txt'
        /// dans le répertoire courant. Si le fichier n'est pas trouvé ou si une erreur survient lors du chargement, la
        /// collection reste vide et un message d'erreur est affiché dans la console.</remarks>
        /// <param name="BibliothequeL">La collection de livres à remplir avec les données chargées. La liste est effacée avant d'être mise à jour.</param>
        public static void Charger(List<Livres> BibliothequeL)
        {
            string chemin = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\fichierTexte\DB_Bibliotheque.txt"));
            Console.WriteLine(chemin);
            try
            {
                if (!File.Exists(chemin))
                {
                    Console.WriteLine("Fichier inexistant");
                }

                BibliothequeL.Clear();

                string[] lignes = File.ReadAllLines(chemin);
                foreach (string ligne in lignes)
                {
                    string[] parties = ligne.Split('|');

                    if (parties.Length != 6) continue;

                    int id = int.Parse(parties[0]);
                    string titre = parties[1];
                    string auteur = parties[2];
                    int annee = int.Parse(parties[3]);
                    Livres.Genre genre = Enum.Parse<Livres.Genre>(parties[4]);
                    bool dispo = bool.Parse(parties[5]);

                    Livres livre = new Livres(id, titre, auteur, annee, genre, dispo);

                    BibliothequeL.Add(livre);
                    Livres.idList.Add(id);
                }
                Console.WriteLine("""
                    ***********************
                    *                     *
                    * Chargement reussi ! *
                    *                     *
                    ***********************
                    """);
            }
            catch
            {
                Console.WriteLine("Erreur de chargement de la liste DB_Bibliotheque.txt");
            }
        }
        /// <summary>
        /// Permet à un utilisateur d'emprunter un livre de la bibliothèque en recherchant par identifiant ou par titre.
        /// Met à jour l'état de disponibilité du livre si l'emprunt est possible.
        /// </summary>
        /// <remarks>Si le livre demandé n'est pas disponible ou n'existe pas dans la liste, un message
        /// approprié est affiché. L'emprunt ne peut être effectué que si le livre est marqué comme
        /// disponible.</remarks>
        /// <param name="BibliothequeL">La liste des livres disponibles dans la bibliothèque. Chaque livre doit contenir un identifiant, un titre et
        /// un état de disponibilité.</param>
        public static void EmprunterLivre(List<Livres> BibliothequeL)
        {
            Console.WriteLine("""

                Quel est le livre que vous souhaitez emprunter ?

                """);

            string idTitre = Console.ReadLine();
            bool estNbr = int.TryParse(idTitre, out int correspondance);
            bool livreTrouve = false;

            foreach (Livres livre in BibliothequeL)
            {
                bool correspond = false;

                if (estNbr)
                {
                    correspond = livre.id == correspondance;
                }
                else
                {
                    correspond = livre.titre.Equals(idTitre, StringComparison.OrdinalIgnoreCase);
                }

                if (correspond)
                {
                    livreTrouve = true;

                    if (livre.dispo)
                    {
                        livre.Disponible(false);
                        // ou livre.Disponible(false);

                        Console.WriteLine($"""

                            Emprunt effectué à la date du {DateTime.Now} !

                            """);
                    }
                    else
                    {
                        Console.WriteLine("""

                            Un emprunt a déjà été effectué !

                            """);
                    }

                    break;
                }
            }

            if (!livreTrouve)
            {
                Console.WriteLine("Livre inexistant");
            }
        }
        /// <summary>
        /// Permet à l'utilisateur de rendre un livre dans la bibliothèque en identifiant le livre par son titre ou son
        /// identifiant.
        /// </summary>
        /// <remarks>Si le livre n'est pas trouvé dans la liste ou s'il n'a pas été emprunté, un message
        /// approprié est affiché à l'utilisateur. Le livre est marqué comme disponible après avoir été rendu.</remarks>
        /// <param name="BibliothequeL">La liste des livres disponibles dans la bibliothèque. Chaque livre doit être représenté par un objet de type
        /// Livres.</param>
        public static void RendreLivre(List<Livres> BibliothequeL)
        {
            Console.WriteLine("""

                Quel est le livre que vous souhaitez rendre ?

                """);

            string idTitre = Console.ReadLine();
            bool estNbr = int.TryParse(idTitre, out int correspondance);
            bool livreTrouve = false;

            foreach (Livres livre in BibliothequeL)
            {
                bool correspond = false;

                if (estNbr)
                {
                    correspond = livre.id == correspondance;
                }
                else
                {
                    correspond = livre.titre.Equals(idTitre, StringComparison.OrdinalIgnoreCase);
                }

                if (correspond)
                {
                    livreTrouve = true;

                    if (!livre.dispo)
                    {
                        livre.Disponible(true);
                        // ou livre.Disponible(false);

                        Console.WriteLine($"""

                        Vous avez rendu le livre à la date du {DateTime.Now} !

                        """);
                    }
                    else
                    {
                        Console.WriteLine("""

                         Vous ne pouvez pas rendre un livre que vous n'avez pas emprunté !

                        """);
                    }

                    break;
                }
            }

            if (!livreTrouve)
            {
                Console.WriteLine("Livre inexistant");
            }
        }
    }
}
