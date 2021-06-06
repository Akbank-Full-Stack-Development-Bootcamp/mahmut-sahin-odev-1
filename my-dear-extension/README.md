## My Dear Extension ##
This is the extension method written for strings. It returns each character's hexadecimal value in a List<>.

***
#### Source Code ####
```csharp 
public static List<string> CharHexs(this string sentence)
{
    List<string> hexList = new();
    byte[] wordBytes = Encoding.ASCII.GetBytes(sentence);
    foreach (var character in wordBytes)
    {
        //Adding prefix and specifying hexadecimal format
        hexList.Add(character.ToString("X")); 
    }
    return hexList;
}

```
***
#### Example ####
```csharp 
Console.Write("Please enter a sentence: ");
string sentence = Console.ReadLine(); // Input: Lorem Ipsum.

List<string> hexList = sentence.CharHexsInArr(); // Calling extension
hexList.ForEach(i => Console.ReadLine(i)); // Printing output
/* 
OUTPUT:
4C
6F
72
65
6D
20
49
70
73
75
6D
2E
*/
```
