namespace SchoolManager
{
    public class SchoolMember
    {
        
        public string Name;
        public string Address;
        
         // Modification du type de phone
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
