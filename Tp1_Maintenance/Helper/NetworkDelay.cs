using System;
using System.Threading;

namespace Util
{
    public class NetworkDelay
    {
         private static HelperConfig _config;
        
        public static void LoadConfig(string path)
        {
            _config = ConfigLoader.LoadConfig(path);
        }

        static public void SimulateNetworkDelay()
        {
            if (_config == null) throw new Exception("Config not loaded");
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(_config.MinDelay, _config.MaxDelay));
        }

        static public void PayEntity(string entity, string name, ref int balance, int income)
        {
            SimulateNetworkDelay();
            balance += income;
            Console.WriteLine($"Paid {entity}: {name}. Total balance: {balance}");
        }
    }
}
