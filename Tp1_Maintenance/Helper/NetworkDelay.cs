using System;
using System.Threading;


/// <summary>
/// Fournit des méthodes pour simuler des délais réseau et gérer le paiement des entités.
/// </summary>
namespace Util
{
    public class NetworkDelay
    {
        private static HelperConfig? _config;


        /// <summary>
        /// Charge la configuration nécessaire pour les délais réseau depuis un fichier JSON.
        /// </summary>
        /// <param name="path">Chemin du fichier de configuration JSON.</param>
        public static void LoadConfig(string path)
        {
            _config = ConfigLoader.LoadConfig(path);
        }

        /// <summary>
        /// Simule un délai réseau aléatoire entre les valeurs minimales et maximales définies dans la configuration.
        /// Lance une exception si la configuration n'a pas été chargée.
        /// </summary>
        static public void SimulateNetworkDelay()
        {
            if (_config == null) throw new Exception(ReferenceText.Get("ConfigNotLoaded"));
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(_config.MinDelay, _config.MaxDelay));
        }

        /// <summary>
        /// Simule le paiement d'une entité en ajoutant un revenu à son solde.
        /// Affiche un message indiquant le paiement effectué.
        /// <summary>
        /// <param name="entity">Type de l'entité (ex. "Teacher", "Principal").</param>
        /// <param name="name">Nom de l'entité.</param>
        /// <param name="balance">Balance actuelle, mise à jour après le paiement.</param>
        /// <param name="income">Montant du revenu à ajouter.</param>
        static public void PayEntity(string entity, string name, ref int balance, int income)
        {
            SimulateNetworkDelay();
            balance += income;
            Console.WriteLine(ReferenceText.Format("EntityPaid", new Dictionary<string, string>
        {
            { "entity", entity },
            { "name", name },
            { "balance", balance.ToString() }
        }));
        }
    }
}
