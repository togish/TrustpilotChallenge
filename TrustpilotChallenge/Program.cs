using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace TrustpilotChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * It solves the problem.
             * Event though it takes some time.
             * A number of optimizations.
             */

            var ts = DateTime.Now.Ticks;

            var words = new List<String>();

            try
            {
                StreamReader sr = new StreamReader("../../wordlist");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    words.Add(line);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }


            var md5 = "4624d200580677270a54ccff86b9610e";
            var challenge = "poultry outwits ants";

            var anagramSolver = new AnagramSolver(challenge, words);
            var anagram = anagramSolver.FindAnagram(md5);

            Console.WriteLine(anagram);
            Console.WriteLine("Found in " + (DateTime.Now.Ticks - ts) / 10000000 + " secs");
            Console.ReadLine();
        }
    }
}
