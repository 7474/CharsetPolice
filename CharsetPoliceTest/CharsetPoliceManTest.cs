using CharsetPolice.Police;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace CharsetPoliceTest
{
    [TestClass]
    public class CharsetPoliceManTest
    {
        [TestMethod]
        public void TestSearchCharset()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-8\"></head>";

            var expected = 18;
            var actual = policeMan.SearchCharset(Encoding.UTF8.GetBytes(body));

            Assert.AreEqual(expected, actual, "charsetが含まれている。");
        }
        [TestMethod]
        public void TestSearchCharsetUppercase()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<HTML><HEAD><META CHARSET=\"UTF-8\"></HEAD>";

            // 当面大文字は無視
            //var expected = 18;
            var expected = -1;
            var actual = policeMan.SearchCharset(Encoding.UTF8.GetBytes(body));

            Assert.AreEqual(expected, actual, "CHARSETが含まれているが対象外。");
        }
        [TestMethod]
        public void TestSearchLe()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-16\"></head>";

            var expected = 36;
            var actual = policeMan.SearchCharset(Encoding.Unicode.GetBytes(body));

            Assert.AreEqual(expected, actual, "charsetが含まれている。");
        }
        [TestMethod]
        public void TestSearchBe()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-16\"></head>";

            var expected = 36;
            var actual = policeMan.SearchCharset(Encoding.BigEndianUnicode.GetBytes(body));

            Assert.AreEqual(expected, actual, "charsetが含まれている。");
        }
        [TestMethod]
        public void TestSearchBomLe()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-16\"></head>";

            var expected = 38;
            // XXX BOM付与
            var actual = policeMan.SearchCharset(Encoding.Unicode.GetBytes(body));

            Assert.AreEqual(expected, actual, "charsetが含まれている。");
        }
        [TestMethod]
        public void TestSearchBomBe()
        {
            var policeMan = new CharsetPoliceMan();
            var body = "<html><head><meta charset=\"utf-16\"></head>";

            var expected = 38;
            // XXX BOM付与
            var actual = policeMan.SearchCharset(Encoding.BigEndianUnicode.GetBytes(body));

            Assert.AreEqual(expected, actual, "charsetが含まれている。");
        }
    }
}
