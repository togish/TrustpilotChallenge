using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrustpilotChallenge;

namespace TrustpilotChallengeTest
{
    [TestClass]
    public class AnagramSolverTest
    {
        [TestMethod]
        public void TestCleanAndSort()
        {
            Assert.AreEqual("abcde", AnagramSolver.cleanAndSort("baced"));
            Assert.AreEqual("abcde", AnagramSolver.cleanAndSort("bca  ed"));
            Assert.AreEqual("abcde", AnagramSolver.cleanAndSort("   bca  e         d"));
        }

        [TestMethod]
        public void TestIsInsideSpaced()
        {
            var ags = new AnagramSolver("ailnooprssttttuuwy", new List<string>());
            Assert.IsTrue(ags.isInsideSpace("iloprstttu")); // Trustpilot!
            Assert.IsTrue(ags.isInsideSpace("wyioloprstttu"));
            Assert.IsTrue(ags.isInsideSpace(""));
            Assert.IsFalse(ags.isInsideSpace("qlk"));
        }

        [TestMethod]
        public void TestSingleWord()
        {
            var words = new List<String>();
            words.Add("rbid");
            words.Add("irdb"); // c75e59ac055236b9b7f27a9a82213b8a
            words.Add("bdir");
            words.Add("birg");

            // Bird is the word!
            var ag = new AnagramSolver("bird", words);
            var anagram = ag.FindAnagram("c75e59ac055236b9b7f27a9a82213b8a");
            Assert.AreEqual("irdb", anagram);
        }

        [TestMethod]
        public void TestTwoWord()
        {
            var words = new List<String>();
            words.Add("bird");
            words.Add("word");

            // Bird is the word!
            var ag = new AnagramSolver("bird word", words);
            var anagram = ag.FindAnagram("80584d36adfcd7c41cf421f10aa408aa");
            Assert.AreEqual("word bird", anagram);
        }

        [TestMethod]
        public void TestTwoWord2()
        {
            var words = new List<String>();
            words.Add("bird");
            words.Add("word");
            words.Add("wrod");

            // Bird is the word!
            var ag = new AnagramSolver("bird word", words);
            var anagram = ag.FindAnagram("843281363c1d2406e9479ebad2c2e2b1");
            Assert.AreEqual("bird wrod", anagram);
        }
    }
}
