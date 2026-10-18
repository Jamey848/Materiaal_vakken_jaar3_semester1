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
        public int countspam { get; set; }
        public int countham { get; set; }
        public int totalmessages { get; set; }


        public Hamspam(string filename)
        {
            Regex r = new Regex(@"\w+");

            spam_dict = new Dictionary<string, int>();
            ham_dict = new Dictionary<string, int>();
            foreach(string s in File.ReadAllLines(filename))
            {
                totalmessages += 1;
                string classifier = s[0..3];
                if(classifier == "ham")
                {
                    string newstring = s.Substring(4);
                    MatchCollection mc = r.Matches(newstring);
                    foreach(Match m in mc)
                    {
                        string stringstringstring = m.ToString();
                        string matchstring = stringstringstring.ToString().ToLower().Trim();
                        if (ham_dict.Keys.Contains(matchstring))
                        {
                            ham_dict[matchstring] += 1;
                        }
                        else
                        {
                            ham_dict[matchstring] = 1;
                        }
                    }

                    countham += 1;
                }
                else
                {
                    string newstring = s.Substring(5);
                    MatchCollection mc = r.Matches(newstring);
                    foreach (Match m in mc)
                    {
                        string matchstring = m.Value.ToString().ToLower().Trim();
                        if (spam_dict.Keys.Contains(matchstring))
                        {
                            spam_dict[matchstring] += 1;
                        }
                        else
                        {
                            spam_dict[matchstring] = 1;
                        }
                    }

                    countspam += 1;
                }
            }

            ham_dict.OrderByDescending(x => x.Value);
            spam_dict.OrderByDescending(x => x.Value);
        }

        public float likelihood(string word, bool IsSpam)
        {
            if (IsSpam)
            {
                if(spam_dict.ContainsKey(word))
                {
                    return (float)(float)spam_dict[word] / (float)countspam;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (ham_dict.ContainsKey(word))
                {
                    return (float)ham_dict[word] / (float)countham;
                }
                else
                {
                    return 1;
                }
            }
        }

        public bool IsSpam(string message)
        {
            float priorSpam = (float)countspam / (float)totalmessages;
            float priorHam = (float)countham / (float)totalmessages;

            float LogscoreSpam = (float)Math.Log(priorSpam);
            float LogscoreHam = (float)Math.Log(priorHam);

            Regex r = new Regex(@"\w+");
            MatchCollection m = r.Matches(message);
            foreach(Match match in m)
            {
                string matchvalue = match.Value.ToString().ToLower().Trim();
                LogscoreSpam = LogscoreSpam + (float)Math.Log(likelihood(matchvalue, true));
                LogscoreHam = LogscoreHam + (float)Math.Log(likelihood(matchvalue, false));
            }

            Console.WriteLine("Logscorespam: " + LogscoreSpam);
            Console.WriteLine("Logscoreham: " + LogscoreHam);

            return LogscoreSpam > LogscoreHam;
        }














        /*public void Train(string message, bool isSpam)
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
        }*/


    }
}
