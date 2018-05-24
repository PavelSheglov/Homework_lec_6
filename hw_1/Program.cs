using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hw_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = InputData();
            string file = @"..\..\Files\numbers.txt";
            Console.WriteLine("Нажмите любую клавишу для записи данных в файл.....");
            Console.ReadKey();
            if (WriteData(numbers, file))
                Console.WriteLine("Данные успешно записаны в файл {0}", file);
            else
                Console.WriteLine("При записи в файл {0} произошла ошибка!!!", file);
            Console.WriteLine("Нажмите любую клавишу для чтения данных из файла.....");
            Console.ReadKey();
            var loadNumbers = ReadData(file);
            if (loadNumbers.Count > 0)
            {
                Console.WriteLine("Данные успешно считаны из файла {0}", file);
                foreach (var number in loadNumbers)
                    Console.WriteLine(number);
            }
            else
                Console.WriteLine("При чтении из файла {0} произошла ошибка!!!", file);
        }

        public static List<short> InputData()
        {
            bool isFinal = false;
            List<short> numbers = new List<short>();
            while (!isFinal)
            {
                bool isOk = false;
                short number = 0;
                Console.Clear();
                while (!isOk)
                {
                    try
                    {
                        Console.WriteLine("Введите целое число (от {0} до {1}):", short.MinValue, short.MaxValue);
                        number = Convert.ToInt16(Console.ReadLine());
                        isOk = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Значение не является целым числом. Повторите ввод....");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Значение вне допустимого диапазона. Повторите ввод....");
                    }
                }
                numbers.Add(number);
                Console.WriteLine("Продолжить ввод?\n1.Да\n2.Нет");
                switch (Console.ReadLine())
                {
                    case "1":
                        isFinal = false;
                        break;
                    case "2":
                    default:
                        isFinal = true;
                        break;
                }
            }
            return numbers;
        }
        public static bool WriteData(List<short> numbers, string fileName)
        {
            bool result = false;
            using (var stream = new StreamWriter(fileName))
            {
                foreach (var number in numbers)
                    stream.WriteLine(number.ToString());
                result = true;
            }
            return result;
        }
        public static List<short> ReadData(string fileName)
        {
            var result = new List<short>();
            using (var stream = new StreamReader(fileName))
            {
                string number = stream.ReadLine();
                while(number!=null)
                {
                    result.Add(Convert.ToInt16(number));
                    number = stream.ReadLine();
                }
            }
            return result;
        }
    }
}
