using Util;
using SchoolManager;
namespace Util

{

    public static class ConsoleHelper
    {
        static public string AskQuestion(string question)
        {
            System.Console.Write(question);
            return Console.ReadLine();
        }

        static public int AskQuestionInt(string question)
        {
            System.Console.Write(question);
            bool state = int.TryParse(System.Console.ReadLine(), out int result);
            while (!state)
            {
                System.Console.Write("Invalid input. Please try again: ");
                state = int.TryParse(System.Console.ReadLine(), out result);
            }

            return result;
        }

        public static SchoolMember AskAttributes()
        {
            string name = AskQuestion("Enter name: ");
            string address = AskQuestion("Enter address: ");
            string phone = AskQuestion("Enter phone: ");

            return new SchoolMember(name, address, phone);
        }

        public static int AskMemberType()
        {
            int x = AskQuestionInt("\n1. Principal\n2. Teacher\n3. Student\n4. Receptionist\nPlease enter the member type: ");
            return Enum.IsDefined(typeof(SchoolMemberType), x) ? x : -1;
        }

        public static int AskChoices()
        {
            return AskQuestionInt("\n1. Add\n2. Display\n3. Pay\n4. Raise Complaint\n5. Student Performance\n6. Remove last action\n7. Leave the program\nPlease enter the command: ");
        }
    }
}
