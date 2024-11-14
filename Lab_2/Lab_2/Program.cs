using System;
using System.Text.RegularExpressions;

class LR2
{
    static void Main()
    {
        string input = "Primer stroki dlya testа";
        string inputRus = "Пример строки для теста";
        Console.WriteLine(input);
        Console.WriteLine(inputRus);
        string result = DuplicateVowels(input);
        string resultRus = DuplicateVowels(inputRus);
        Console.WriteLine(result);
        Console.WriteLine(resultRus);
    }

    static string DuplicateVowels(string input)
    {
        string pattern = "[AEIOUYaeiouyАЕЁИОУЫЭЮЯаеёиоуыэюя]";
        string result = Regex.Replace(input, pattern, "$0$0");
        return result;
    }
}
