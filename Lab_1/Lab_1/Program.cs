using System;
public class LR1
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
        string vowels = "AEIOUYaeiouyАЕЁИОУЫЭЮЯаеёиоуыэюя";
        string result = "";

        foreach (char c in input)
        {
            result += c;
            if (vowels.Contains(c))
            {
                result += c;
            }
        }

        return result;
    }
}
