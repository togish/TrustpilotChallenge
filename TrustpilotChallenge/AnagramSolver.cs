using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

namespace TrustpilotChallenge
{
    public class AnagramSolver
    {
        // No words consist of a single consonant.
        private static string[] CONSONANTS = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "x", "z", "w", "Y" };

        // Word lookup, is filled during the construction
        private Dictionary<String, List<String>> sWords = new Dictionary<String, List<String>>();
        private string[] sWordsClean;

        // Variables used running the algorithm
        private string sChallenge;
        private int[] sChallengeMap;

        private LinkedAnagram sTree = new LinkedAnagram();

        // Answer validation
        private MD5 md5Hash = MD5.Create();
        private string sTargetMD5;

        /**
         * Sets up the anagram solver.
         * Challenge is the sentense used as a base.
         * Words is the dictionary for the anagram solver.
         */
        public AnagramSolver(String challenge, List<String> words)
        {
            // Clean and sort the letters in the challenge
            sChallenge = cleanAndSort(challenge);
            sChallengeMap = distributionMap(sChallenge);

            // Reduces the problem size. By initially cleaning the dataset.
            // - Remove dublicates
            foreach (var w in words.Distinct())
            {
                var cleanWord = cleanAndSort(w);

                // - only use words that can be constructed of the letters in the challenge
                // - Ignore single letter words only consisting of consonants.
                if (isInsideSpace(cleanWord) && !(cleanWord.Length == 1 && CONSONANTS.Contains(cleanWord)))
                {
                    if (!sWords.ContainsKey(cleanWord))
                        sWords.Add(cleanWord, new List<string>());
                    sWords[cleanWord].Add(w);
                }
            }

            // Creating array for fast lookup
            sWordsClean = sWords.Keys.ToArray();

            // Sorting for longest words first, optimizing the chance of finding the anagram faster.
            Array.Sort(sWordsClean, (a, b) => b.Length.CompareTo(a.Length));
        }

        /**
         * Find anagrams returns all the possible anagrams using the words i the dictionary.
         */
        public String FindAnagram(String md5Sum)
        {
            // Sets the local var with the wanted md5 sum.
            sTargetMD5 = md5Sum;

            // prepares for searching
            sTree.Clear();

            // Starts the process
            return findNext(0);
        }

        /**
         * Recursive function that finds the next word and anagram.
         * iv - init vector, how long down the array to start the next levels iteration.
         */
        private String findNext(int iv)
        {
            // The current anagram to be considered
            var treeString = cleanAndSort(sTree.ToString());

            // Maximum length of possible next word
            var maxLength = sChallenge.Length - treeString.Length;

            // Printing status messages to the console
            if (sTree.Count == 1) Console.WriteLine(iv);

            // IT IS AN ANAGRAM! Horray, now verify the anagram.
            if (sChallenge.Length == treeString.Length && sChallenge.Equals(treeString))
            {
                byte[] data;
                StringBuilder sBuilder;
                foreach (var anagram in sTree.AnagramsFromTree(sWords))
                {
                    data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(anagram));
                    sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                    
                    // if the hash is correct, return.
                    if(sBuilder.ToString().Equals(sTargetMD5)) 
                        return anagram;
                }
            }

            // It was not an anagram. Not full length, append words.
            else if (0 < maxLength && isInsideSpace(treeString))
            {
                for (int i = iv; i < sWordsClean.Length; i++)
                {
                    // Skips words that is too long
                    if (sWordsClean[i].Length <= maxLength)
                    {
                        // Add word and try.
                        sTree.AddLast(sWordsClean[i]);
                        var childRet = findNext(i);
                        if(childRet != null) return childRet;
                    }
                }

                // We are done! 
                if (sTree.First == null) return null;
            }

            // pop the last child and return.
            sTree.RemoveLast();
            return null;
        }

        /**
         * Checks if the subject is inside the char space for the challenge.
         */
        public bool isInsideSpace(String subject)
        {
            if (sChallenge.Length < subject.Length) return false;
            int[] distMapSubject = distributionMap(subject);

            for (var i = 0; i < 255; i++)
                if (sChallengeMap[i] < distMapSubject[i]) return false;

            return true;
        }

        /**
         * Creates a sorted string without spaces.
         */
        public static string cleanAndSort(string str)
        {
            return String.Concat(str.Replace(" ", String.Empty).OrderBy(c => c));
        }

        /**
         * Creates an array with the count of each chars apperance.
         */
        public static int[] distributionMap(string str)
        {
            var charArr = str.ToCharArray(0, str.Length);
            int[] map = new int[255];
            foreach (var ch in charArr)
                map[ch]++;
            return map;
        }
    }
}
