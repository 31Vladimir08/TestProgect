using System.Xml.Linq;

namespace FunctionAnalysis
{
    public class ClassTask
    {
        /*
         * Если файл не существует, упадет в ошибку (возможно проверка реализована в мете вызова этой функции).
         * Если <book 
		        id="bk101" 
		        name="Test1">
           упадет в ошибку (возможно такого кейса не будет)
         * result заменить string на StringBuilder.
         * сразу считывать весь файл не очень хорошо, если на вход придет бесконечно большой xml 30Гб
         * логику работы с массивом строк винести в отдельный метод
         * возвращает первый найденный элемент, а не все элементы, удовлетворяющие условию (возможно это правильная реализация)
         * условия startElEndex != -1 и line[startElEndex - 1] == '<' можно объеденить в один if
         */
        public static string Func1(string input, string elementName, string attrName)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            string result = null;

            foreach (var line in lines)
            {
                var startElEndex = line.IndexOf(elementName);

                if (startElEndex != -1)
                {
                    if (line[startElEndex - 1] == '<')
                    {
                        var endElIndex = line.IndexOf('>', startElEndex - 1);
                        var attrStartIndex = line.IndexOf(attrName, startElEndex, endElIndex - startElEndex + 1);

                        if (attrStartIndex != -1)
                        {
                            int valueStartIndex = attrStartIndex + attrName.Length + 2;

                            while (line[valueStartIndex] != '"')
                            {
                                result += line[valueStartIndex];
                                valueStartIndex++;
                            }

                            break;
                        }
                    }
                }
            }

            return result;
        }

        public static string? MyFunc(string input, string elementName, string attrName)
        {
            try
            {
                var root = XElement.Load(input);

                var result = root.Elements(elementName).FirstOrDefault()?.Attribute(attrName)?.Value;
                return result;
            }
            catch (FileNotFoundException e)
            {
                return null;
            }            
        }
    }
}
