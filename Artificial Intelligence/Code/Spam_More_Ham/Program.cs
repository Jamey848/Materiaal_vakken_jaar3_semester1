namespace Spam_More_Ham
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> spam = new List<string>();
            List<string> ham = new List<string>();

            foreach (string line in File.ReadAllLines("data.txt"))
            {
                if (line.StartsWith("spam")) spam.Add(line.Substring(5));
                else ham.Add(line.Substring(4));
            }

            Console.WriteLine(String.Join(" ", spam));

            Console.WriteLine(String.Join(" ", ham));
            Hamspam nb = new Hamspam();
        }
    }
}
