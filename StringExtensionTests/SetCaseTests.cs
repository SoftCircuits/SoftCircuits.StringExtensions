using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.StringExtensions;

namespace StringExtensionTests
{
    [TestClass]
    public class SetCaseTests
    {
        [TestMethod]
        public void TestToUpper()
        {
            Assert.AreEqual("ABC", StringExtensions.SetCase("abc", CaseType.Upper));
            Assert.AreEqual("THIS IS A TEST.", StringExtensions.SetCase("This is a Test.", CaseType.Upper));
        }

        [TestMethod]
        public void TestToLower()
        {
            Assert.AreEqual("abc", StringExtensions.SetCase("abc", CaseType.Lower));
            Assert.AreEqual("this is a test.", StringExtensions.SetCase("This is a Test.", CaseType.Lower));
        }

        [TestMethod]
        public void TestCapitalizeFirstCharacter()
        {
            Assert.AreEqual("Abc", StringExtensions.SetCase("abc", CaseType.CapitalizeFirstCharacter));
            Assert.AreEqual("This Is A Test.", StringExtensions.SetCase("this Is A Test.", CaseType.CapitalizeFirstCharacter));
        }

        [TestMethod]
        public void TestSentenceCase()
        {
            Assert.AreEqual(string.Empty, StringExtensions.SetCase(string.Empty, CaseType.Sentence));
            Assert.AreEqual("Abc", StringExtensions.SetCase("abc", CaseType.Sentence));
            Assert.AreEqual("This is a test.", StringExtensions.SetCase("This Is a Test.", CaseType.Sentence));
            Assert.AreEqual("This is HTML.", StringExtensions.SetCase("This Is HTML.", CaseType.Sentence));
            Assert.AreEqual("HTML is different than XML.", StringExtensions.SetCase("HTML is different than XML.", CaseType.Sentence));
            Assert.AreEqual("Abc-def", StringExtensions.SetCase("abc-def", CaseType.Sentence));
        }

        [TestMethod]
        public void TestTitleCase()
        {
            Assert.AreEqual(string.Empty, StringExtensions.SetCase(string.Empty, CaseType.Title));
            Assert.AreEqual("Abc", StringExtensions.SetCase("abc", CaseType.Title));
            Assert.AreEqual("This is a Test.", StringExtensions.SetCase("This Is a Test.", CaseType.Title));
            Assert.AreEqual("This is HTML.", StringExtensions.SetCase("This Is HTML.", CaseType.Title));
            Assert.AreEqual("HTML is Different than XML.", StringExtensions.SetCase("HTML is different than XML.", CaseType.Title));
            Assert.AreEqual("Abc-Def", StringExtensions.SetCase("abc-def", CaseType.Title));
        }
    }
}
