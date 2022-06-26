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

        [TestCase("book", "id", ExpectedResult = "bk101")]
        [TestCase("book", "name", ExpectedResult = "Test1")]
        public string? MyFuncTest(string elementName, string attrName)
        {
            var actual = ClassTask.MyFunc(_path, elementName, attrName);
            return actual;
        }
    }
}