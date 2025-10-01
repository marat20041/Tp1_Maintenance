namespace SchoolManager
{
    public class SchoolMember
    {
        /* 1-Modification du type de phone
         2 - Ajout des paramètres de recuperation et de modification
        */
        public string Name { get; set; }
        public string Address{ get; set; }
        private string phone;
        
       

        public SchoolMember(string name = "", string address = "", string phone = "")
        {
            Name = name;
            Address = address;
            this.phone = phone;
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}
