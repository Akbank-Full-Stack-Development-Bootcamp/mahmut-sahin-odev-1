using my_dear_extension.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace my_dear_extension
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please enter a sentence: ");
            string sentence = Console.ReadLine();
            List<string> hexList = sentence.CharHexs();
            hexList.ForEach(i => Console.WriteLine(i));
            Console.ReadLine();
        }
    }
}
