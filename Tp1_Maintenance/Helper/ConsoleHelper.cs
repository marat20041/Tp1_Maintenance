using Util;
using SchoolManager;
using System.Text.Json;


/// <summary>
/// Fournit des méthodes utilitaires pour interagir avec l’utilisateur via la console.
/// Permet de demander des informations, des choix ou de confirmer des actions.
/// </summary>

namespace Util
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Demande le nom, l'adresse et le téléphone d'un membre et retourne un objet <see cref="SchoolMember"/>.
        /// </summary>
        public static SchoolMember AskAttributes()
        {
            SchoolMember member = new SchoolMember();
            member.Name = AskQuestion(ReferenceText.Get("AskName"));
            member.Address = AskQuestion(ReferenceText.Get("AskAddress"));
            member.Phone = AskQuestion(ReferenceText.Get("AskPhone"));

            return member;
        }

        /// <summary>
        /// Affiche une question à l'utilisateur et retourne la réponse sous forme de chaîne.
        /// </summary>
        static public string AskQuestion(string question)
        {
            System.Console.Write(question);
            return Console.ReadLine()!;
        }


        /// <summary>
        /// Affiche une question à l'utilisateur et retourne la réponse convertie en entier.
        /// Boucle tant que l'utilisateur n'entre pas un entier valide.
        /// </summary>
        static public int AskQuestionInt(string question)
        {
            Console.Write(question);
            bool state = int.TryParse(Console.ReadLine(), out int result);
            while (!state)
            {
                Console.Write(ReferenceText.Get("Invalid"));
                state = int.TryParse(Console.ReadLine(), out result);
            }

            return result;
        }


        /// <summary>
        /// Demande le type de membre et retourne l'entier correspondant au <see cref="SchoolMemberType"/>.
        /// Retourne -1 si le type est invalide.
        /// </summary>
        public static int AskMemberType()
        {
            int x = AskQuestionInt(ReferenceText.Get("MemberOption"));
            return Enum.IsDefined(typeof(SchoolMemberType), x) ? x : -1;
        }


        /// <summary>
        /// Demande un choix principal dans le menu principal et retourne l'entier choisi.
        /// </summary>
        public static int AskChoices()
        {
            return AskQuestionInt(ReferenceText.Get("MainMenu"));
        }
        
        /// <summary>
        /// Demande une confirmation de l'utilisateur pour annuler une action.
        /// Retourne la réponse entrée par l'utilisateur.
        /// </summary>
        public static string ConfirmCancel(string action)
        {
            return AskQuestion(ReferenceText.Get("ConfirmCancel") + action + " (y/n) ? ");
        }
    }
}
