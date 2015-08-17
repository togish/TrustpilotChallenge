using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrustpilotChallenge;

namespace TrustpilotChallengeTest
{
    [TestClass]
    public class LinkedAnagramTest
    {
        [TestMethod]
        public void TestToSting()
        {
            var la = new LinkedAnagram();
            Assert.AreEqual(la.ToString(), string.Empty);

            la.AddLast("ehj"); // Hej
            Assert.AreEqual(la.ToString(), "ehj");

            la.AddLast("dem"); // med
            Assert.AreEqual(la.ToString(), "ehjdem");

            la.AddLast("dig"); // dig
            Assert.AreEqual(la.ToString(), "ehjdemdig");
        }
        
        [TestMethod]
        public void TestAnagramFromTree()
        {
            var la = new LinkedAnagram();
            Assert.AreEqual(la.ToString(), string.Empty);

            la.AddLast("ehj"); // Hej
            la.AddLast("dem"); // med
            la.AddLast("dig"); // dig

            var words = new Dictionary<String, List<String>>();

            var ehjList = new List<String>();
            ehjList.Add("hej");
            words.Add("ehj", ehjList);

            var demList = new List<String>();
            demList.Add("med");
            words.Add("dem", demList);
            
            var digList = new List<String>();
            digList.Add("dig");
            words.Add("dig", digList);
            
            var anagrams = la.AnagramsFromTree(words);

            // Testing permutation among word groups.
            Assert.AreEqual(6, anagrams.Count);
        }

        [TestMethod]
        public void TestPermutation()
        {
            var sp = new int[] { 5, 6 };
            var ret = LinkedAnagram.Permut<int>(sp);
            Assert.AreEqual(2, ret.Length);
            Assert.AreEqual(2, ret.Distinct().Count());

            sp = new int[] { 5, 6, 7 };
            ret = LinkedAnagram.Permut<int>(sp);
            Assert.AreEqual(6, ret.Length);
            Assert.AreEqual(6, ret.Distinct().Count());

            sp = new int[] { 5, 6, 7, 8 };
            ret = LinkedAnagram.Permut<int>(sp);
            Assert.AreEqual(24, ret.Length);
            Assert.AreEqual(24, ret.Distinct().Count());

            sp = new int[] { 5, 6, 7, 8, 9 };
            ret = LinkedAnagram.Permut<int>(sp);
            Assert.AreEqual(120, ret.Length);
            Assert.AreEqual(120, ret.Distinct().Count());
        }

    }
}
