using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class OzonTasks
    {
        public void A()
        {
            var arr = Console.ReadLine()?.Split(' ');
            if (arr == null || arr.Length != 2)
                return;
            try
            {
                var result = int.Parse(arr[0]) + int.Parse(arr[1]);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                return;
            }            
        }
    }
}
