using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my_dear_extension.Extensions
{
    public static class MyDearExtension
    {
        public static List<string> CharHexs(this string sentence)
        {
            List<string> hexList = new();
            byte[] wordBytes = Encoding.ASCII.GetBytes(sentence);
            foreach (var character in wordBytes)
            {
                hexList.Add(character.ToString("X"));
            }
            return hexList;
        }
    }
}
