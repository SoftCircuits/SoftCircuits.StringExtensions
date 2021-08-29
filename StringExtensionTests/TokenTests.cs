using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftCircuits.StringExtensions;

namespace StringExtensionTests
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void TestTokenize()
        {
            CollectionAssert.AreEqual(new[] { "abc", "def" }, StringExtensions.Tokenize("abc  def", " "));
            CollectionAssert.AreEqual(new[] { "abc", "def", "ghi", "jkl", "mno" }, StringExtensions.Tokenize("   , abc,def  ghi   ,jkl     ,      mno    ", " ,"));

            CollectionAssert.AreEqual(new[] { "abc", "def", "ghi", "jkl", "mno" }, StringExtensions.Tokenize("   \t abc\tdef  ghi   \tjkl     \t      mno    ", char.IsWhiteSpace));
        }

        [TestMethod]
        public void TestGetNextToken()
        {
            string s = " abc def,ghi:;jkl...mno!@#pqr";
            int pos = 0;

            Assert.AreEqual("abc", StringExtensions.GetNextToken(s, " ", ref pos));
            Assert.AreEqual("def", StringExtensions.GetNextToken(s, " ,", ref pos));
            Assert.AreEqual("ghi", StringExtensions.GetNextToken(s, ",:;", ref pos));
            Assert.AreEqual("jkl", StringExtensions.GetNextToken(s, ",:;.", ref pos));
            Assert.AreEqual("mno", StringExtensions.GetNextToken(s, ".!@#", ref pos));
            Assert.AreEqual("pqr", StringExtensions.GetNextToken(s, "!@#", ref pos));
        }
    }
}
