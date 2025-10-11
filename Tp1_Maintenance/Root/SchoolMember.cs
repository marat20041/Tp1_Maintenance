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
            get
            {
                return _phone;
            }
            set
            {
                var config = ConfigLoader.LoadConfig("networkConfig.json");
                if (PhoneVerificator.IsValidPhone(value, config))
                {
                    _phone = value;
                }
                else
                {
                    _phone = "";
                }
            }
        }

        public virtual string Display()
        {
            return $"Name: {Name}, Address: {Address}, Phone: {Phone}";
        }
    }


}
