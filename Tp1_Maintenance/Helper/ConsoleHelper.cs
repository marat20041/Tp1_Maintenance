using Util;
using SchoolManager;
using System.Text.Json;
namespace Util


{

    public static class ConsoleHelper
    {
        public static SchoolMember AskAttributes()
        {
            SchoolMember member = new SchoolMember();
            member.Name = AskQuestion(ReferenceText.Get("Name"));
            member.Address = AskQuestion(ReferenceText.Get("Address"));
            member.Phone = AskQuestion(ReferenceText.Get("Phone"));

            return member;
        }
        static public string AskQuestion(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }

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

        public static int AskMemberType()
        {
            int x = AskQuestionInt(ReferenceText.Get("MemberOption"));
            return Enum.IsDefined(typeof(SchoolMemberType), x) ? x : -1;
        }

        public static int AskChoices()
        {
            return AskQuestionInt(ReferenceText.Get("Option"));
        }
    }
}
