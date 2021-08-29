using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.StringExtensions;

namespace StringExtensionTests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void TestNullAndEmptyStrings()
        {
            Assert.AreEqual("abc", StringExtensions.EmptyIfNull("abc"));
            Assert.AreEqual(string.Empty, StringExtensions.EmptyIfNull(null));
            Assert.AreEqual(string.Empty, StringExtensions.EmptyIfNull(string.Empty));

            Assert.AreEqual("abc", StringExtensions.NullIfEmpty("abc"));
            Assert.AreEqual(null, StringExtensions.NullIfEmpty(null));
            Assert.AreEqual(null, StringExtensions.NullIfEmpty(string.Empty));

            Assert.AreEqual("abc", StringExtensions.EmptyIfNullOrWhiteSpace("abc"));
            Assert.AreEqual(string.Empty, StringExtensions.EmptyIfNullOrWhiteSpace(null));
            Assert.AreEqual(string.Empty, StringExtensions.EmptyIfNullOrWhiteSpace(string.Empty));
            Assert.AreEqual(string.Empty, StringExtensions.EmptyIfNullOrWhiteSpace("   "));

            Assert.AreEqual("abc", StringExtensions.NullIfEmptyOrWhiteSpace("abc"));
            Assert.AreEqual(null, StringExtensions.NullIfEmptyOrWhiteSpace(null));
            Assert.AreEqual(null, StringExtensions.NullIfEmptyOrWhiteSpace(string.Empty));
            Assert.AreEqual(null, StringExtensions.NullIfEmptyOrWhiteSpace("   "));
        }

        [TestMethod]
        public void TestNormalizeWhiteSpace()
        {
            Assert.AreEqual(string.Empty, StringExtensions.NormalizeWhiteSpace(string.Empty));
            Assert.AreEqual(string.Empty, StringExtensions.NormalizeWhiteSpace("   "));
            Assert.AreEqual("a", StringExtensions.NormalizeWhiteSpace("   a   "));
            Assert.AreEqual("a b c", StringExtensions.NormalizeWhiteSpace("    a   b   c   "));
            Assert.AreEqual("This is a test!", StringExtensions.NormalizeWhiteSpace("  This\r\n is  a\t\t\ttest!   "));
        }

        [TestMethod]
        public void TestCountWords()
        {
            Assert.AreEqual(0, StringExtensions.CountWords(string.Empty));
            Assert.AreEqual(0, StringExtensions.CountWords("   "));
            Assert.AreEqual(1, StringExtensions.CountWords("   abc   "));
            Assert.AreEqual(4, StringExtensions.CountWords("   This is  a  test   "));
            Assert.AreEqual(14, StringExtensions.CountWords("This is a test of the Emergency Broadcast System. This is only a test."));
            Assert.AreEqual(6, StringExtensions.CountWords("It's the 44.7 plus another 22.456%!"));
            Assert.AreEqual(5, StringExtensions.CountWords("10-11 44 inch. 32.77 19!"));
            Assert.AreEqual(1, StringExtensions.CountWords("  abc.def "));
        }

        [TestMethod]
        public void TestReverse()
        {
            Assert.AreEqual("cba", StringExtensions.Reverse("abc"));
            Assert.AreEqual("fedcba", StringExtensions.Reverse("abcdef"));
            Assert.AreEqual("   ", StringExtensions.Reverse("   "));
            Assert.AreEqual(string.Empty, StringExtensions.Reverse(string.Empty));
        }

        [TestMethod]
        public void TestDistinct()
        {
            Assert.AreEqual("abc", StringExtensions.Distinct("abc"));
            Assert.AreEqual("abcdef", StringExtensions.Distinct("abcdef"));
            Assert.AreEqual("abcdef", StringExtensions.Distinct("aabbccddeeff"));
            Assert.AreEqual(" ", StringExtensions.Distinct("   "));
            Assert.AreEqual(string.Empty, StringExtensions.Distinct(string.Empty));

            Assert.AreEqual("abc", StringExtensions.Distinct("abc", true));
            Assert.AreEqual("abcdef", StringExtensions.Distinct("abcdef", true));
            Assert.AreEqual("abcdef", StringExtensions.Distinct("aAbBcCdDeEfF", true));
            Assert.AreEqual(" ", StringExtensions.Distinct("   ", true));
            Assert.AreEqual(string.Empty, StringExtensions.Distinct(string.Empty, true));
        }

        [TestMethod]
        public void TestUnion()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Union(string.Empty, string.Empty));
            Assert.AreEqual("abc", StringExtensions.Union("abc", string.Empty));
            Assert.AreEqual("def", StringExtensions.Union(string.Empty, "def"));
            Assert.AreEqual("abcdef", StringExtensions.Union("abc", "def"));
            Assert.AreEqual("abcdef", StringExtensions.Union("abcd", "cdef"));

            Assert.AreEqual("abc", StringExtensions.Union("abc", "ABC", true));
            Assert.AreEqual("abcDEF", StringExtensions.Union("abc", "DEF", true));
            Assert.AreEqual("abcdEF", StringExtensions.Union("abcd", "CDEF", true));
        }

        [TestMethod]
        public void TestIntersect()
        {
            Assert.AreEqual("cd", StringExtensions.Intersect("abcd", "cdef"));
            Assert.AreEqual("abcd", StringExtensions.Intersect(" a.b,c#d*", "abcdefghijklmnopqrstuvwxyz"));
            Assert.AreEqual(string.Empty, StringExtensions.Intersect("ABCD", "cdef"));
            Assert.AreEqual("CD", StringExtensions.Intersect("ABCD", "cdef", true));
            Assert.AreEqual("AC", StringExtensions.Intersect("ABCDEFG", "ca", true));
        }

        [TestMethod]
        public void TestExcept()
        {
            Assert.AreEqual(string.Empty, StringExtensions.Except("abc", "abc"));
            Assert.AreEqual("abc", StringExtensions.Except("abc", "ABC"));
            Assert.AreEqual(string.Empty, StringExtensions.Except("abc", "ABC", true));
            Assert.AreEqual("abcdef", StringExtensions.Except("a1b2c3d4e5f6", "123456", true));
        }

        [TestMethod]
        public void TestSort()
        {
            Assert.AreEqual("dfhisw", StringExtensions.Sort("dihsfw"));
            Assert.AreEqual("FSWdhi", StringExtensions.Sort("dihSFW"));
            Assert.AreEqual("dFhiSW", StringExtensions.Sort("dihSFW", true));
        }

        [TestMethod]
        public void TestContainsAny()
        {
            Assert.AreEqual(false, StringExtensions.ContainsAny("abcdef", string.Empty));
            Assert.AreEqual(true, StringExtensions.ContainsAny("abcdef", "a"));
            Assert.AreEqual(true, StringExtensions.ContainsAny("abcdef", "d"));
            Assert.AreEqual(true, StringExtensions.ContainsAny("abcdef", "f"));
            Assert.AreEqual(false, StringExtensions.ContainsAny(string.Empty, " xdyz "));
        }

        [TestMethod]
        public void TestInsertCamelCaseSpaces()
        {
            Assert.AreEqual("abc", StringExtensions.InsertCamelCaseSpaces("abc"));
            Assert.AreEqual("Abc", StringExtensions.InsertCamelCaseSpaces("Abc"));

            Assert.AreEqual("this Is A Test", StringExtensions.InsertCamelCaseSpaces("thisIsATest"));
            Assert.AreEqual("This Is A Test", StringExtensions.InsertCamelCaseSpaces("ThisIsATest"));

            Assert.AreEqual("this Is HTML", StringExtensions.InsertCamelCaseSpaces("thisIsHTML"));
            Assert.AreEqual("This Is HTML", StringExtensions.InsertCamelCaseSpaces("ThisIsHTML"));

            Assert.AreEqual("this is a test", StringExtensions.InsertCamelCaseSpaces("this is a test"));
            Assert.AreEqual("This Is A Test", StringExtensions.InsertCamelCaseSpaces("This Is A Test"));

            Assert.AreEqual("I Bought An IBMXT", StringExtensions.InsertCamelCaseSpaces("IBoughtAnIBMXT"));   // No way to know IBM XT
            Assert.AreEqual("I Bought An Ibm XT", StringExtensions.InsertCamelCaseSpaces("IBoughtAnIbmXT"));
            Assert.AreEqual("I Bought An Ibm Xt", StringExtensions.InsertCamelCaseSpaces("IBoughtAnIbmXt"));
        }
    }
}
