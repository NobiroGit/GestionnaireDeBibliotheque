using GestionnaireDeBibliothèque;
using System.Text.RegularExpressions;
using static GestionnaireDeBibliothèque.Livres;

List<Livres> BibliothequeLivre = new List<Livres>();

Bibliotheque.Charger(BibliothequeLivre);
Livres.SauvegardeListeId();

int choix;
do

{
    
    Console.WriteLine($"""

        1. Afficher les livres 
        2. Rechercher un livre
        3. Emprunter un livre
        4. Rendre un livre
        5. Ajouter un livre à la bibliothèque
        6. Supprimer un livre de la bibliothèque
        7. Quitter la bibliothèque

        """);

    while (!int.TryParse(Console.ReadLine(), out choix) || choix < 1 || choix > 7)
    {
        Console.WriteLine("Choix incorrect !");
    }

    switch (choix)
    {
        case 1:
            Bibliotheque.AfficherLivres(BibliothequeLivre);
            break;

        case 2:
            Bibliotheque.RechercheLivres(BibliothequeLivre);
            break;

        case 3:
            Bibliotheque.EmprunterLivre(BibliothequeLivre);
            break;

        case 4:
            Bibliotheque.RendreLivre(BibliothequeLivre);
            break;

        case 5:
            Bibliotheque.AjouterLivre(BibliothequeLivre);
            break;

        case 6:
            Bibliotheque.SupprimerLivreParId(BibliothequeLivre);
            break;

        default:
            Bibliotheque.Sauvegarder(BibliothequeLivre);
            break;
    };
}
while (choix != 7);

