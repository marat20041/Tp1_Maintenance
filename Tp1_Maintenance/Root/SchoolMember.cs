using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManager
{
    public class SchoolMember
    {
        /* 1-Modification du type de phone
         2 - Ajout des paramètres de recuperation et de modification
        */
        public string Name { get; set; }
        public string Address { get; set; }
        private string _phone;

        public SchoolMember(string name = "", string address = "", string phone = "")
        {

            if (string.IsNullOrWhiteSpace(name) || name.All(char.IsDigit))
            {
                Name = "";
            }
            else
            {
                Name = name;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                Address = "";
            }
            else
            {
                Address = address;
            }

            _phone = phone;

        }
        public string Phone
        {
            get => _phone;
            set
            {
                var config = ConfigLoader.LoadConfig("networkConfig.json");
                while (!PhoneVerificator.IsValidPhone(value, config))
                {
                    Console.WriteLine(ReferenceText.Get("AskPhoneAgain"));
                    Console.Write("Enter a valid phone number: ");
                    value = Console.ReadLine() ?? "";
                }
                _phone = value;
            }
        }

        public virtual string Display()
        {
            return $"Name: {Name}, Address: {Address}, Phone: {Phone}";
        }
    }


}
