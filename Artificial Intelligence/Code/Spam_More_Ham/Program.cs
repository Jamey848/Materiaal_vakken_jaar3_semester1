namespace Spam_More_Ham
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Hamspam hs = new Hamspam("data.txt");

            Dictionary<string, int> blub = hs.spam_dict
                .OrderByDescending(x => x.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            //Win new iphone 5 with unlimited data! => Spam
            //Hey, did you find my car keys anywhere? Can't find 'em anymore. => Spam
            //Where did you go? I can't see you anymore? => Ham

            bool checkspam = hs.IsSpam("Where did you go? I can't see you anymore? WIN WIN WIN WIN");
            if(checkspam)
            {
                Console.WriteLine("SPAM!!!!");
            }
            else
            {
                Console.WriteLine("Ham");
            }

        }
    }
}
