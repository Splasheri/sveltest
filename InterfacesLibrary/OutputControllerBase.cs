using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary
{
    public class OutputControllerBase
    {
        public void PrintList(List<string> hintList)
        {
            foreach (var hint in hintList)
            {
                Console.WriteLine("-" + hint);
            }
            Console.WriteLine();
        }
        public void PrintError(Exception exception)
        {
            Console.WriteLine("\n\n\tПроизошла ошибка " + exception.Message);
        }
        public void PrintLine(string text = "")
        {
            Console.WriteLine(text);
        }
        public void Print(string text)
        {
            Console.Write(text);
        }
    }
}
