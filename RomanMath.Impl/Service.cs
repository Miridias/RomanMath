using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RomanMath.Impl
{
    public static class Service
    {
        /// <summary>
        /// See TODO.txt file for task details.
        /// Do not change contracts: input and output arguments, method name and access modifiers
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static int Evaluate(string expression)
        {
            // Заполнение коллекции
            List<int> noMullNum = new List<int>();
            Dictionary<char, int> mathDictionary = new Dictionary<char, int>();
            mathDictionary.Add('M', 1000);
            mathDictionary.Add('D', 500);
            mathDictionary.Add('C', 100);
            mathDictionary.Add('L', 50);
            mathDictionary.Add('X', 10);
            mathDictionary.Add('V', 5);
            mathDictionary.Add('I', 1);
            mathDictionary.Add(' ', 0);
            
            int expressionLength = 0;
            // Поиск Римских чисел
            
            string patternNumber = @"\w+";
            var regex = new Regex(patternNumber);
            MatchCollection romanNumber = regex.Matches(expression);

            // Поиск знаков операций
            string patternOperation = @"[-,+,*]+";
            regex = new Regex(patternOperation);
            MatchCollection operation = regex.Matches(expression);

            // Проверка отобраных символов на соответствие с ключами коллекции
            foreach (var item in romanNumber)
            {
                string str = item.ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    if (mathDictionary.ContainsKey(str[i])) expressionLength++;
                }
            }
            foreach (var item in operation) expressionLength++;

            if (expressionLength != expression.Length) return 0;

            var num = NumberGeneration(romanNumber, mathDictionary);

            //Выполнение операций умножения
            for (int i = operation.Count-1; i >= 0; i--)
            {
                if (operation[i].Value == "*")
                {
                    int[] newArr = new int[num.Length - 1];
                    newArr = SearchMul(i,num);
                    num = newArr;
                }
            }

            //Запись полученых результатов в новую коллекцию
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] != 0)
                {
                    noMullNum.Add(num[i]);
                }
            }
            int result = noMullNum[0];
            int count = 1;
            // Выполнение операций додавания и вычитания
            for (int i = 0; i < operation.Count; i++)
            {
                if (operation[i].Value == "+")
                {
                    result += noMullNum[count];
                    count++;
                }
                if (operation[i].Value == "-")
                {
                    result -= noMullNum[count];
                    count++;
                }
            }
            if (result > 0)
            {
                return result;
            }
            else return 0;
        }
        // Метод умноженияя чисел
        public static int[] SearchMul(int i, int[] num)
        {
            int[] testNum = new int[num.Length];
            for (int f = 0 ; f < testNum.Length ; f++)
            {
                testNum[f] = num[f];
            }
            testNum[i] = num[i] * num[i + 1];

            testNum[i + 1] = 0;

            return testNum;
        }
        // Преобразование римских чисел в арабские
        public static int [] NumberGeneration(MatchCollection romanNumber, Dictionary<char, int> mathDictionary)
        {
            int[] numb = new int[romanNumber.Count];
            for (int i = 0; i < romanNumber.Count; i++)
            {
                int[] value1 = new int[romanNumber[i].Length];
                for (int j = 0; j < romanNumber[i].Length; j++)
                {
                    foreach (var item in mathDictionary)
                    {
                        if (item.Key == romanNumber[i].Value[j])
                        {
                            value1[j] = item.Value;
                        }
                    }
                }
                numb[i] = value1[0];
                if (value1.Length > 1)
                {
                    for (int j = 0; j < value1.Length - 1; j++)
                    {
                        if (value1[j] >= value1[j + 1])
                        {
                            numb[i] += value1[j + 1];
                        }
                        else if (value1[j] < value1[j + 1])
                        {
                            numb[i] = value1[j + 1] - value1[j];
                        }
                    }
                }
                else
                {
                    numb[i] = value1[0];
                }
            }
            return numb;
        }
        
    }
}