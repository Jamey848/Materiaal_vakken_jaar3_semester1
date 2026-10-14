using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Spam_More_Ham
{
    public class Hamspam
    {
        public Dictionary<string, int> spam_dict { get; set; }
        public Dictionary<string, int> ham_dict { get; set; }

        public void Train(string message, bool isSpam)
        {
            string[] words = message.Split(" ");
            foreach(string w in words)
            {
                if (isSpam)
                {
                    if (!spam_dict.ContainsKey(w))
                    {
                        spam_dict[w] += 2;
                    }
                    else
                    {
                        spam_dict[w] += 1;
                    }
                }
                else
                {
                    if (!ham_dict.ContainsKey(w))
                    {
                        ham_dict[w] += 2;
                    }
                    else
                    {
                        ham_dict[w] += 1;
                    }
                }
            }
        }

        public void Train_them(List<string> spam, List<string> ham)
        {
            Regex r = new Regex(@"\w+");
            foreach (string w in spam)
            {
                MatchCollection m = r.Matches(w);
            }
        }


    }
}
