using FunctionAnalysis;

namespace FunctionAnalysisTests
{
    public class ClassTaskTests
    {
        private string _path;

        [SetUp]
        public void Setup()
        {
            var path = Directory.GetCurrentDirectory();
            var t = path.IndexOf("FunctionAnalysisTests");
            _path = path.Remove(t, path.Length - t) + "FunctionAnalysisTests\\Resources\\TestXml.xml";
        }

        [TestCase(null, "book", "sddsds", ExpectedResult = null)]
        [TestCase(null, "dsdsds", "id", ExpectedResult = null)]
        [TestCase("weew", "book", "id", ExpectedResult = null)]
        [TestCase(null, "book", "id", ExpectedResult = "bk101")]
        [TestCase(null, "book", "name", ExpectedResult = "Test1")]
        public string? MyFuncTest(string path, string elementName, string attrName)
        {
            if (string.IsNullOrEmpty(path))
                path = _path;
            var actual = ClassTask.MyFunc(path, elementName, attrName);
            return actual;
        }
    }
}